using _Game.Scripts.Ui.Base;
using Coffee.UIExtensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.View.Fx
{
    public class ChangeProgressBlind : BaseUIView
    {
        private Image _image;
        private Color _color;
        private UIParticle _uiParticle;
        private Tween _tween;

        public void Init()
        {
            _image = GetComponent<Image>();
            _color = _image.color;
            _uiParticle = GetComponentInChildren<UIParticle>();
        }

        public void Play()
        {
            if(!_image) return;
            _tween?.Kill();
            _image.color = Color.white;
            _tween = _image.DOColor(_color, 0.5f);
            if (!_uiParticle) return;
            UpdateParticlePosition();
            _uiParticle.Play();
        }

        private void UpdateParticlePosition()
        {
            if (_image.type != Image.Type.Filled) return;
            var pos = new Vector2 (_image.fillAmount, 0.5f);
            _uiParticle.rectTransform.anchorMin = pos;
            _uiParticle.rectTransform.anchorMax = pos;
            _uiParticle.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
