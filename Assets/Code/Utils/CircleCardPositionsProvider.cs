using System;
using Core.Cards;
using Extensions;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Utils
{
    public class CircleCardPositionsProvider : MonoBehaviour
    {
        private Vector3 _circleCenter;
        private float _circleRadius = 0;
        private float _halfAngle;

        public void Init(float horizontalOffset, float verticalOffset, Vector3 centerOffset)
        {
            Vector2 origin = (centerOffset).FromXY();
            Vector2 one = origin + new Vector2(-horizontalOffset, 0);
            Vector2 two = origin + new Vector2(0, verticalOffset);
            Vector2 three = origin + new Vector2(horizontalOffset, 0);
            
            Init(one, two, three);
        }

        public void Init(Vector2 one, Vector2 two, Vector2 three)
        {
            CircleEquation(one, two, three, out Vector2 center, out _circleRadius);
            _circleCenter = center;
            _halfAngle = Vector2.Angle(one - center, two - center) * (float)Math.PI / 180;
        }
        
        private void OnDrawGizmos()
        {
            if(_circleRadius == 0) return;
            
            Gizmos.color = Color.green;
            
            Gizmos.DrawWireSphere(_circleCenter, (float)_circleRadius);
            Gizmos.DrawCube(_circleCenter, Vector3.one);
            
            int pointsCount = 5;
            Gizmos.color = Color.red;

            for (int i = 0; i < pointsCount; i++)
            {
                Gizmos.DrawCube(_circleCenter + GetPoint(i, pointsCount).ToXYZero(), Vector3.one);
            }
        }
        
        public Vector2 GetPoint(int index, int pointsCount)
        {
            double angle = -_halfAngle + 2 * _halfAngle / (pointsCount - 1) * index;

            Vector2 point = new Vector2();
            point.x = (float)(_circleRadius * Math.Sin(angle));
            point.y = (float)(_circleRadius * Math.Cos(angle));

            return point;
        }
        
        public CardPosition GetPosition(int index, int pointsCount)
        {
            double angle = pointsCount > 1
                ? -_halfAngle + 2 * _halfAngle / (pointsCount - 1) * index
                : angle = 0;
            
            Vector3 point = Vector3.zero;
            point.x = (float)(_circleRadius * Math.Sin(angle));
            point.y = (float)(_circleRadius * Math.Cos(angle));

            return new CardPosition(_circleCenter + point, new Vector3(0,0, (float)-angle * 180 / (float)Math.PI));
        }

        private void CircleEquation(Vector2 one, Vector2 two, Vector2 three, out Vector2 center, out float radius)
        {
            double a =
                ((two.y - three.y) * (one.x * one.x + (one.y - two.y) * (one.y - three.y)) +
                 two.x * two.x * (three.y - one.y) + three.x * three.x * (one.y - two.y)) /
                (2 * (one.x * (three.y - two.y) + two.x * (one.y - three.y) + three.x * (two.y - one.y)));

            double b = (one.x * one.x * (two.x - three.x) +
                        one.x * (-two.x * two.x + three.x * three.x - two.y * two.y + three.y * three.y) +
                        two.x * two.x * three.x -
                        two.x * (three.x * three.x - one.y * one.y + three.y * three.y) +
                        three.x * (two.y * two.y - one.y * one.y)) /
                       (2 * (one.x * (two.y - three.y) + two.x * (three.y - one.y) + three.x * (one.y - two.y)));

            double c =
                (two.x * (one.x * one.x * (-three.y) + three.x * three.x * one.y +
                          one.y * three.y * (three.y - one.y)) +
                 two.y * (one.x * one.x * three.x - one.x * (three.x * three.x - two.y * three.y + three.y * three.y) +
                          three.x * one.y * (one.y - two.y)) + two.x * two.x * (one.x * three.y - three.x * one.y)) /
                (one.x * (two.y - three.y) + two.x * (three.y - one.y) + three.x * (one.y - two.y));

            center = new Vector2((float)-a, (float)-b);
            radius = (float)Math.Sqrt(a * a + b * b - c);
        }
    }
}