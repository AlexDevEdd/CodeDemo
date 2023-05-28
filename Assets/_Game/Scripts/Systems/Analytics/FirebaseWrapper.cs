using _Game.Scripts.Enums;
using _Game.Scripts.ScriptableObjects;

namespace _Game.Scripts.Systems.Analytics
{
    public class FirebaseWrapper : IAnalyticsWrapper
    {
        public IAnalyticsWrapper Init(ProjectSettings settings)
        {
            // Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            //     var dependencyStatus = task.Result;
            //     if (dependencyStatus == Firebase.DependencyStatus.Available)
            //     {
            //         var app = Firebase.FirebaseApp.DefaultInstance;
            //     }
            // });
            
            return this;
        }

        public void SendEvent(GameEvents eventType, params object[] parameters)
        {
            
        }
    }
}