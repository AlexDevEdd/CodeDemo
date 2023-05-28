using _Game.Scripts.Enums;
using _Game.Scripts.Vehicles.Base;
using _Game.Scripts.Vehicles.View;
using Zenject;

namespace _Game.Scripts.Vehicles
{
    public class SpawnVehicleSystem
    {
        [Inject] private VehicleFactory _vehicles;
        [Inject] private VehicleLogicSystem _vehicleLogic;

        public VehicleViewBase SpawnVehicle(VehicleType vehicleType, VehicleBehaviourType type, params object[] list)
        {
            var vehicle = _vehicles.SpawnVehicle(vehicleType);
            _vehicleLogic.AddVehicle(vehicle, type, list);
            return vehicle;
        }
        
        public void DeSpawnVehicle(VehicleViewBase vehicle)
        {
            _vehicleLogic.RemoveVehicle(vehicle);
            _vehicles.RemoveVehicle(vehicle);
        }
    }
}