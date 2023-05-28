using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Systems.GamePlayElements;
using _Game.Scripts.Ui.Base;

namespace _Game.Scripts.Systems.Base
{
    public class WindowsSystem : ITickableProjectSystem
    {
        public Action AnyWindowOpened;
        public event Action<BaseWindow> WindowOpenedEvent, WindowClosedEvent;

        private readonly List<BaseWindow> _all = new();
        private readonly List<BaseWindow> _windowsStack = new();
        private readonly List<BaseGamePlayElement> _gamePlayElements = new();

        private readonly GameCamera _camera;
        private readonly GameBalanceConfigs _balance;
        
        private BaseWindow _openedWindow;
        private BaseWindow _openedOverStackWindow;
        
        private bool _allowedOpenWindows = true;

        public WindowsSystem(GameCamera camera, GameBalanceConfigs balance)
        {
            _camera = camera;
            _balance = balance;
        }

        public void UpdateLocalization()
        {
            foreach (var window in _all)
            {
                window.UpdateLocalization();
            }

            foreach (var element in _gamePlayElements)
            {
                element.UpdateLocalization();
            }
        }

        public void Tick(float deltaTime)
        {
            if (_openedWindow != null) _openedWindow.Tick(deltaTime);
            if (_openedOverStackWindow != null) _openedOverStackWindow.Tick(deltaTime);
        }
        
        public virtual void ResetGameLogic()
        {
            _openedWindow = null;
            _openedOverStackWindow = null;
            _windowsStack.Clear();
        }
        
        public void AddWindow(BaseWindow window)
        {
            if (!_all.Contains(window))
            {
                _all.Add(window);
            }
        }
        
        public void RemoveWindow(BaseWindow window)
        {
            if (_all.Contains(window))
            {
                RemoveGamePlayElement(window);
                _all.Remove(window);
            }
        }

        public void InitWindows()
        {
            foreach (var window in _all.Where(w => !w.Inited))
            {
                window.Init();
                window.UpdateLocalization();
                InitGamePlayElements(window);
            }

            UpdateLocalization();
        }

        private void InitGamePlayElements(BaseWindow window)
        {
            var elements = window.GetComponentsInChildren<BaseGamePlayElement>(true);
            foreach (var element in elements)
            {
                element.Init(this, _balance);
                _gamePlayElements.Add(element);
            }
            
            SetGamePlayElementActive(GamePlayElement.None, true);
        }

        private void RemoveGamePlayElement(BaseWindow window)
        {
            var elements = window.GetComponentsInChildren<BaseGamePlayElement>(true);
            foreach (var element in elements)
            {
                _gamePlayElements.Remove(element);
            }
        }

        public BaseWindow ForceOpenWindow<T>(params object[] list)
        {
            if (!_allowedOpenWindows) return null;
            var window = _all.FirstOrDefault(w => w.GetType() == typeof(T));
            if (window == null) return null;
            if (window.IsOpened || !window.CanOpen(list)) return null;
            
            window.Opened += OnOpenedWindow;
            window.Closed += OnCloseButtonPressed;
            window.ForceOpen(list);
            
            _windowsStack.Add(window);
            _openedWindow = window;
            
            if (window.RenderUiOnly) _camera.SetRenderType(GameCamera.RenderType.UI);
            
            return window;
        }

        public BaseWindow OpenWindow<T>(params object[] list)
        {
            if (!_allowedOpenWindows) return null;
            var window = _all.FirstOrDefault(w => w.GetType() == typeof(T));
            if (window == null) return null;
            if (window.IsOpened || !window.CanOpen(list)) return null;
            if (window.AddToOpenedStack)
            {
                if (_windowsStack.Count != 0)
                {
                    if (!_windowsStack.Contains(window)) _windowsStack.Add(window);
                    if (_windowsStack[0].IsClosed) OpenFromStack();
                    return window;
                }
                
                window.Opened += OnOpenedWindow;
                window.Closed += OnCloseButtonPressed;
                window.Open(list);

                _windowsStack.Add(window);
                _openedWindow = window;
                if (window.RenderUiOnly) _camera.SetRenderType(GameCamera.RenderType.UI);
            }
            else
            {
                if (!window.ToOpenOverStack)
                {
                    CloseAllWindows();
                }
                else
                {
                    _openedOverStackWindow = window;
                }
                window.Opened += OnOpenedWindow;
                window.Closed += OnCloseButtonPressed;
                window.Open(list);
            }
            
            AnyWindowOpened?.Invoke();

            return window;
        }

