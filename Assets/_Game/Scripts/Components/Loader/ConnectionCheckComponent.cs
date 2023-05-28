using _Game.Scripts.Components.Base;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Systems;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Ui;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    /// <summary>
    /// Класс для проверки интернета на этапе экрана загрузки
    /// </summary>
    public class ConnectionCheckComponent : BaseComponent
    {
        [Inject] private ProjectSettings _settings;
        [Inject] private ConnectionSystem _connection;
        [Inject] private WindowsSystem _windows;
        
        public override void Start()
        {
            if (!_settings.Internet)
            {
                End();
                return;
            }
            
#if UNITY_EDITOR
            End();
            return;
#endif
            _connection.Connected += OnConnected;
            _connection.RunCheckConnection();
            base.Start();
        }

        private void OnConnected(bool success)
        {
            if (!success)
            {
                _windows.OpenWindow<NoConnectionWindow>();
                return;
            }
            _connection.Connected -= OnConnected;
            End();
        }
    }
}