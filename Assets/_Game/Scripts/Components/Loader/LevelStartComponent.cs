using _Game.Scripts.Components.Base;
using _Game.Scripts.Systems;
using _Game.Scripts.Systems.Base;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    /// <summary>
    /// Класс, запускает необходимые активности в момент старта нового уровня.
    /// </summary>
    public class LevelStartComponent : BaseComponent
    {
        [Inject] private SoundSystem _soundSystem;
        [Inject] private GameSystem _game;
        
        public override void Start()
        {
            _game.LoadedGame();
            _soundSystem.PlayMusic(GameSoundType.Music);
            End();
        }
    }
}