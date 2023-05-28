using System.Collections.Generic;
using _Game.Scripts.Enums;
using _Game.Scripts.ScriptableObjects;

namespace _Game.Scripts.Systems.Analytics
{
    /// <summary>
    /// Класс для интеграции с SDK аналитик
    /// </summary>
    public class AnalyticsSystem
    {
        private readonly ProjectSettings _settings;
        private readonly List<IAnalyticsWrapper> _analyticsWrappers;
        
        public AnalyticsSystem(ProjectSettings settings)
        {
            _settings = settings;
            if (!_settings.Analytics)
            {
                return;
            }
            
            _analyticsWrappers = new List<IAnalyticsWrapper>
            {
                new GameAnalyticsWrapper(),
                new FirebaseWrapper()
            };
        }

        public void Connect()
        {
            if (!_settings.Analytics)
            {
                return;
            }
            
            foreach (var wrapper in _analyticsWrappers)
            {
                wrapper.Init(_settings);
            }
        }

        public void SendEvent(GameEvents gameEvent, params object[] list)
        {
            if (!_settings.Analytics) return;
            foreach (var wrapper in _analyticsWrappers)
            {
                wrapper.SendEvent(gameEvent, list);
            }
        }
    }
}