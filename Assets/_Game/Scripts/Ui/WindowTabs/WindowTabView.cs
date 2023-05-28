using System;
using _Game.Scripts.Ui.Base;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Ui.WindowTabs
{    
    public enum WindowTabType
    {
        None
    }
    
    public class WindowTabView : BaseButton
    {
        public Action<WindowTabType> OnPressedTab;
        
        [SerializeField] private WindowTabType _type;
        [SerializeField] private GameObject _container;
        [SerializeField] private RectTransform _tabImage;

        private BaseWindow _window;
        private Sequence _tween;

        public WindowTabType Type => _type;
        public GameObject Container => _container;
        public bool IsSelected { get; private set; }

        public void Init()
        {
            //MOCK
        }

        private void OnPressed()
        {
            //MOCK
        }

        public void Select(bool value)
        {
            //MOCK
        }
    }
}
