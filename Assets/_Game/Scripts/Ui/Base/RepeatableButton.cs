using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Scripts.Ui.Base
{
    public class RepeatableButton : BaseButton
    {
        [Header("Repeatable Settings")]
        [SerializeField] private float _repeatInterval = 0.2f;

        private bool _pressed;
        private float _timer;
        
        protected override void OnDisable()
        {
            _timer = 0;
            base.OnDisable();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            base.OnPointerDown(eventData);
        }

        public override void Tick(float deltaTime)
        {
            if (!_pressed) return;
            _timer += deltaTime;
            if (_timer >= _repeatInterval)
            {
                SimulateClick();
                _timer = 0;
            }
            base.Tick(deltaTime);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
            _timer = 0;
            base.OnPointerUp(eventData);
        }
    }
}