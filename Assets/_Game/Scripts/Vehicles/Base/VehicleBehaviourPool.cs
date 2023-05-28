using _Game.Scripts.Vehicles.View;
using Zenject;

namespace _Game.Scripts.Vehicles.Base
{
    public class VehicleBehaviourPool<TBehaviour> : MemoryPool<VehicleViewBase, VehicleBehaviourType, TBehaviour>, IVehicleLogicPool
        where TBehaviour : BaseVehicleBehaviour
    {
        public virtual VehicleBehaviourType GetBehaviourType { get; }

        protected override void Reinitialize(VehicleViewBase characterView, VehicleBehaviourType type, TBehaviour logic)
        {
            logic.Reinitialize(characterView, type);
        }

        public BaseVehicleBehaviour SpawnBehaviour(VehicleViewBase characterView, VehicleBehaviourType type)
        {
            return Spawn(characterView, type);
        }

        public void DeSpawn(BaseVehicleBehaviour item)
        {
            Despawn((TBehaviour)item);
        }
    }
}