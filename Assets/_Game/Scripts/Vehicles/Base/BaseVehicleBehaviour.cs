using System;
using _Game.Scripts.MapPoints;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Vehicles.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Vehicles.Base
{
    public class BaseVehicleBehaviour
    {
        public enum VehicleState
        {
            Idle,
            Moving,
            Stop
        }

        private const float _rotationSpeed = 3.5f;
        private const float _stoppingDistance = 0.01f;

        private Action _callback;
        private VehicleState _state;
        
        private float _baseSpeed;
        private float _speed;
        private bool _checkObstacles = true;
        private int _directionsCounter;
        
        private PointView _targetPoint;

        public VehicleViewBase Vehicle;

        private readonly Vector3[] _directions = { Vector3.right, Vector3.left, Vector3.zero };

        public VehicleBehaviourType BehaviourType { get; private set; }

        public virtual void InitSceneObjects()
        {
        }

        public virtual void DeSpawnBehaviour()
        {
        }

        public virtual void Reinitialize(VehicleViewBase vehicle, VehicleBehaviourType type)
        {
            Vehicle = vehicle;
            BehaviourType = type;
            _state = VehicleState.Idle;
            _targetPoint = null;
            _baseSpeed = Vehicle.NavMeshAgent.speed;
            _speed = _baseSpeed;
        }

        public virtual void Init(params object[] list)
        {
        }

        public virtual void RunNextStep()
        {
        }

        public virtual void Tick(float deltaTime)
        {
        }

        protected virtual void GoTo(Action callback, PointView targetPoint)
        {
            _callback = callback;
            _targetPoint = targetPoint;
            if (BehaviourType != VehicleBehaviourType.EnviromentCar) Vehicle.LightingParticleAction(true);
            Vehicle.SetNavMeshAgentFlag(true);
            Vehicle.SetNavMeshDestination(_targetPoint.transform.position);
            Vehicle.NavMeshAgent.speed = _speed;
            SetState(VehicleState.Moving);
        }

        public void SetState(VehicleState state)
        {
            if (_state == state) return;
            _state = state;
        }

        public void UpdateMovement(float deltaTime)
        {
            if (_state == VehicleState.Idle) return;
            if (_checkObstacles) CheckObstacles();
            if (_state == VehicleState.Stop) return;

            var navMeh = Vehicle.NavMeshAgent;
            var currentDistance = Vector3.Distance(navMeh.transform.position, navMeh.destination);
            if (currentDistance < _stoppingDistance)
            {
                Vehicle.SetPosition(navMeh.steeringTarget);
                Stop();
                return;
            }

            var offset = (navMeh.steeringTarget - Vehicle.transform.position).normalized * (navMeh.speed * deltaTime);
            if (offset.magnitude >= currentDistance) return;

            Vehicle.ChangePosition(offset);
        }

        private void CheckObstacles()
        {
            var ray = new Ray(Vehicle.ModelTransform.position, Vehicle.transform.forward + _directions[_directionsCounter] * 0.15f);

            _directionsCounter++;
            if (_directionsCounter > _directions.Length - 1) _directionsCounter = 0;
        }

        protected void UpdateRotation(float deltaTime)
        {
            if (_state == VehicleState.Idle) return;
            var navMeshAgent = Vehicle.NavMeshAgent;
            if (navMeshAgent.enabled == false) return;
            if (Vector3.Distance(navMeshAgent.transform.position, navMeshAgent.steeringTarget) <= 0.01f) return;

            var steeringTarget = navMeshAgent.steeringTarget;
            var transform = navMeshAgent.transform;
            var position = transform.position;
            navMeshAgent.transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(steeringTarget - position),
                _rotationSpeed * deltaTime);
        }

        protected virtual void Stop()
        {
            Vehicle.LightingParticleAction(false);

            SetState(VehicleState.Idle);
            Vehicle.SetNavMeshAgentFlag(false);
            _targetPoint = null;

            var action = _callback;
            _callback = null;
            action?.Invoke();
        }
        
        public void SetSpeed(float value)
        {
            _speed = value;
        }

        public void ResetSpeed()
        {
            _speed = _baseSpeed;
        }

        public void SetCheckObstacleFlag(bool flag)
        {
            _checkObstacles = flag;
        }
    }
}