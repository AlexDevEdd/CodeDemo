using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using UnityEngine;

namespace _Game.Scripts.Ui.Loading
{
    public class WindowLoader : BaseUIView
    {
        [SerializeField] private LoaderState _loaderState;
        
        protected LoadingWindow LoadingWindow;
        protected GameResources Resources;
        
        public LoaderState LoaderState => _loaderState;

        protected virtual void UpdateProgress(float value)
        {
        }

        protected virtual void StartLoading()
        {
            gameObject.Activate();
        }
        
        public virtual void InitializeLoader(LoadingWindow loadingWindow, GameResources resources)
        {
            LoadingWindow = loadingWindow;
            LoadingWindow.OnProgressUpdate += UpdateProgress;
            LoadingWindow.OnEndLoading += EndLoading;
            LoadingWindow.OnStartLoading += StartLoading;
            Resources = resources;
        }
        
        protected virtual void EndLoading()
        {
            LoadingWindow.OnProgressUpdate -= UpdateProgress;
            LoadingWindow.OnEndLoading -= EndLoading;
            LoadingWindow.OnStartLoading -= StartLoading;
            LoadingWindow = null;
            gameObject.Deactivate();
        }
    }
}
