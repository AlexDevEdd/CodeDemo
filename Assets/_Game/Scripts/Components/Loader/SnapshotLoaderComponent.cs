using _Game.Scripts.Components.Base;
using _Game.Scripts.Systems.Save;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    /// <summary>
    /// Класс для загрузки снапшота в момент загрузки игры
    /// </summary>
    public class SnapshotLoaderComponent : BaseComponent
    {
        [Inject] private SaveSystem _save;
        
        public override void Start()
        {
            _save.LoadSnapshot();
            End();
            base.Start();
        }
    }
}