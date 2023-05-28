using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using _Game.Scripts.Ui.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Ui
{
    public class LoadingWindow : BaseWindow
    {
        public Action<float> OnProgressUpdate;
        public Action OnEndLoading;
        public Action OnStartLoading;

        [SerializeField] private List<WindowLoader> _loadingWindows;
        private LoaderState _loaderState;
        private GameResources _resources;

        [Inject]
        public void Construct(GameResources resources)
        {
            _resources = resources;
        }
        
        public void StartLoading(LoaderState state)
        {
            _loaderState = state;
            gameObject.Activate();
            var needWindow = _loadingWindows.First(x => x.LoaderState == state);
            needWindow.InitializeLoader(this, _resources);
            OnStartLoading?.Invoke();
        }
        
        public void UpdateProgress(float value)
        {
            if (_loaderState is LoaderState.LevelLoading) return;
            OnProgressUpdate?.Invoke(value);
        }

        public void EndLoading()
        {
            gameObject.Deactivate();
            OnEndLoading?.Invoke();
        }
    }
}