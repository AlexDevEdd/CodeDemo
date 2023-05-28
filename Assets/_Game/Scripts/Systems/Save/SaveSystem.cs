using System;
using _Game.Scripts.Enums;
using _Game.Scripts.GameParams;
using _Game.Scripts.GameProgresses;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Systems.Base;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Systems.Save
{
    /// <summary>
    /// Данный класс для загрузки и сохранения данных
    /// </summary>
    public class SaveSystem : ITickableProjectSystem, IResetGameLogic
    {
        public Action OnSave;

        [Inject] private ProjectSettings _settings;
        [Inject] private GameSettings _gameSettings;
        [Inject] private GameFlags _flags;
        [Inject] private GameParamFactory _paramFactory;
        [Inject] private GameProgressFactory _progressFactory;

        private MainSnapshot _snapshot;

        private const float SAVE_TIME = 3f;
        private float _saveTimer;
        private bool _active = true;

        private const string KEY = "MAIN_DATA";
        
        public bool OnDeleteSave { get; private set; }
        
        public void Init()
        {
            _snapshot = new MainSnapshot();
        }

        public void ResetGameLogic()
        {
            OnSave = null;
            OnDeleteSave = false;
        }

        public void Save(bool onlyMainSave = false)
        {
            //MOCK
        }

        private void SaveData()
        {
            //Сохранение глобальных параметров
        }
        
        public void LoadSnapshot()
        {
           //MOCK
        }
        
        public void LoadData()
        {
            //Загрузка глобальных параметров
        }

        private void DeleteSave()
        {
            //File.Delete(MAIN_DATA_PATH);
            PlayerPrefs.DeleteAll();
        }

        public void SetActive(bool flag)
        {
            _active = flag;
        }

        public void Tick(float deltaTime)
        {
            if (!_active || _flags.Has(GameFlag.TutorialRunning)) return;
            _saveTimer += deltaTime;
            if (_saveTimer >= SAVE_TIME)
            {
                Save();
                _saveTimer = 0;
            }
        }
    }
}