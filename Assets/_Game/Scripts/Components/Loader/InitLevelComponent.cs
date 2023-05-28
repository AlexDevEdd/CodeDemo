using _Game.Scripts.Components.Base;
using _Game.Scripts.Core;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Systems.Save;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    /// <summary>
    /// Класс используется для инициализации объектов на уровне сцены
    /// </summary>
    public class InitLevelComponent : BaseComponent
    {
        [Inject] private LevelSystem _levelSystem;
        [Inject] private SaveSystem _saveSystem;
        
        public override void Start()
        {
            var logic = _levelSystem.SceneRoot.GetComponentInChildren<BaseLevelInstaller>();
            logic.InitGameLogic();
            logic.LoadSave(false);
            logic.InitLevelUI();
            logic.InitSceneObjects();
            
            _saveSystem.SetActive(true);
            _saveSystem.Save();
            base.Start();
            End();
        }
    }
}