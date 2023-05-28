using _Game.Scripts.Enums;
using _Game.Scripts.GameParams;

namespace _Game.Scripts.Interfaces
{
    public interface ICurrencySystem
    {
        public GameParam Soft { get; set; }
        public GameParam Tokens { get; set; }
        public void AddCurrency(GameParamType type, float value, GetRewardPlace place);
        public float GetCurrencyValue(GameParamType type);
        public GameParam GetCurrency(GameParamType type);
        public bool IsEnoughCurrency(GameParamType type, float needed);
        public void SpendCurrency(GameParamType type, float value, SpendCurrencyPlace place);
    }
}