using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Vehicles.Base;
using _Game.Scripts.Vehicles.View;
using Zenject;

namespace _Game.Scripts.Vehicles
{
    public class VehicleLogic
    {
        private readonly List<BaseVehicleBehaviour> _behaviours = new();
        
        public VehicleViewBase Vehicle { get; private set; }
        public List<BaseVehicleBehaviour> Behaviours => _behaviours;
        
        public class Pool : MemoryPool<VehicleViewBase, VehicleLogic>
        {
            protected override void Reinitialize(VehicleViewBase vehicle, VehicleLogic logic)
            {
                logic.Reinitialize(vehicle);
            }
        }
        
        private void Reinitialize(VehicleViewBase vehicle)
        {
            Vehicle = vehicle;
        }
        
        public void Init(params object[] list)
        {
            foreach (var behaviour in _behaviours)
            {
                behaviour.Init(list);
            }
        }
        
        public virtual void Tick(float deltaTime)
        {
            for (int i = 0; i < _behaviours.Count; i++)
            {
                _behaviours[i].Tick(deltaTime);
            }
        }
        
        public void Clear()
        {
            _behaviours.Clear();
        }
        
        public virtual void Start()
        {
            foreach (var behaviour in _behaviours)
            {
                behaviour.RunNextStep();
            }
        }
        
        public void AddBehaviour(BaseVehicleBehaviour behaviour)
        {
            _behaviours.Add(behaviour);
        }

        public BaseVehicleBehaviour RemoveBehaviour()
        {
            var behaviour = _behaviours.Last();
            _behaviours.Remove(behaviour);
            return behaviour;
        }
        
        public BaseVehicleBehaviour GetBehaviour()
        {
            return _behaviours.Last();
        }

        public BaseVehicleBehaviour RemoveLastBehaviour()
        {
            var behaviour = _behaviours.Last();
            _behaviours.Remove(behaviour);
            return behaviour;
        }
    }
}