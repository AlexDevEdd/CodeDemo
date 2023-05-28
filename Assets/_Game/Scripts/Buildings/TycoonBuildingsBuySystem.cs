using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects.Balance;
using Zenject;

namespace _Game.Scripts.Buildings
{
    public class TycoonBuildingsBuySystem : IInitSceneObjects
    {
        [Inject] private TycoonBuildingsFactory _buildingsFactory;

        public void InitSceneObjects()
        {
            foreach (var view in _buildingsFactory.Views)
            {
                var visualView = view.GetComponent<BuildingViewVisualConfig>();
                if (visualView == null) continue;
                
                if (!view.IsUnlocked)
                {
                    visualView.ShowInactive();
                }
                else
                {
                    visualView.ShowActive();
                    view.Unlock();
                }
            }
        }

        public void Unlock(int buildingId)
        {
            var view = _buildingsFactory.GetById(buildingId);
            if (view == null) return;
            
            _buildingsFactory.Unlock(view);
            
            var visualView = view.GetComponent<BuildingViewVisualConfig>();
            visualView?.ShowActive();

            RunAction(view.Config, view.Config.Action);
            AppEventProvider.TriggerEvent(AppEventType.Tasks, GameEvents.BuildBuilding, buildingId);
        }
        
        private void RunAction(BuildingItemConfig config, TaskAction action)
        {
            //MOCK
        }
    }
}