using System;
using _Game.Scripts.View.Base;
using UnityEngine;

namespace _Game.Scripts.Systems.Input
{
    public class ButtonCollider : BaseView
    {
        public event Action<ButtonCollider> OnClick;
        
        [SerializeField] private int _id;
        
        private BaseView _parentView;
        private static BaseView _allowedTutorialView;
        private static bool _blockAllInput;
        
        public int Id => _id;

        public void Init(BaseView parentView)
        {
            _parentView = parentView;
        }
        
        public void ClickCollider()
        {
            if(_blockAllInput && _allowedTutorialView == null) return;
            if (_allowedTutorialView != null)
            {
                if (_allowedTutorialView != _parentView) return;
                _allowedTutorialView = null;
            }
            OnClick?.Invoke(this);
        }
        
        public static void SetAllowedView(BaseView view)
        {
            _allowedTutorialView = view;
        }

        public static void BlockAll()
        {
            _blockAllInput = true;
        }

        public static void UnlockAll()
        {
            _blockAllInput = false;
        }
    }
}