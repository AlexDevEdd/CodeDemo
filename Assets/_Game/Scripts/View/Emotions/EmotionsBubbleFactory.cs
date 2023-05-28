using System.Collections.Generic;
using _Game.Scripts.Interfaces;
using UnityEngine;

namespace _Game.Scripts.View.Emotions
{
    public class EmotionsBubbleFactory : ITickableSystem
    {
        private readonly EmotionBubbleView.Pool _pool;
        private readonly List<EmotionBubbleView> _bubbles = new();
        
        public EmotionsBubbleFactory(EmotionBubbleView.Pool pool)
        {
            _pool = pool;
        }
        
        public void SpawnEmotion(string token, Transform parent)
        {
            var textView = _pool.Spawn(token, parent);
            textView.SetCallback(Remove);
            _bubbles.Add(textView);
        }
        
        private void Remove(EmotionBubbleView text)
        {
            _pool.Despawn(text);
            _bubbles.Remove(text);
        }

        public void Tick(float deltaTime)
        {
            foreach (var bubble in _bubbles)
            {
                bubble.Tick();
            }
        }
    }
}