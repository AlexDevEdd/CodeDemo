using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Systems.Currencies;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Systems
{
    public class CheatsSystem : ITickableSystem
    {
        [Inject] private GlobalCurrencySystem _globalCurrencySystem;
        [Inject] private ICurrencySystem _currencySystem;
        [Inject] private GameBalanceConfigs _balance;
        [Inject] private WindowsSystem _windows;

        public void Tick(float deltaTime)
        {
            Pause();
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.F11)) CheckTimeScale(3f);
            if (UnityEngine.Input.GetKeyDown(KeyCode.F12)) CheckTimeScale(10f);
            if (UnityEngine.Input.GetKeyDown(KeyCode.S)) AddSoft();
            if (UnityEngine.Input.GetKeyDown(KeyCode.H)) AddHard();
            if (UnityEngine.Input.GetKeyDown(KeyCode.T)) AddTokens();
            if (UnityEngine.Input.GetKeyDown(KeyCode.E)) AddEnergy();
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1)) AddCards(5);
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2)) AddCards(9);
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3)) AddCards(11);
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4)) AddCards(13);
            if (UnityEngine.Input.GetKeyDown(KeyCode.R)) StartTutorial(600, 1);
            if (UnityEngine.Input.GetKeyDown(KeyCode.F)) CreateTask(1, 25);
            if (UnityEngine.Input.GetKeyDown(KeyCode.O)) OpenWindow();
            if (UnityEngine.Input.GetKeyDown(KeyCode.L)) SkipTime();
        }
        
        private void OpenWindow()
        {
           
        }
        
        private void SkipTime()
        {
        }

        private void CreateTask(int region, int id)
        {
        }

        private void StartTutorial(int tutorialId, int stepId)
        {
            //_tutorial.StartTutorial(tutorialId, stepId);
        }

        private void AddCards(int cardId)
        {
        }

        public static void CheckTimeScale(float value) => Time.timeScale = Time.timeScale > 1f ? 1f : value;
        private static bool IsKeyDown(KeyCode keyCode) => UnityEngine.Input.GetKeyDown(keyCode);
        
        private static void Pause()
        {
            if (!IsKeyDown(KeyCode.P)) return;
            Time.timeScale = Time.timeScale >= 1 ? 0 : 1;
        }
        
        public void AddSoft()
        {
            _currencySystem.AddCurrency(GameParamType.Soft, 100000000000000000, GetRewardPlace.None);
        }
        
        public void AddHard()
        {
            _globalCurrencySystem.AddCurrency(GameParamType.Hard, 9000, GetRewardPlace.None);
        }
        
        public void AddTokens()
        {
            _currencySystem.AddCurrency(GameParamType.Tokens, 1000, GetRewardPlace.None);
        }
        
        public void AddEnergy()
        {
            //_params.GetParam(GameParamOwnerType.CurrencySystem, GameParamType.Energy)?.Change(1000);
        }

        public void RemoveAllPurchases()
        {
            // _flags.Remove(GameFlag.AllAdsRemoved);
            // _flags.Remove(GameFlag.InAppWasBought);
            // _flags.Remove(GameFlag.ManagerIsBought);
            // _flags.Remove(GameFlag.MarketerIsBought);
            // _flags.Remove(GameFlag.StarterPackIsBought);
        }
    }
}