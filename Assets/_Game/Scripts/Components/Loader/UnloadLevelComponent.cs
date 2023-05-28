using _Game.Scripts.Components.Base;
using _Game.Scripts.Systems.Base;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    public class UnloadLevelComponent : BaseComponent
    {
        [Inject] private LevelSystem _levels;

        public override void Start()
        {
            _levels.OnUnloadedLevel += OnUnloadedLevel;
            _levels.UnloadLevel();
            base.Start();
        }

        private void OnUnloadedLevel()
        {
            _levels.OnUnloadedLevel -= OnUnloadedLevel;
            End();
        }
    }
}