using System;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using _Game.Scripts.View.Other;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.View.Emotions
{
    public class EmotionBubbleView : BaseUIView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Animation _animation;
        
        [Inject] private WorldSpaceCanvas _worldSpaceCanvas;
        [Inject] private GameResources _gameResources;
        
        private Action<EmotionBubbleView> _callback;
        private Transform _target;
        
        public class Pool : MonoMemoryPool<string, Transform, EmotionBubbleView>
        {
            protected override void Reinitialize(string token, Transform parent, EmotionBubbleView bubble)
            {
                bubble.Init(token, parent);
            }
        }

        private void Init(string token, Transform parent)
        {
            //MOCK
        }

        private void Hide()
        {
            _callback?.Invoke(this);
        }

        public void SetCallback(Action<EmotionBubbleView> callback)
        {
            _callback = callback;
        }

        public void Tick()
        {
            RectTransform.position = new Vector3(_target.transform.position.x, 
                transform.position.y, 
                _target.transform.position.z);
        }
    }
}