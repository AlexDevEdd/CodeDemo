using _Game.Scripts.Components.Base;
using _Game.Scripts.Systems.Save;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    /// <summary>
    /// Класс для загрузки сейвов в момент загрузки игры
    /// </summary>
    public class SaveDataLoaderComponent : BaseComponent
    {
        [Inject] private SaveSystem _save;
        
        public override void Start()
        {
            _save.LoadData();
            End();
            base.Start();
        }
    }
}