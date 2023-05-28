using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Systems;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base.Animations;
using _Game.Scripts.Ui.Buttons;
using _Game.Scripts.Ui.WindowTabs;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Ui.Base
{
    public class BaseWindow : BaseUIView
    {
        private enum WindowState
        {
            Opened,
            Closed,
            PlayingAnim
        }

        public static event Action<int> SoundEvent = delegate { };
        
        public Action<BaseWindow> Opened;
        public Action<BaseWindow> Closed;
     
        [SerializeField] private bool _addToOpenedStack = true;
        [SerializeField] private bool _toOpenedOverStack;
        [SerializeField] private bool _ignoreAnimations;
        [SerializeField] private bool _ignoreInitClose;
        [SerializeField] private bool _renderUiOnly;
        [SerializeField] private bool _openAtStart;

        [Inject] protected LevelSystem _levelSystem;
        
        private const string INNER_WINDOW = "Window";
        
        private WindowState _state;

        private CloseWindowButton _closeButton;
        private WindowBack _windowBack;
        private List<BaseUIAnimation> _windowAnimations = new();
        
        protected WindowTabView[] Tabs;
        protected RectTransform WindowRect;

        public bool Inited { get; private set; }
        public bool AddToOpenedStack => _addToOpenedStack;
        public bool ToOpenOverStack => _toOpenedOverStack;
        public bool IsOpened => _state == WindowState.Opened;
        public bool IsClosed => _state == WindowState.Closed;
        public bool RenderUiOnly => _renderUiOnly;
        public bool OpenAtStart => _openAtStart;
        public bool IgnoreAnimations => _ignoreAnimations;
        public BaseButton CloseButton => _closeButton;

        public virtual void Init()
        {
            //MOCK
        }

        public virtual bool CanOpen(params object[] list)
        {
            return true;
        }

        public virtual void ForceOpen(params object[] list)
        {
            SetState(WindowState.Opened);
            if(Tabs.Length > 0) SelectTab(Tabs[0]);
            this.Activate();
            OnOpened();
        }

        public virtual void Open(params object[] list)
        {
            SetState(WindowState.Opened);
            if(Tabs.Length > 0) SelectTab(Tabs[0]);
            if (_windowAnimations.Count == 0)
            {
                this.Activate();
                OnOpened();
            }
            else
            {
                this.Activate();
                SetState(WindowState.PlayingAnim);
                foreach (var windowAnim in _windowAnimations)
                {
                    windowAnim.PlayOpenAnimation(WindowRect, OnOpened);   
                }
            }
            if(this is not MainWindow &&
               this is not ICurrencyWindow) PlaySound(GameSoundType.Window);
        }

        public WindowTabType SelectedTab()
        {
            if(Tabs == null || Tabs.Length == 0) return WindowTabType.None;
            var tab = Tabs.FirstOrDefault(i => i.IsSelected);
            return tab ? tab.Type : WindowTabType.None;
        }
        
        public virtual void SelectTab(WindowTabType type)
        {
            if(Tabs.Length == 0) return;
            foreach (var tab in Tabs)
            {
                tab.Select(tab.Type == type);
            }
        }
        
        public virtual void SelectTab(WindowTabView selected)
        {
            if(Tabs.Length == 0) return;
            if(!Tabs.Contains(selected)) return;

            foreach (var tab in Tabs)
            {
                tab.Select(tab == selected);
            }
        }

        private void OnOpened()
        {
            Opened?.Invoke(this);
        }

        public virtual void Close()
        {
            if(this is not MainWindow && this is not ICurrencyWindow && !IsClosed) PlaySound(GameSoundType.Window);
            if (_windowAnimations.Count == 0)
            {
                OnClosed();
            }
            else
            {
                SetState(WindowState.PlayingAnim);
                foreach (var windowAnim in _windowAnimations)
                {
                    windowAnim.PlayCloseAnimation(WindowRect, OnClosed);   
                }
            }
        }

        public virtual void ForceClose()
        {
            this.Deactivate();
            SetState(WindowState.Closed);
            Closed?.Invoke(this);
        }

        public void PlayOpenAnimation()
        {
            if (_state == WindowState.Opened) return;
            foreach (var windowAnim in _windowAnimations)
            {
                windowAnim.PlayOpenAnimation(WindowRect);   
            }
        }
        
        public void PlayCloseAnimation()
        {
            if (_state == WindowState.Closed) return;
            foreach (var windowAnim in _windowAnimations)
            {
                windowAnim.PlayCloseAnimation(WindowRect);   
            }
        }

        private void OnClosed()
        {
            this.Deactivate();
            SetState(WindowState.Closed);
            Closed?.Invoke(this);
        }

        private void SetState(WindowState state)
        {
            _state = state;
        }

        public virtual void UpdateLocalization()
        {
        }
        
        public virtual void Tick(float deltaTime)
        {
        }

        public virtual void ResetGameLogic()
        {
        }

        protected void PlaySound(GameSoundType type)
        {
            SoundEvent?.Invoke((int)type);
        }

        public virtual BaseButton GetTutorialButton(int value = 0)
        {
            return null;
        }
    }
}