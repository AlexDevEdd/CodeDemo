using _Game.Scripts.GameProgresses;
using Zenject;

namespace _Game.Scripts.Systems
{
    /// <summary>
    /// Класс пересчета времени игровых активностей в момент отсутствия игрока
    /// </summary>
    public class CalculationOfflineSystem
    {
        [Inject] private GameProgressFactory _progresses;
       
        public void Calculate(float time)
        {
            CalculateOfflineProgresses(time);
            CalculateOfflineBoosts(time);
            CalculateOfflineInApps(time);
        }

        private void CalculateOfflineProgresses(float time)
        {
            //MOCK
        }
        
        private void CalculateOfflineBoosts(float time)
        {
            //MOCK
        }
        
        private void CalculateOfflineInApps(float time)
        {
            //MOCK
        }
    }
}