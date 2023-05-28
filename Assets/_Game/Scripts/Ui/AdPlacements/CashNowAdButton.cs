using _Game.Scripts.Enums;
using _Game.Scripts.GameParams;
using _Game.Scripts.GameProgresses;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Systems;
using _Game.Scripts.Systems.Base;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Ui.AdPlacements
{
    public class CashNowAdButton : BaseAdButton
    {
        [SerializeField] private ParticleSystem _vfx;
        
        //private ICalculationSystem _calculationSystem;
        private ICurrencySystem _currency;

        public float Amount { get; private set; }

        [Inject]
        public void Construct(ICurrencySystem currency)
        {
            _currency = currency;
            //_calculationSystem = calculationSystem;
        }

        public override void Init()
        {
            base.Init();

            Progress = ProgressFactory.CreateProgress(GameProgressOwnerType.CashNowButton,
                GameParamType.Timer,
                Balance.DefaultBalance.AdButtonTime,
                1,
                false);
            Progress.UpdatedEvent += OnShowingUpdate;
            Progress.CompletedEvent += Hide;
            Progress.Pause();

            if (Flags.Has(GameFlag.AdCashNowUnlocked))
            {
                InvokeSystem.StartInvoke(Play, Balance.DefaultBalance.CashNowStartGameTime);
            }
            else
            {
                Flags.OnFlagSet += OnFlagSet;
            }
        }

        private void OnFlagSet(GameFlag flag)
        {
            if (flag != GameFlag.AdCashNowUnlocked) return;
            InvokeSystem.StartInvoke(Play, Balance.DefaultBalance.CashNowStartGameTime);
        }

        protected override void Play()
        {
            Amount = Balance.DefaultBalance.CashNowValue / 30 * _currency.GetCurrencyValue(GameParamType.Income);
            if (Amount == 0)
            {
                InvokeSystem.StartInvoke(Play, Balance.DefaultBalance.CashNowTime);
                return;
            }
            base.Play();
        }

        protected override void OnOpened()
        {
            base.OnOpened();
            Progress.Play();
            _vfx.Play();
        }
        
        public override void Hide()
        {
            _vfx.Stop();
            base.Hide();
            Progress.Reset();
            Progress.Pause();
            InvokeSystem.StartInvoke(Play, Balance.DefaultBalance.CashNowTime);
        }
        
        protected override void ShowWindow()
        {
            Windows.OpenWindow<CashNowWindow>();
            base.ShowWindow();
        }

        public override void ResetGameLogic()
        {
            Flags.OnFlagSet -= OnFlagSet;
            base.ResetGameLogic();
        }
    }
}