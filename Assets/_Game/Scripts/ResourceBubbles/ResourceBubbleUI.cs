using System;
using _Game.Scripts.Ui.Base;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _Game.Scripts.ResourceBubbles
{
    public class ResourceBubbleUI : BaseUIView
    {
        private enum Phase
        {
            First,
            Second
        }

        public Action<ResourceBubbleUI> OnFinished;
        
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _text;
        [Range(0f, 1f), SerializeField] private float _firstPhaseDuration = 0.2f;
        [Range(0f, 1f), SerializeField] private float _secondPhaseDelay = 0.2f;
        [Range(0f, 1f), SerializeField] private float _secondPhaseDuration = 0.2f;

        private RectTransform _rect;
        private Vector2 _startMovePos, 
                        _middlePos, 
                        _targetPos,
                        _delta;
        
        private Phase _phase;
        private Tweener _tween;
        private float _progress;

        public class Pool : MonoMemoryPool<Transform, Vector3, Vector3, Sprite, float, ResourceBubbleUI>
        {
            protected override void Reinitialize(Transform parent, 
                Vector3 startPos, 
                Vector3 targetPos, 
                Sprite icon, 
                float delay, 
                ResourceBubbleUI resourceBubbleUI)
            {
                resourceBubbleUI.SetConfig(parent, startPos, targetPos, icon, delay);
            }
        }

        private void SetConfig(Transform parent, Vector3 startPos, Vector3 targetPos, Sprite icon, float delay)
        {
            transform.SetParent(parent);
            
            _phase = Phase.First;
            
            _startMovePos = _middlePos = startPos;
            _targetPos = targetPos;

            _middlePos = Vector2.zero;
            _delta = Vector2.zero;

            _icon.sprite = icon;
            _text.text = "";

            _rect = GetComponent<RectTransform>();
            _rect.position = _startMovePos;
            _rect.localScale = Vector3.one;
            
            StartTween(_firstPhaseDuration, delay);
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetRandomOffsetPosition(int value = 100)
        {
            _middlePos = _startMovePos + Random.insideUnitCircle * value;
            _delta = _middlePos - _startMovePos;
        }

        private void StartTween(float duration, float delay = 0f)
        {
            _progress = 0f;
            _tween.Kill();
            _tween = DOTween.To(() => _progress, v => _progress = v, 1, duration)
                .SetDelay(delay)
                .SetEase(_phase == Phase.First ? Ease.OutCubic : Ease.InCubic)
                .SetUpdate(UpdateType.Manual)
                .OnUpdate(OnUpdate)
                .OnComplete(OnTweenPlayed);
        }

        private void OnUpdate()
        {
            var pos = _startMovePos + _progress * _delta;
            _rect.position = pos;
        }
        
        private void OnTweenPlayed()
        {
            if (_phase == Phase.Second)
            {
                Hide();
                return;
            }
            
            _phase = Phase.Second;

            _startMovePos = _middlePos;
            _delta = _targetPos - _startMovePos;

            StartTween(_secondPhaseDuration, _secondPhaseDelay);
        }

        private void Hide()
        {
            _tween?.Kill();
            OnFinished?.Invoke(this);
        }

        public void Clear()
        {
            _tween?.Kill();
        }
    }
}