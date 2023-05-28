using System.Collections.Generic;
using _Game.Scripts.Components.Base;
using _Game.Scripts.Components.Loader;
using _Game.Scripts.Ui;
using Zenject;

namespace _Game.Scripts.Systems.Base
{
    public enum LoaderState
    {
        GameLoading,
        LevelLoading
    }
    
    public class LoadingSystem
    {
        [Inject] private DiContainer _container;
        [Inject] private WindowsSystem _windows;
        
        private LoadingWindow _loadingWindow;

        private readonly List<BaseComponent> _steps = new();
        
        private BaseComponent _currentStep;

        public LoadingSystem Init()
        {
            _loadingWindow = (LoadingWindow)_windows.GetWindow<LoadingWindow>();
            return this;
        }

        public void LoadGame()
        {
            StartLoader(LoaderState.GameLoading);
        }
        
        public void LoadLevel()
        {
            StartLoader(LoaderState.LevelLoading);
        }

        private void StartLoader(LoaderState state)
        {
            _loadingWindow.StartLoading(state);

            switch (state)
            {
                case LoaderState.GameLoading:
                    _steps.Add(_container.Resolve<ATTrackingStatusComponent>());
                    _steps.Add(_container.Resolve<ConnectionCheckComponent>());
                    _steps.Add(_container.Resolve<UpdateVersionComponent>());
                    _steps.Add(_container.Resolve<AnalyticsComponent>());
                    _steps.Add(_container.Resolve<GameBalanceInitializeComponent>());
                    _steps.Add(_container.Resolve<InitGameSystemsComponent>());
                    _steps.Add(_container.Resolve<SnapshotLoaderComponent>());
                    _steps.Add(_container.Resolve<LevelLoaderComponent>());
                    _steps.Add(_container.Resolve<InitLevelComponent>());
                    _steps.Add(_container.Resolve<EndLoadingComponent>());
                    _steps.Add(_container.Resolve<GameStartComponent>());
                    break;
                case LoaderState.LevelLoading:
                    _steps.Add(_container.Resolve<LevelLoaderComponent>());
                    _steps.Add(_container.Resolve<InitNextLevelComponent>());
                    _steps.Add(_container.Resolve<EndLoadingComponent>());
                    _steps.Add(_container.Resolve<LevelStartComponent>());
                    break;
            }
            
            GoToNextStep();
        }

        private void GoToNextStep()
        {
            _currentStep = _steps[0];
            _currentStep.OnEnd += OnEndStep;
            _currentStep.Start();
        }

        private void OnEndStep()
        {
            UpdateProgress();
            
            _currentStep.OnEnd -= OnEndStep;
            _steps.Remove(_currentStep);
            _currentStep = null;

            if (_steps.Count == 0)
            {
                _loadingWindow.EndLoading();
                return;
            }
            
            InvokeSystem.StartExternalInvoke(GoToNextStep, 2);
        }

        private void UpdateProgress()
        {
            var progress = (float)(_steps.IndexOf(_currentStep)+1) / _steps.Count;
            _loadingWindow.UpdateProgress(progress);
        }
    }
}