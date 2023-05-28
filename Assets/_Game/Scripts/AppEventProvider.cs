using _Game.Scripts.Enums;

namespace _Game.Scripts
{
    /// <summary>
    /// Класс для передачи событий между классами 
    /// </summary>
    public class AppEventProvider
    {
        // private static ProjectSettings _settings;
        // private static AnalyticsSystem _analytics;
        // private static GameTaskSystem _tasks;
        // private static ITutorial _tutorial;
        //
        // public AppEventProvider(ProjectSettings settings, AnalyticsSystem analytics, GameTaskSystem tasks, ITutorial tutorial)
        // {
        //     // _settings = settings;
        //     // _analytics = analytics;
        //     // _tasks = tasks;
        //     // _tutorial = tutorial;
        // }
        
        public static void TriggerEvent(AppEventType type, GameEvents gameEvent, params object[] list)
        {
            //MOCK
        }
        
        public static void TriggerEvent(GameEvents gameEvent, params object[] list)
        {
            //MOCK
        }
    }
}