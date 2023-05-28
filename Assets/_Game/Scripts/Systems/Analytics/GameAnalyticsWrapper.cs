using _Game.Scripts.Enums;
using _Game.Scripts.ScriptableObjects;

namespace _Game.Scripts.Systems.Analytics
{
    public class GameAnalyticsWrapper : IAnalyticsWrapper
    {
        public IAnalyticsWrapper Init(ProjectSettings settings)
        {
            //Init GA
            return this;
        }

        public void SendEvent(GameEvents eventType, params object[] parameters)
        {
            
        }
    }
}