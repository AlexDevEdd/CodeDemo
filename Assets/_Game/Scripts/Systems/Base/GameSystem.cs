using _Game.Scripts.Enums;
using UnityEngine;

namespace _Game.Scripts.Systems.Base
{
    public class GameSystem
    {
        private enum GameState 
        {
            None,
            Loading,
            Play,
            Pause
        }

        private GameState _state;
        public bool GameIsLoading => _state is GameState.Loading or GameState.None;
        public bool GamePaused => _state is GameState.Pause or GameState.Loading;

        public GameSystem()
        {
            _state = GameState.None;
        }
        
        public void LoadedGame()
        {
            _state = GameState.Play;
            Time.timeScale = 1f;
        }
        
        public void StartGame()
        {
            _state = GameState.Loading;
            //AppEventProvider.TriggerEvent(AppEventType.Analytics, GameEvents.Launch);
        }

        public void PauseGame()
        {
            if(_state == GameState.Loading) return;
            Time.timeScale = 0f;
            _state = GameState.Pause;
        }
    }
}