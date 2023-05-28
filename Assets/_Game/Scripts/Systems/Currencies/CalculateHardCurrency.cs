using _Game.Scripts.Enums;

namespace _Game.Scripts.Systems.Currencies
{
    public class CalculateHardCurrency
    {
        public int GetHardPriceFromResources(GameParamType gameParam, int value)
        {
            switch (gameParam)
            {
                case GameParamType.RoomCards:
                case GameParamType.CharacterCards:
                    return value * 7;
                default:
                    return (int)(value * 2.5f);
            }
        }
    }
}