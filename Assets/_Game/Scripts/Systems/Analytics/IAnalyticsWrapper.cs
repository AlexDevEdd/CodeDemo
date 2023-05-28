using System;
using _Game.Scripts.Enums;
using _Game.Scripts.ScriptableObjects;

namespace _Game.Scripts.Systems.Analytics
{
    public interface IAnalyticsWrapper
    {
        public IAnalyticsWrapper Init(ProjectSettings settings);
        public void SendEvent(GameEvents eventType, params object[] parameters);
    }
}