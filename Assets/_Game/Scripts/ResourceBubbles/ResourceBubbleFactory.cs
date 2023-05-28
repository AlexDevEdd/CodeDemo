using System.Collections.Generic;
using System.Globalization;
using _Game.Scripts.Enums;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Ui.Base;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.ResourceBubbles
{
    public class ResourceBubbleFactory
    {
        [Inject] private LevelUI _ui;
        [Inject] private WindowsSystem _windows;
        [Inject] private ResourceBubbleConfigs _configs;

        private readonly ResourceBubbleUI.Pool _resourceBubblesPool;
        private readonly List<ResourceBubbleUI> _resourceBubbles = new();
        
        public ResourceBubbleFactory(ResourceBubbleUI.Pool resourceBubblesPool)
        {
            _resourceBubblesPool = resourceBubblesPool;
        }
        
        public virtual void ResetBeforeLoadLevel()
        {
            foreach (var bubble in _resourceBubbles)
            {
                bubble.Clear();
                bubble.OnFinished -= RemoveResourceBubble;
                _resourceBubblesPool.Despawn(bubble);
            }
            _resourceBubbles.Clear();
        } 
        
        public void SpawnResourceBubble(GameParamType gameParam,
            float count = -1,
            Vector3 pos = default,
            Vector2 target = default,
            Sprite spriteIcon = null,
            bool showText = false,
            bool fade = false,
            int randomOffset = 100)
        {
            if (count == 0) return;
            
            var type = GetBubbleType(gameParam);
            var setup = _configs.Configs.Find(s => s.Type == type);
            if (setup == null) return;
            
            var startPos = pos == default ? Input.mousePosition : pos;
            Vector3 targetPos;
            if (target == default)
            {
                var targetElement = _windows.GetGamePlayElement(type);
                if(!targetElement) return;
                targetPos = targetElement.GetComponent<RectTransform>().position;
            }
            else
            {
                targetPos = target;
            }
            
            var countBubbles = Mathf.Clamp((int) (count / setup.Conversion), 1, setup.Max);
            
            var isFirst = true;
            var delay = 0f;
            const float animDelay = 0.01f;
            //var animationElement = targetElement as CurrencyGamePlayElement;
            while (countBubbles-- > 0)
            {
                var icon = spriteIcon == null ? setup.Icon : spriteIcon;
                var bubble = _resourceBubblesPool.Spawn(_ui.transform, startPos, targetPos, icon, delay);
                bubble.SetRandomOffsetPosition(randomOffset);
                bubble.OnFinished += RemoveResourceBubble;
                if (showText) bubble.SetText(count.ToString(CultureInfo.InvariantCulture));
                //if (animationElement) bubble.OnFinished += animationElement.AnimateGetResource;
                _resourceBubbles.Add(bubble);
                
                delay += animDelay;
            
                if (!isFirst) continue;
                isFirst = false;
            }
        }

        private GamePlayElement GetBubbleType(GameParamType gameParam)
        {
            switch (gameParam)
            {
                case GameParamType.Soft:
                    return GamePlayElement.Soft;
                case GameParamType.Tokens:
                    return GamePlayElement.Tokens;
                case GameParamType.Hard:
                    return GamePlayElement.Hard;
                case GameParamType.Prestige:
                    return GamePlayElement.Prestige;
                case GameParamType.AdTickets:
                    return GamePlayElement.AdTickets;
                default:
                    return GamePlayElement.None;
            }
        }

        private void RemoveResourceBubble(ResourceBubbleUI bubble)
        {
            bubble.OnFinished -= RemoveResourceBubble;
            _resourceBubblesPool.Despawn(bubble);
            _resourceBubbles.Remove(bubble);
        }
    }
}