using _Game.Scripts.Components.Base;
using _Game.Scripts.Systems.Ads;
using _Game.Scripts.Systems.Save;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    /// <summary>
    /// Класс для инициализации систем перед загрузкой игры
    /// </summary>
    public class InitGameSystemsComponent : BaseComponent
    {
        [Inject] private SaveSystem _save;
        [Inject] private AdSystem _adSystem;
        [Inject] private GameCamera _camera;

        public override void Start()
        {
            _save.Init();
            _adSystem.Init();
            _camera.Init();

            base.Start();
            End();
        }
    }
}