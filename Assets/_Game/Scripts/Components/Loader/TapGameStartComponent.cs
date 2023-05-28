using _Game.Scripts.Components.Base;
using _Game.Scripts.Systems.Base;
using Zenject;

namespace _Game.Scripts.Components.Loader
{
    public class TapGameStartComponent : BaseComponent
    {
        [Inject] private GameSystem _game;
        
        public override void Start()
        {
            _game.LoadedGame();
            End();
        }
    }
}