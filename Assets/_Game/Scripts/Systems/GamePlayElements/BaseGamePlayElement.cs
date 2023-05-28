using _Game.Scripts.Enums;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using _Game.Scripts.Ui.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Systems.GamePlayElements
{
    public class BaseGamePlayElement : BaseUIView
    {
        [SerializeField] private bool _alwaysVisible;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _back;
        [SerializeField] private GamePlayElement _type;
        [SerializeField] private BaseButton _button;
        
        protected WindowsSystem Windows;
        protected GameBalanceConfigs Balance; 
        
        private BlindEffect _blindEffect;
        private Transform _parent;
        private RectTransform _rect;
        private Vector2 _defaultPos;
        private Color _defaultColor;

        public bool AlwaysVisible => _alwaysVisible;
        public BaseButton Button => _button;
        public GamePlayElement Type => _type;
        public RectTransform Rect => _rect;

        public virtual void Init(WindowsSystem windows, GameBalanceConfigs balance)
        {
           //MOCK
        }

        protected void SetType(GamePlayElement type)
        {
            _type = type;
        }

        public void Blind(bool activate)
        {
            if(!_blindEffect) return;
            _blindEffect.SetActive(activate);
        }

        public virtual void RestorePosition()
        {
            transform.SetParent(_parent);
            _rect.anchoredPosition = _defaultPos;
        }

        protected virtual void OnPressedButton()
        {
        }

        public virtual void SetActive(bool value, Material material = null, bool hide = false)
        {
            if (hide) gameObject.SetActive(value);
            if (material != null)
            {
                if (_icon) _icon.material = material;
                if (_back) _back.color = material.color;
            }
            else
            {
                if (_icon) _icon.material = null;
                if (_back) _back.color = _defaultColor;
            }
        }

        public virtual void Deactivate()
        {
            gameObject.Deactivate();
        }

        public virtual void UpdateLocalization()
        {
        }
    }
}
