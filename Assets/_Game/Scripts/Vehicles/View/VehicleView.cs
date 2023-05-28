using _Game.Scripts.Enums;
using Zenject;

namespace _Game.Scripts.Vehicles.View
{
    public class VehicleView : VehicleViewBase
    {
        public class Pool : MonoMemoryPool<VehicleType, int, VehicleView>
        {
            protected override void Reinitialize(VehicleType type, int id, VehicleView car)
            {
                car.Reinitialize(type, id);
            }
        }
        
        [Inject]
        private void Construct()
        {
            Init();
        }
        
        private void Reinitialize(VehicleType type, int id)
        {
            SetId(id);
            ShowSkin(type);
        }
    }
}