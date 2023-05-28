using System;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using _Game.Scripts.View.Base;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Systems.Input
{
    public class InputSystem : ITickableProjectSystem
    {
        public static event Action<int> ClickSoundEvent = delegate { };
        
        private readonly GameCamera _camera;
        private readonly WindowsSystem _windows;

        [Inject] private GameBalanceConfigs _balance;
        
        private BaseView _allowedToTouch;
        
        private float _prevTouchTime;
        private Vector3? _prevTouchPos;
        private Vector3? _targetTouchPos;

        private float _k;
        private const float DELTA_TOUCH = 0.1f;
        private const float MAX_SCROLL_K = 0.6f;
        private const float ZOOM_SENSETIVITY = 0.12f;
        private const float HOLD_DELAY = 0.2f;
        private const float STOP_SCROLL_LIMIT = 0.03f;

        private bool _zoom;
        private bool _touching;
        private bool _scroll;
        
        private bool _ignoreDrag;
        private bool _ignoreTaps;

        private const int SECOND_MODEL_SELECTION = 1 << 6,
                          GROUND = 1 << 7,
                          FIRST_MODEL_SELECTION = 1 << 10;

        public InputSystem(GameCamera camera, WindowsSystem windows)
        {
            _camera = camera;
            _windows = windows;
            windows.AnyWindowOpened += OnAnyWindowOpened;
            _k = MAX_SCROLL_K;
            BaseButton.AnyButtonClickedEvent += OnPressed;
        }

        private void OnPressed(BaseButton button)
        {
            if (_scroll) StopScroll();
        }

        private void OnAnyWindowOpened()
        {
            return;
            StopScroll();
            ClearAllFlags();
        }

        public void Tick(float deltaTime)
        {
            //MOCK
        }
        
        private void CheckZoom()
        {
            //MOCK
        }
        
        private void CheckDrag(float deltaTime)
        {
            //MOCK
        }

        private void ClearAllFlags()
        {
            _touching = false;
            _zoom = false;
            _scroll = false;
        }

        private Vector3? GetWorldTouchPos()
        {
            var screenTouchPos = UnityEngine.Input.mousePosition;
            var ray = _camera.GetRay(screenTouchPos);

            return Physics.Raycast(ray, out var hit, float.MaxValue, GROUND)
                ? hit.point
                : null;
        }
        
        private void CheckMouseDown()
        {
            //MOCK
        }
        
        private void CheckMouseHold()
        {
        }
        
        public void StopScroll()
        {
            _targetTouchPos = null;
            _prevTouchPos = null;
            _k = 0;
        }

        public void BlockScroll()
        {
            ClearAllFlags();
            _ignoreDrag = true;
        }

        public void BlockTouch()
        {
            ClearAllFlags();
            _ignoreTaps = true;
        }
        
        public void UnblockTouch()
        {
            _ignoreTaps = false;
        }

        public void UnblockInput()
        {
            _ignoreDrag = false;
            _ignoreTaps = false;
            StopScroll();
        }

        public void SetAllowedToTouch(BaseView view)
        {
            ClearAllFlags();
            _allowedToTouch = view;
            _ignoreTaps = false;
        }

        public void BlockAll()
        {
            _ignoreDrag = true;
            _ignoreTaps = true;
        }
    }
}