        public void OpenWindow(BaseWindow window)
        {
            if (window.IsOpened) return;
            window.Opened += OnOpenedWindow;
            window.Closed += OnCloseButtonPressed;
            window.Open();
        }

        private void OnOpenedWindow(BaseWindow window)
        {
            window.Opened -= OnOpenedWindow;
            WindowOpenedEvent?.Invoke(window);
        }

        private void OnCloseButtonPressed(BaseWindow window)
        {
            window.Closed -= OnCloseButtonPressed;
            if (window.RenderUiOnly)
            {
                _camera.SetRenderType(GameCamera.RenderType.Default);
            }
            
            _openedWindow = null;
            if (_openedOverStackWindow == window) _openedOverStackWindow = null;
            if (_windowsStack.Contains(window)) _windowsStack.Remove(window);
            if (_windowsStack.Count > 0)
            {
                _openedWindow = _windowsStack[0];
                if (_openedWindow.IsClosed) OpenNextWindow();
            }

            WindowClosedEvent?.Invoke(window);
        }

        private void OpenNextWindow()
        {
            if (_windowsStack.Count == 0) return;
            _allowedOpenWindows = false;
            InvokeSystem.StartInvoke(OpenFromStack, 0.25f);
        }

        private void OpenFromStack()
        {
            _allowedOpenWindows = true;
            InvokeSystem.CancelInvoke(OpenFromStack, true);
            if(_windowsStack.Count == 0) return;
            _openedWindow = _windowsStack[0];
            _openedWindow.Closed += OnCloseButtonPressed;
            _openedWindow.Open();
        }

        public void CloseAllWindows()
        {
            if (_openedWindow != null) _openedWindow.Closed -= OnCloseButtonPressed;
            _openedWindow = null;
            
            CloseWindow(_openedOverStackWindow);
            _openedOverStackWindow = null;
            
            for (var i = 0; i < _windowsStack.Count; i++)
            {
                var window = _windowsStack[i];
                if (CloseWindow(window))
                {
                    _windowsStack.Remove(window);
                    i--;
                }
            }
            _camera.SetRenderType(GameCamera.RenderType.Default);
        }

        public void CloseWindow<T>()
        {
            var window = _all.FirstOrDefault(w => w.GetType() == typeof(T));
            CloseWindow(window);
            _openedWindow = _all.FirstOrDefault(w => w.GetType() == typeof(T));
        }

        public bool CloseWindow(BaseWindow window)
        {
            if (window == null) return false;
            if (window.IsClosed) return true;
            window.Close();
            
            _openedWindow = null;
            if (_openedOverStackWindow == window) _openedOverStackWindow = null;
            
            WindowClosedEvent?.Invoke(window);
            
            if (_windowsStack.Contains(window)) _windowsStack.Remove(window);

            OpenNextWindow();
            return true;
        }

        public BaseWindow GetWindow<T>()
        {
            return _all.FirstOrDefault(w => w.GetType() == typeof(T));
        }

        public BaseGamePlayElement GetGamePlayElement<T>()
        {
            return _gamePlayElements.Find(e => e.GetType() == typeof(T));
        }
        
        public BaseGamePlayElement GetGamePlayElement(GamePlayElement element)
        {
            return _gamePlayElements.Find(e => e.Type == element);
        }

        public void SetGamePlayElementActive(GamePlayElement type, bool value)
        {
            if (type != GamePlayElement.None)
            {
                var element = GetGamePlayElement(type);
                if (element) element.SetActive(value);
                return;
            }

            foreach (var element in _gamePlayElements.Where(e => !e.AlwaysVisible))
            {
                element.SetActive(value);
            }
        }

        public bool IsWindowOpened()
        {
            if (_openedWindow && _openedWindow.IsClosed) _openedWindow = null;
            if (_openedOverStackWindow && _openedOverStackWindow.IsClosed) _openedOverStackWindow = null;
            return _openedOverStackWindow != null || _openedWindow != null || _windowsStack.Count > 0;
        }

        public BaseWindow GetOpenedWindow()
        {
            return _openedWindow;
        }

        public void OpenStartWindows()
        {
            var windows = _all.Where(w => w.OpenAtStart);
            foreach (var window in windows)
            {
                OpenWindow(window);
            }
        }
    }
}