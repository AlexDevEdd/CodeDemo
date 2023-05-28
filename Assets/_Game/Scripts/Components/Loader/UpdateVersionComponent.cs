using _Game.Scripts.Components.Base;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Ui;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    /// <summary>
    /// Класс для проверки версии игры
    /// </summary>
    public class UpdateVersionComponent : BaseComponent
    {
        [Inject] private ProjectSettings _settings;
        [Inject] private WindowsSystem _windows;
        
        public override void Start()
        {
            if (!_settings.CheckVersion)
            {
                End();
                return;
            }
#if UNITY_EDITOR
            End();
            return;
#endif
            
            End();
        }
    }
}