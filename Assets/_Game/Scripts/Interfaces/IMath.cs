using _Game.Scripts.Enums;

namespace _Game.Scripts.Interfaces
{
    public interface IMath
    {
        public float GetValue(GameParamType type, int level, float value = 0, int targetParam1 = 0, int targetParam2 = 0);
    }
}