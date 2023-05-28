using System.Collections.Generic;
using _Game.Scripts.Enums;
using _Game.Scripts.Tools;
using _Game.Scripts.Vehicles.View;

namespace _Game.Scripts.Vehicles
{
    public class VehicleFactory
    {
        private readonly VehicleView.Pool _pool;
        private readonly List<VehicleViewBase> _vehicles = new();
        
        public VehicleFactory(VehicleView.Pool pool)
        {
            _pool = pool;
        }
        
        public VehicleViewBase SpawnVehicle(VehicleType type)
        {
            var vehicle = _pool.Spawn(type, _vehicles.Count + 1);
            vehicle.Activate();
            _vehicles.Add(vehicle);
            return vehicle;
        }
        
        public void RemoveVehicle(VehicleViewBase vehicle)
        {
            _pool.Despawn((VehicleView)vehicle);
            _vehicles.Remove(vehicle);
        }
    }
}