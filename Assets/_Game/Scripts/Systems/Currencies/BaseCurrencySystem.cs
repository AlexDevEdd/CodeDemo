using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Enums;
using _Game.Scripts.GameParams;
using _Game.Scripts.ScriptableObjects.Balance;

namespace _Game.Scripts.Systems.Currencies
{
    public class BaseCurrencySystem
    {
        protected readonly GameParamFactory Params;
        protected readonly GameBalanceConfigs GameBalance;

        protected List<GameParam> Currencies = new();

        protected BaseCurrencySystem(GameParamFactory gameParams, GameBalanceConfigs balance)
        {
            Params = gameParams;
            GameBalance = balance;
        }

        protected GameParam AddCurrencyType(GameParamType type, float value = 0, bool global = true)
        {
            var currency = Params.CreateParam(GameParamOwnerType.CurrencySystem, type, value, global);
            Currencies.Add(currency);
            return currency;
        }

        public GameParam GetCurrency(GameParamType type)
        {
            return Currencies.FirstOrDefault(c => c.Type == type);
        }

        public float GetCurrencyValue(GameParamType type)
        {
            return Currencies.FirstOrDefault(c => c.Type == type)?.Value ?? 0;
        }

        public void SetValue(GameParamType type, float value)
        {
            var currency = GetCurrency(type);
            currency?.SetValue(value);
        }

        public void AddCurrency(GameParamType type, float value, GetRewardPlace place)
        {
            var param = GetCurrency(type);
            param?.Change(value);
            AppEventProvider.TriggerEvent(AppEventType.Tasks, GameEvents.CollectCurrency, type, value);
        }
        
        public bool IsEnoughCurrency(GameParamType type, float needed)
        {
            var param = GetCurrency(type);
            return param?.Value >= needed;
        }

        public void SpendCurrency(GameParamType type, float value, SpendCurrencyPlace place)
        {
            var param = GetCurrency(type);
            if (param == null) return;
            param.Change(-value);
            SendSpendCurrency(type, value, param.Value, place);
        }
        
        private void SendSpendCurrency(GameParamType currency, float value, float total, SpendCurrencyPlace place)
        {
            AppEventProvider.TriggerEvent(AppEventType.Tasks, GameEvents.SpendCurrency, currency, value);
        }
    }
}