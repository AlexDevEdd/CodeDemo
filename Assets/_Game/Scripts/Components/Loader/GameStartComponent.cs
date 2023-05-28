using _Game.Scripts.Components.Base;
using _Game.Scripts.Systems;
using _Game.Scripts.Systems.Base;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    /// <summary>
    /// Класс, запускает необходимые активности в момент старта игры.
    /// </summary>
    public class GameStartComponent : BaseComponent
    {
        [Inject] private SoundSystem _soundSystem;
        [Inject] private GameSystem _game;
        [Inject] private WindowsSystem _windows;

        public override void Start()
        {
            _game.LoadedGame();
            _soundSystem.PlayMusic(GameSoundType.Music);
            //_windows.OpenWindow<ReturnToGameWindow>();
            End();
        }
    }
}