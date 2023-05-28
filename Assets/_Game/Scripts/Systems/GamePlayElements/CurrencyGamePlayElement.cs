using _Game.Scripts.ResourceBubbles;
using _Game.Scripts.Ui;
using Coffee.UIExtensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Systems.GamePlayElements
{
    public class CurrencyGamePlayElement : BaseGamePlayElement
    {
        [SerializeField] private RectTransform _animatingRect;
        [SerializeField] private Image _blind;
        [SerializeField] private ParticleSystem _particle;

        private Tween _tweenColor;
        private Tween _tweenScale;

        public void AnimateGetResource(ResourceBubbleUI bubble)
        {
           //MOCK
        }
    }
}
