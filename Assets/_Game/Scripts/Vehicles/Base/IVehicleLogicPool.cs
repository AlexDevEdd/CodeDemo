using _Game.Scripts.Vehicles.View;

namespace _Game.Scripts.Vehicles.Base
{
    public interface IVehicleLogicPool
    {
        public VehicleBehaviourType GetBehaviourType { get; }
        public BaseVehicleBehaviour SpawnBehaviour(VehicleViewBase vehicleView, VehicleBehaviourType type);
        public void DeSpawn(BaseVehicleBehaviour item);
    }
}