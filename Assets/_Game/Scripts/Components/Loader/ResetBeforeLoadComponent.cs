using _Game.Scripts.Components.Base;
using _Game.Scripts.Core;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Systems.Input;
using _Game.Scripts.Systems.Save;
using DG.Tweening;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    /// <summary>
    /// Класс для обнуления значений при переходе между сценами
    /// </summary>
    public class ResetBeforeLoadComponent : BaseComponent
    {
        [Inject] private SaveSystem _save;
        [Inject] private LevelSystem _levelSystem;

        public override void Start()
        {
            _save.Save();
            _save.SetActive(false);

            InvokeSystem.Clear();
            DOTween.KillAll();
            
            var installer = _levelSystem.SceneRoot.GetComponentInChildren<MainGameLevelInstaller>();
            installer.ResetGameLogic();
            
            InvokeSystem.StartExternalInvoke(End, 1);
        }
    }
}
