using System;
using _Game.Scripts.Balance;
using _Game.Scripts.GameProgresses;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using _Game.Scripts.Ui.Base.Animations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.Ui.AdPlacements
{
    public class BaseAdButton : BaseUIView
    {
        protected enum ButtonState
        {
            Pause,
            Showing,
            Opening,
            Using
        }

        [SerializeField] private Image _timerProgress;
        [SerializeField] private BaseButton _button;
        
        [Inject] protected WindowsSystem Windows;
        [Inject] protected GameBalanceConfigs Balance;
        [Inject] protected GameFlags Flags;
        [Inject] protected GameProgressFactory ProgressFactory;

        private RectTransform _rect;
        private PositionGuiAnim _anim;
        protected ButtonState State;

        protected GameProgress Progress;
        public GameProgress ProgressButton => Progress;

        public virtual void Init()
        {
            _button.SetCallback(ShowWindow);
            
            State = ButtonState.Pause;
            _rect = transform.GetChild(0).GetComponent<RectTransform>();
            _anim = GetComponentInChildren<PositionGuiAnim>();

            this.Deactivate();
        }

        protected void SetState(ButtonState state)
        {
            if (State == state) return;
            State = state;
        }

        protected virtual void Play()
        {
            State = ButtonState.Showing;
            Show(OnOpened);
            this.Activate();
        }
        
        public void Show()
        {
            gameObject.Activate();
        }
        
        public void Show(Action callback)
        {
            _rect.anchoredPosition = _anim.DefaultPos;
            _rect.localScale = Vector3.one;
            _timerProgress.fillAmount = 1;
            _anim.PlayOpenAnimation(_rect, callback);
        }
        
        protected virtual void OnOpened()
        {
            State = ButtonState.Opening;
        }

        public void ForceHide()
        {
            this.Deactivate();
        }

        public virtual void Hide()
        {
            _anim.PlayCloseAnimation(_rect, OnHided);
        }

        protected virtual void OnHided()
        {
            this.Deactivate();
        }
        
        protected void OnShowingUpdate()
        {
            _timerProgress.fillAmount = 1 - Progress.ProgressValue;
        }
        
        protected virtual void ShowWindow()
        {
        }

        public virtual void ResetGameLogic()
        {
        }
    }
}