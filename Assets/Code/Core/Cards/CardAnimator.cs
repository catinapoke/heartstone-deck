using System.Collections.Generic;
using Configs;
using Core.Drag;
using DG.Tweening;
using Extensions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Zenject;

namespace Core.Cards
{
    [RequireComponent(typeof(Card), typeof(Image))]
    public class CardAnimator : MonoBehaviour
    {
        [SerializeField] private Image _cardBackground;

        [Inject] private CoreGameConfig _config;
        [Inject(Id = "ShinyCard")] private Material _shinyMaterial;
        [Inject(Id = "DefaultCard")] private Material _defaultMaterial;

        private CardIntAttributeView[] _attributeViews;
        private Image _raycastCatcher;
        private List<Tween> _tweens;

        public bool IsComplete => IsAllTweensComplete() && IsAttributeAnimationComplete();

        private void Awake()
        {
            _raycastCatcher = GetComponent<Image>();
            _tweens = new List<Tween>(2);
            _attributeViews = GetComponentsInChildren<CardIntAttributeView>();
        }

        private void OnValidate()
        {
            Assert.IsNotNull(_cardBackground);
        }

        public Tween Appear()
        {
            KillTweens();
            Tweener tween = transform.DOScale(Vector3.one, _config.AnimatorDuration)
                .From(Vector3.zero)
                .OnKill(() => transform.localScale = Vector3.one);

            _tweens.Add(tween);
            return tween;
        }
        
        public Tween Disappear()
        {
            KillTweens();
            Tweener tween = transform.DOScale(Vector3.zero, _config.AnimatorDuration)
                .From(Vector3.one)
                .OnKill(() => transform.localScale = Vector3.zero);
            
            _tweens.Add(tween);
            return tween;
        }

        public Tween MoveTo(Vector3 point, Vector3 eulerAngles)
        {
            KillTweens();
            
            _tweens.Add(transform.DOMove(point, _config.AnimatorDuration));
            _tweens.Add(transform.DORotate(eulerAngles, _config.AnimatorDuration));
            return _tweens[0];
        }
        
        public Tween LocalMoveTo(Vector3 point, Vector3 eulerAngles)
        {
            KillTweens();
            
            _tweens.Add(transform.DOLocalMove(point, _config.AnimatorDuration));
            _tweens.Add(transform.DORotate(eulerAngles, _config.AnimatorDuration));
            return _tweens[0];
        }
        
        public void MoveTo(CardPosition data)
        {
            MoveTo(data.Position, data.Rotation);
        }
        
        public void LocalMoveTo(CardPosition data)
        {
            LocalMoveTo(data.Position, data.Rotation);
        }

        public void FollowPointer(IInputProvider inputProvider)
        {
            KillTweens();
            
            _tweens.Add(transform.DORotate(Vector3.zero, _config.AnimatorDuration));
            Tweener tween = transform.DOMove(inputProvider.GetPosition().ToXYZero(), _config.CardMoveSpeed).SetSpeedBased();
            tween.OnUpdate (() => tween.ChangeEndValue(inputProvider.GetPosition().ToXYZero(), true))
                    .OnStart(() => _raycastCatcher.raycastTarget = false)
                    .OnKill(() => _raycastCatcher.raycastTarget = true);

            _tweens.Add(tween);
        }

        public Tween GetAttributeAnimation(AttributeType attributeType)
        {
            foreach (CardIntAttributeView item in _attributeViews)
            {
                if (item.Type == attributeType) return item.CurrentAnimation;
            }
            
            Debug.LogError("Can't find attribute view!");
            return null;
        }

        public void Glow(bool isActive)
        {
            _cardBackground.material = isActive ?_shinyMaterial:_defaultMaterial;
        }

        private void KillTweens()
        {
            foreach (var item in _tweens)
            {
                item.Kill(false);
            }
            
            _tweens.Clear();
        }

        private bool IsAllTweensComplete()
        {
            foreach (var item in _tweens)
            {
                if (!IsTweenComplete(item)) return false;
            }

            return true;
        }

        private bool IsAttributeAnimationComplete()
        {
            foreach (var item in _attributeViews)
            {
                if (!item.IsAnimationComplete) return false;
            }

            return true;
        }

        private bool IsTweenComplete(Tween tween)
        {
            return tween == null || !tween.active || tween.IsComplete();
        }
    }
}