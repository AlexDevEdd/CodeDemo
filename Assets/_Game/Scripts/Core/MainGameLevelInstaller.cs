using _Game.Scripts.Interfaces;
using _Game.Scripts.MapPoints;
using _Game.Scripts.ResourceBubbles;
using _Game.Scripts.Systems;
using _Game.Scripts.Vehicles;
using _Game.Scripts.Vehicles.View;
using _Game.Scripts.View.Emotions;
using UnityEngine;

namespace _Game.Scripts.Core
{
    public class MainGameLevelInstaller : BaseLevelInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.Bind<AppEventProvider>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MapPointsFactory>().AsSingle().NonLazy();

            #region Vehicles

            Container.Bind<VehicleFactory>().AsSingle().NonLazy();
            Container.BindMemoryPool<VehicleView, VehicleView.Pool>()
                .WithInitialSize(15)
                .FromComponentInNewPrefab(Resources.Load<VehicleView>("Prefabs"))
                .UnderTransformGroup("Vehicles");
            
            Container.BindInterfacesAndSelfTo<SpawnVehicleSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VehicleLogicSystem>().AsSingle().NonLazy();
            Container.BindMemoryPool<VehicleLogic, VehicleLogic.Pool>().WithInitialSize(10);

            #endregion

            Container.BindInterfacesAndSelfTo<RewardSystem>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<EmotionsBubbleFactory>().AsSingle().NonLazy();
            Container.BindMemoryPool<EmotionBubbleView, EmotionBubbleView.Pool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(Resources.Load<EmotionBubbleView>("Prefabs"))
                .UnderTransformGroup("EmotionBubbles");
        }

        public override void InitLevelUI()
        {
            base.InitLevelUI();
            Windows.OpenStartWindows();
        }

        public override void ResetGameLogic()
        {
            base.ResetGameLogic();
            Container.Resolve<ResourceBubbleFactory>().ResetBeforeLoadLevel();
        }

        public override void LoadSave(bool clearData)
        {
            var loadSystems = Container.ResolveAll<ILoadSystem>();
            foreach (var system in loadSystems)
            {
                system.Load(clearData);
            }
        }
    }
}