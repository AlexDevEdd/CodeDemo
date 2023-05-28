using System;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Systems.Input;
using _Game.Scripts.View.Base;
using UnityEngine;

namespace _Game.Scripts.Buildings
{
    [ExecuteInEditMode]
    public class BaseBuildingView : BaseView
    {
        public Action<BaseBuildingView> UnlockEvent;
        
        [SerializeField] private int _buildingId;
        [SerializeField] private bool _unlockBuilding;

        [Header("VisualSettings")]
        [SerializeField] private float _camFocusScale = 4f;
        [SerializeField] private Vector3 _camFocusOffset = new Vector3(0, 0, 0);

        protected bool _isUnlocked;
        
        public int BuildingId => _buildingId;
        public BuildingItemConfig Config { get; private set; }
        public float CamFocusScale => _camFocusScale;
        public Vector3 CamFocusOffset => _camFocusOffset;
        public bool IsUnlocked => _isUnlocked;

        public override void Init()
        {
            if (_unlockBuilding)
            {
                _isUnlocked = true;
            }
            else
            {
                var colliders = GetComponentsInChildren<ButtonCollider>();
                foreach (var button in colliders)
                {
                    button.Init(this);
                    button.OnClick += OnClicked;
                }
            }

            base.Init();
        }
        
        private void OnClicked(ButtonCollider button)
        {
            ResultClickBuilding(button);
        }

        protected virtual void ResultClickBuilding(ButtonCollider button)
        {
        }

        public virtual void SetConfig(BuildingItemConfig config)
        {
            Config = config;
        }

        public virtual void Unlock()
        {
            _isUnlocked = true;
            UnlockEvent?.Invoke(this);
        }

        public virtual void SetData(bool unlocked)
        {
            if (unlocked) Unlock();
        }
    }
}
