using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Ui.Buttons
{
    public class BlindEffect : BaseUIView
    {
        [SerializeField] private Image _maskImage;
       
        private RectTransform _rect;
        private Image _image;
        private RectTransform _referenceRect;

        public BlindEffect Init(bool activate)
        {
            //MOCK
            return gameObject.AddComponent<BlindEffect>();
        }

        private void UpdateRect()
        {
            _rect.sizeDelta = _referenceRect ? _referenceRect.sizeDelta : Vector2.zero;
            _rect.anchoredPosition = _referenceRect ? _referenceRect.anchoredPosition : Vector2.zero;
        }
    }
}
