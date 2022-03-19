using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ToXYOne(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 1);
        }
        
        public static Vector3 ToXYZero(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }
        
        public static Vector2 FromXY(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
    }
}