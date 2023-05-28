using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Vehicles.Base;
using _Game.Scripts.Vehicles.View;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Vehicles
{
    public class VehicleLogicSystem : IInitGameLogic, ITickableSystem
    {
        [Inject] private DiContainer _container;
        
        private readonly VehicleLogic.Pool _pool;
        private readonly List<VehicleLogic> _logics = new();
        private List<IVehicleLogicPool> _vehicleLogicPool;
        
        public VehicleLogicSystem(VehicleLogic.Pool pool)
        {
            _pool = pool;
        }

        public void InitGameLogic()
        {
            _container.ResolveAll<IVehicleLogicPool>();
            _vehicleLogicPool = new List<IVehicleLogicPool>();
            
            var poolType = typeof(IVehicleLogicPool);
            foreach (var contract in _container.AllContracts.Where(c => poolType.IsAssignableFrom(c.Type)))
            {
                _vehicleLogicPool.Add(_container.Resolve(contract.Type) as IVehicleLogicPool);
            }
        }

        public void Tick(float deltaTime)
        {
            for (int i = 0; i < _logics.Count; i++)
            {
                _logics[i].Tick(deltaTime);
            }
        }

        public void AddVehicle(VehicleViewBase vehicleView, VehicleBehaviourType type, params object[] list)
        {
            var logic = _pool.Spawn(vehicleView);
            var behaviour = GetBehaviour(vehicleView, type);
            behaviour.InitSceneObjects();
            logic.AddBehaviour(behaviour);
            logic.Init(list);
            logic.Start();
            _logics.Add(logic);
        }
        
        public void RemoveVehicle(VehicleViewBase vehicleView)
        {
            var logic = _logics.FirstOrDefault(l => l.Vehicle == vehicleView);
            if (logic == null) return;
            
            var behaviour = logic.RemoveLastBehaviour();
            RemoveBehaviour(behaviour);
            _logics.Remove(logic);
        }

        public void AddNewBehaviour(VehicleView vehicleView, VehicleBehaviourType type)
        {
            var logic = _logics.FirstOrDefault(l => l.Vehicle == vehicleView);
            if (logic == null) return;
            
            var behaviour = logic.RemoveBehaviour();
            RemoveBehaviour(behaviour);
            logic.AddBehaviour(GetBehaviour(vehicleView, type));
            logic.Init();
            logic.Start();
        }

        public BaseVehicleBehaviour GetBehavior(VehicleView vehicleView)
        {
            var logic = _logics.FirstOrDefault(l => l.Vehicle == vehicleView);
            return logic?.GetBehaviour();
        }

        public List<BaseVehicleBehaviour> GetBehaviors(List<VehicleView> vehicles)
        {
            return _logics.Where(l => vehicles.Contains(l.Vehicle))
                .Select(logic => logic.GetBehaviour())
                .ToList();
        }

        public BaseVehicleBehaviour GetLogic(VehicleBehaviourType type)
        {
            return _logics.Where(l => l.GetBehaviour().BehaviourType == type)
                .Select(logic => logic.GetBehaviour())
                .FirstOrDefault();
        }

        public List<BaseVehicleBehaviour> GetLogics(VehicleBehaviourType type)
        {
            return _logics.Where(l => l.GetBehaviour().BehaviourType == type)
                .Select(logic => logic.GetBehaviour())
                .ToList();
        }

        public void RemoveBehaviour(BaseVehicleBehaviour behaviour)
        {
            behaviour.DeSpawnBehaviour();
            foreach (var test in _vehicleLogicPool.Where(test => test.GetBehaviourType == behaviour.BehaviourType))
            {
                test.DeSpawn(behaviour);
            }
        }

        private BaseVehicleBehaviour GetBehaviour(VehicleViewBase vehicleView, VehicleBehaviourType type)
        {
            return _vehicleLogicPool.FirstOrDefault(b => b.GetBehaviourType == type)?
                .SpawnBehaviour(vehicleView, type);
        }
    }
}