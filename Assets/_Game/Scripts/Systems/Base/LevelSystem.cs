using System;
using System.Linq;
using _Game.Scripts.Enums;
using _Game.Scripts.GameParams;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Game.Scripts.Systems.Base
{
    /// <summary>
    /// Класс для загрузки/уничтожения уровней
    /// </summary>
    public class LevelSystem : IInitProjectSystem
    {
        public Action OnLoadedLevel, OnUnloadedLevel;

        [Inject] private ProjectSettings _projectSettings;
        [Inject] private GameParamFactory _paramFactory;
        [Inject] private GameBalanceConfigs _balance;
        [Inject] private SceneSystem _sceneSystem;
        [Inject] private GameParamFactory _gameParamFactory;

        private string _sceneName;
        private LevelConfig _config;
        private GameParam _levelId => _gameParamFactory.GetParam(GameParamOwnerType.LevelSystem, GameParamType.GameLevel);

        public GameObject SceneRoot { get; private set; }

        public void InitProjectSystem()
        {
            _paramFactory.CreateParam(GameParamOwnerType.LevelSystem, GameParamType.GameLevel, 1);
            SceneRoot = SceneManager.GetSceneByName("MainScene").GetRootGameObjects()[0];
        }

        public void SetLevel(int level)
        {
            _levelId.SetValue(level);
        }

        public void IncreaseLevel()
        {
            _levelId.Change(1);
        }

        public void LoadLevel(string name)
        {
            _config = _balance.DefaultBalance.LevelConfigs.FirstOrDefault(s => s.SceneName == name);
            if (_config == null) return;
            _sceneName = name;
            _sceneSystem.LoadScene(_sceneName, OnSceneLoaded);
        }

        public void LoadLevel()
        {
            if (!_sceneName.IsNullOrEmpty())
            {
                _sceneSystem.UnloadScene(_sceneName, LoadScene);
            }
            else
            {
                LoadScene();
            }

            void LoadScene()
            {
                if (_projectSettings.MapId > 0)
                {
                    _sceneName = $"Level{_projectSettings.MapId}";
                }
                else
                {
                    _config = _balance.DefaultBalance.LevelConfigs.FirstOrDefault(s => s.Id == _levelId.Value);
                    if (_config == null) return;
                    _sceneName = _config.SceneName;
                }
                
                _sceneSystem.LoadScene(_sceneName, OnSceneLoaded);
            }
        }

        public void UnloadLevel()
        {
            if (_sceneName.IsNullOrEmpty()) return;
            _sceneSystem.UnloadScene(_sceneName, OnSceneUnloaded);
        }

        private void OnSceneUnloaded()
        {
            _sceneName = "";
            OnUnloadedLevel?.Invoke();
        }

        private void OnSceneLoaded()
        {
            SceneRoot = SceneManager.GetSceneByName(_sceneName).GetRootGameObjects()[0];
            OnLoadedLevel?.Invoke();
        }

        public string GetLevelName()
        {
            return $"LEVEL_NAME_{_levelId.Value}".ToLocalized();
        }
        
        public string GetLevelLongName()
        {
            var lvlName = $"LEVEL_NAME_{_levelId.Value}".ToLocalized();
            var str = $"{_levelId.Value}";
            if (str.Length == 1) str = $"[0{_levelId.Value}] {lvlName}";
            return str;
        }

        public int GetLevelId()
        {
            return (int)_levelId.Value;
        }
        
        public int GetTutorialId()
        {
            return _config.TutorialId;
        }

        public string GetLevelPlacement()
        {
            return _config.LevelPlacement;
        }
    }
}