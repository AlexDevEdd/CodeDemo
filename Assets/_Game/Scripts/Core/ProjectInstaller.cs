using System.Collections.Generic;
using _Game.Scripts.Components.Loader;
using _Game.Scripts.GameParams;
using _Game.Scripts.GameProgresses;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Systems;
using _Game.Scripts.Systems.Ads;
using _Game.Scripts.Systems.Analytics;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Systems.Currencies;
using _Game.Scripts.Systems.Input;
using _Game.Scripts.Systems.Save;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Game.Scripts.Core
{
    public class ProjectInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private ProjectSettings _projectSettings;
        [SerializeField] private GameResources _resources;
        [SerializeField] private GameBalanceConfigs _balances;
        [SerializeField] private Localization _localization;
        
        private bool _init;
        
        private GameObject _root;
        private GameSystem _game;
        private WindowsSystem _windows;
        private List<ITickableProjectSystem> _tickSystems;

        public override void Start()
        {
#if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;
#else
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif
            base.Start();
        }

        public override void InstallBindings()
        {
            _root = SceneManager.GetActiveScene().GetRootGameObjects()[0];
            
            BindScriptableObjects();
            BindSystems();
            BindLoaderComponents();

            PrettyPrint.Init(_localization);
        }

        private void BindScriptableObjects()
        {
            Container.BindInstance(_projectSettings).AsSingle().NonLazy();
            Container.BindInstance(_resources).AsSingle().NonLazy();
            Container.BindInstance(_balances).AsSingle().NonLazy();
            Container.BindInstance(_localization).AsSingle().NonLazy();

            _localization.Initialize();
        }

        private void BindSystems()
        {
            //Main logic
            Container.BindInterfacesAndSelfTo<IInitializable>().FromInstance(this).AsSingle().NonLazy();
            
            Container.Bind<GameSettings>().AsSingle().NonLazy();
            Container.Bind<AnalyticsSystem>().AsSingle().NonLazy();
            Container.Bind<AdSystem>().AsSingle().NonLazy();
            Container.Bind<GameSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelSystem>().AsSingle().NonLazy();
            Container.Bind<LoadingSystem>().AsSingle().NonLazy();
            Container.Bind<GameFlags>().AsSingle().NonLazy();
            Container.Bind<GlobalCurrencySystem>().AsSingle().NonLazy();
            Container.Bind<CalculateHardCurrency>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameParamFactory>().AsSingle().NonLazy();
            Container.BindMemoryPool<GameParam, GameParam.Pool>().WithInitialSize(100);

            Container.BindInterfacesAndSelfTo<GameProgressFactory>().AsSingle().NonLazy();
            Container.BindMemoryPool<GameProgress, GameProgress.Pool>().WithInitialSize(100);

            Container.BindInterfacesAndSelfTo<WindowsSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
            
            AddSinglePrefab<SoundSystem>();
            AddSinglePrefab<SceneSystem>();
            AddSinglePrefab<GameCamera>();
            AddSinglePrefab<ConnectionSystem>();
        }

        private void AddSinglePrefab<T>(bool copyPrefab = false) where T : MonoBehaviour
        {
            var prefab = copyPrefab
                ? Resources.Load<T>("Prefabs")
                : _root.GetComponentInChildren<T>(true);
        
            if (prefab == null) return;
            Container.BindInterfacesAndSelfTo<T>().FromInstance(prefab).AsSingle().NonLazy();
        }
        
        private void BindLoaderComponents()
        {
            Container.Bind<ATTrackingStatusComponent>().AsSingle().NonLazy();
            Container.Bind<ConnectionCheckComponent>().AsSingle().NonLazy();
            Container.Bind<AnalyticsComponent>().AsSingle().NonLazy();
            Container.Bind<GameBalanceInitializeComponent>().AsSingle().NonLazy();
            Container.Bind<InitGameSystemsComponent>().AsSingle().NonLazy();
            Container.Bind<LevelLoaderComponent>().AsSingle().NonLazy();
            Container.Bind<ResetBeforeLoadComponent>().AsSingle().NonLazy();
            Container.Bind<EndLoadingComponent>().AsSingle().NonLazy();
            Container.Bind<UpdateVersionComponent>().AsSingle().NonLazy();
            Container.Bind<InitLevelComponent>().AsSingle().NonLazy();
            Container.Bind<InitNextLevelComponent>().AsSingle().NonLazy();
            Container.Bind<GameStartComponent>().AsSingle().NonLazy();
            Container.Bind<LevelStartComponent>().AsSingle().NonLazy();
            Container.Bind<UnloadLevelComponent>().AsSingle().NonLazy();
            Container.Bind<SnapshotLoaderComponent>().AsSingle().NonLazy();
            Container.Bind<SaveDataLoaderComponent>().AsSingle().NonLazy();
            Container.Bind<TapGameStartComponent>().AsSingle().NonLazy();
        }

        public void Initialize()
        {
            _init = true;
            _tickSystems = Container.ResolveAll<ITickableProjectSystem>();
            _game = Container.Resolve<GameSystem>();
            _game.StartGame();

            InitSystems();
            InitWindows();
            Container.Resolve<LoadingSystem>().Init().LoadGame();
        }

        private void InitSystems()
        {
            var systems = Container.ResolveAll<IInitProjectSystem>();
            foreach (var system in systems)
            {
                system.InitProjectSystem();
            }
        }

        private void InitWindows()
        {
            _windows = Container.Resolve<WindowsSystem>();
            var windows = _root.GetComponentsInChildren<BaseWindow>(true);
            foreach (var window in windows)
            {
                _windows.AddWindow(window);
            }
            _windows.InitWindows();
        }

        private void Update()
        {
            if (!_init) return;

            var deltaTime = Time.deltaTime;
            if (_game.GamePaused)
            {
                InvokeSystem.ForceTick();
                return;
            }

            DOTween.ManualUpdate(deltaTime, Time.unscaledDeltaTime);
            foreach (var element in _tickSystems)
            {
                element.Tick(deltaTime);
            }

            InvokeSystem.Tick(deltaTime);
        }
    }
}