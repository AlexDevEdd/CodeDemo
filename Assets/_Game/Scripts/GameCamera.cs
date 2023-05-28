using System;
using System.Linq;
using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Systems.Input;
using _Game.Scripts.Ui.Base;
using _Game.Scripts.View;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Game.Scripts
{
    public class GameCamera : MonoBehaviour, IInitSceneObjects, ITickableProjectSystem
    {
        public enum RenderType
        {
            Default,
            UI
        }

        [Serializable]
        private class CameraConfig
        {
            public float Value;
            public float Duration;
        }

        public Action OnCamFocused;        
        public Action OnStartCamFocused;        
        
        [SerializeField] private float _minCamSize;
        [SerializeField] private float _maxCamSize;
        [SerializeField] private float _defaultCamSize;
        [SerializeField] private float _startCamSize;
        [SerializeField] private CameraConfig[] _distanceConfigs;
        [SerializeField] private CameraConfig[] _scaleConfigs;

        [Inject] private LevelSystem _levels;
        [Inject] private InputSystem _input;
        [Inject] private GameFlags _gameFlags;

        private static Camera _unityCam;

        private const float ZOOM_STEP = 0.05f;
        private const float MOVE_LERP_SPEED = 0.6f;

        private Tweener _tweenFocus;
        private Tween _tweenScale;
        private Transform _target;
        private float _lastCamSize;

        private LevelViewConfig _config;

        public bool IsFocus => _tweenFocus != null;

        public void Init()
        {
            _unityCam = GetComponent<Camera>();
        }

        public void InitSceneObjects()
        {
            //MOCK
        }

        public void Tick(float deltaTime)
        {
            if (_target != null)
            {
                MoveTo(_target.transform.position);
            }
        }

        public void SetCameraSize(float value)
        {
            _unityCam.orthographicSize = value;
        }

        public void SetDefaultScale()
        {
            SetScale(_defaultCamSize);
        }
        
        public void SetLastScale()
        {
            SetScale(_lastCamSize);
        }

        public void FocusWithLastScale(Vector3 position)
        {
            Focus(position, _lastCamSize);
        }

        private float GetScaleDuration(float scale)
        {
            var delta = _unityCam.orthographicSize - scale;
            var config = _scaleConfigs.LastOrDefault(c => c.Value <= delta);
            if (config != null)
            {
                return config.Duration == 0 ? 0.35f : config.Duration;
            }
            return 0.35f;
        }

        private void SetScale(float value)
        {
            if (value == 0) value = _defaultCamSize;
            _tweenScale?.Kill();
            _tweenScale = _unityCam
                .DOOrthoSize(value, GetScaleDuration(value))
                .SetEase(Ease.Linear);
        }
        
        public void Zoom(bool inc, float zoomStep = 0)
        {
            var zoom = zoomStep > 0 ? zoomStep : ZOOM_STEP;
            var size = Mathf.Clamp(_unityCam.orthographicSize + (inc ? zoom : -zoom),
                _minCamSize,
                _maxCamSize);

            _unityCam.orthographicSize = size;
        }
        
        public void Zoom(float delta)
        {
            var size = Mathf.Clamp(_unityCam.orthographicSize + delta * ZOOM_STEP,
                _minCamSize,
                _maxCamSize);

            _unityCam.orthographicSize = size;
        }

        public bool MoveAnim(Vector3 delta)
        {
            var localPosition = transform.position;
            
            var deltaPos = 
                new Vector3(localPosition.x + delta.x,
                    localPosition.y,
                    localPosition.z + delta.z);

            bool isBounded = false;
            var newPosition = GetBoundedAnim(deltaPos, ref isBounded);

            var speed = MOVE_LERP_SPEED;
            if (isBounded)
            {
                speed = MOVE_LERP_SPEED / 4;
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, speed);
            
            return (deltaPos - newPosition).magnitude > float.Epsilon;
        }

        public bool Move(Vector3 delta)
        {
            var localPosition = transform.position;
            var deltaPos = new Vector3(localPosition.x + delta.x,
                localPosition.y,
                localPosition.z + delta.z);

            var newPosition = GetBounded(deltaPos);
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, MOVE_LERP_SPEED);
            return (deltaPos - newPosition).magnitude > float.Epsilon;
        }

        public bool MoveTo(Vector3 position)
        {
            var newPosition = GetBounded(position);
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, MOVE_LERP_SPEED);
            return (position - newPosition).magnitude > float.Epsilon;
        }

        private Vector3 GetBoundedAnim(Vector3 position,ref bool isBounded)
        {
            isBounded = false;
            if (_config == null)
                return position;

            if (position.x < _config.BoundsMin.x + _config.DeltaBounds)
            {
                position.x = _config.BoundsMin.x + _config.DeltaBounds;
                isBounded = true;
            }

            if (position.x > _config.BoundsMax.x - _config.DeltaBounds)
            {
                position.x = _config.BoundsMax.x - _config.DeltaBounds;
                isBounded = true;
            }
            
            if (position.z < _config.BoundsMin.z + _config.DeltaBounds)
            {
                position.z = _config.BoundsMin.z + _config.DeltaBounds;
                isBounded = true;
            }
            
            if (position.z > _config.BoundsMax.z - _config.DeltaBounds)
            {
                position.z = _config.BoundsMax.z - _config.DeltaBounds;
                isBounded = true;
            }

            return position;
        }

        private Vector3 GetBounded(Vector3 position)
        {
            if (_config == null) return position;
            if (position.x < _config.BoundsMin.x) position.x = _config.BoundsMin.x;
            if (position.x > _config.BoundsMax.x) position.x = _config.BoundsMax.x;

            if (position.z < _config.BoundsMin.z) position.z = _config.BoundsMin.z;
            if (position.z > _config.BoundsMax.z) position.z = _config.BoundsMax.z;

            return position;
        }

        public Ray GetRay(Vector3 pos = default)
        {
            return _unityCam.ScreenPointToRay(pos == default ? Input.mousePosition : pos);
        }

        private float GetMoveDuration(Vector3 target)
        {
            var delta = (transform.position - target).magnitude;
            var config = _distanceConfigs.LastOrDefault(c => c.Value <= delta);
            if (config != null)
            {
                return config.Duration == 0 ? 0.3f : config.Duration;
            }
            return 0.5f;
        }

        public void Focus(Vector3 pos, float scale = 0f, bool isAction = false, bool saveSize = false, Action callback = null)
        {
            if (saveSize)
            {
                _lastCamSize = _unityCam.orthographicSize;
                _lastCamSize = Mathf.Clamp(_lastCamSize, _minCamSize, _maxCamSize);
            }

            _input.StopScroll();
            _target = null;

            if (scale == 0) scale = _defaultCamSize;
            var scaleDuration = GetScaleDuration(scale);
            var moveDuration = GetMoveDuration(pos);
            var duration = MathF.Min(scaleDuration, moveDuration);
            
            _tweenScale?.Kill();
            _tweenScale = _unityCam
                .DOOrthoSize(scale, duration)
                .SetEase(Ease.Linear);
            if (callback != null) _tweenScale.OnComplete(() => callback());
            
            _tweenFocus?.Kill();
            if (isAction)
            {
                _tweenFocus = transform.DOMove(pos, duration)
                    .SetEase(Ease.Linear)
                    .OnComplete(OnCameraFocused);
            }
            else
            {
                _tweenFocus = transform.DOMove(pos, duration)
                    .SetEase(Ease.Linear)
                    .OnComplete(KillFollow);
            }
        }

        private void KillFollow()
        {
            _tweenFocus?.Kill();
            _tweenFocus = null;
            _target = null;
        }

        private void OnCameraFocused()
        {
            AppEventProvider.TriggerEvent(AppEventType.Tutorial, GameEvents.CameraFocused);
            OnCamFocused?.Invoke();
            _tweenFocus?.Kill();
            _tweenFocus = null;
        }

        public void FollowObject(Transform target)
        {
            _tweenFocus?.Kill();
            _input.StopScroll();
            _tweenFocus = transform.DOMove(target.transform.position, GetMoveDuration(target.position))
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _target = target;
                });
            _tweenFocus.OnUpdate(delegate
            {
                if (Vector3.Distance(target.position, target.position) > 0.01f)
                {
                    _tweenFocus.ChangeEndValue(target.position, true);
                }
            });
        }

        public void RemoveTarget()
        {
            KillFollow();
        }

        public void SetRenderType(RenderType type)
        {
            switch (type)
            {
                case RenderType.UI:
#if UNITY_EDITOR
                    _unityCam.enabled = true;
                    break;
#endif
                    _unityCam.enabled = false;
                    break;
                default:
                    _unityCam.enabled = true;
                    break;
            }
        }
    }
}