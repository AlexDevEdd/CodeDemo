using _Game.Scripts.Enums;
using _Game.Scripts.GameParams;
using _Game.Scripts.ScriptableObjects.Balance;

namespace _Game.Scripts.Systems.Currencies
{
    public class GlobalCurrencySystem : BaseCurrencySystem
    {
        public GameParam Hard { get; private set; }
        public GameParam Tickets { get; private set; }

        public GlobalCurrencySystem(GameParamFactory gameParams, GameBalanceConfigs balance) : base(gameParams, balance)
        {
            Hard = AddCurrencyType(GameParamType.Hard);
            Tickets = AddCurrencyType(GameParamType.AdTickets);
        }
    }
}
