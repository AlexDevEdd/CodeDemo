namespace _Game.Scripts.Interfaces
{
    public interface ICalculationSystem
    {
        public float GetIncomeByTime(float count);
        public float GetIncomeByAllRooms(float count);
        public float GetIncomeByOffline(float count);
    }
}