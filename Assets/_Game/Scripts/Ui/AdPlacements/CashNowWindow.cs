using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ResourceBubbles;
using _Game.Scripts.Systems;
using _Game.Scripts.Systems.Ads;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Systems.Currencies;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.Ui.AdPlacements
{
    public class CashNowWindow : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private TextMeshProUGUI _text1;
        [SerializeField] private TextMeshProUGUI _text2;

        [SerializeField] private Image _timerProgress;

        [SerializeField] private BaseButton _noButton;
        [SerializeField] private BaseButton _yesButton;
        
        [Inject] private CashNowAdButton _cashNowAdButton;
        [Inject] private AdSystem _ad;
        [Inject] private ICurrencySystem _currency;
        [Inject] private ResourceBubbleFactory _resourceBubbleFactory;
        [Inject] private GlobalCurrencySystem _currencySystem;
        
        private string _watchStr;
        
        public override void Init()
        {
            _noButton.SetCallback(Close);
            _yesButton.SetCallback(OnPressedYes);
            base.Init();
        }

        public override void UpdateLocalization()
        {
            _title.text = "CASH_NOW_TITLE".ToLocalized();
            _text1.text = "CASH_NOW_TEXT1".ToLocalized();
            _text2.text = "CASH_NOW_TEXT2".ToLocalized();
            _noButton.SetText("NOT_YET".ToLocalized());
            _yesButton.SetText("DO_IT".ToLocalized());
            _watchStr = "WATCH".ToLocalized();
            base.UpdateLocalization();
        }

        public override void Open(params object[] list)
        {
            _amount.text = $"<sprite name=Soft>{_cashNowAdButton.Amount.ToFormattedString()}";
            _timerProgress.fillAmount = 1;
            
            _cashNowAdButton.ProgressButton.UpdatedEvent += OnShowingUpdate;
            _cashNowAdButton.ProgressButton.CompletedEvent += OnShowingComplete;
            
            RedrawWatchButton();
            
            base.Open(list);
        }
        
        private void CheckAvailableAd()
        {
            if (_ad.AdIsAvailable())
            {
                _yesButton.SetInteractable(true);
            }
            else
            {
                _yesButton.SetInteractable(false);
                InvokeSystem.StartInvoke(CheckAvailableAd, 1f);
            }
        }
        
        private void OnShowingUpdate()
        {
            _timerProgress.fillAmount = 1 - _cashNowAdButton.ProgressButton.ProgressValue;
        }

        private void OnShowingComplete()
        {
            Close();
        }
        
        private void OnPressedYes()
        {
            _yesButton.SetInteractable(false);
            _ad.OnCompleted += OnAdCompleted;
            _ad.ShowReward(AdPlacement.FastCash, SpendCurrencyPlace.FastCashWindow);
        }
        
        private void OnAdCompleted(bool success)
        {
            _ad.OnCompleted -= OnAdCompleted;
            RedrawWatchButton();
            
            if (!success) return;
            
            PlaySound(GameSoundType.RewardReceive);
            
            _currency.AddCurrency(GameParamType.Soft, _cashNowAdButton.Amount, GetRewardPlace.FastCashWindow);
            _resourceBubbleFactory.SpawnResourceBubble(GameParamType.Soft, _cashNowAdButton.Amount, _amount.rectTransform.position);
            _cashNowAdButton.Hide();
            
            AppEventProvider.TriggerEvent(AppEventType.Tasks, GameEvents.GetFastCash);
            
            Close();
        }
        
        private void RedrawWatchButton()
        {
            _yesButton.SetInteractable(true);
            
            var tickets = _currencySystem.Tickets.Value;
            var text = tickets == 0 
                ? "DO_IT".ToLocalized() 
                : $"<sprite name=AdTickets>1/{tickets} {_watchStr}";
            _yesButton.SetText(text);
            CheckAvailableAd();
        }

        public override void Close()
        {
            _cashNowAdButton.ProgressButton.UpdatedEvent -= OnShowingUpdate;
            _cashNowAdButton.ProgressButton.CompletedEvent -= OnShowingComplete;
            _cashNowAdButton.Hide();
            base.Close();
        }
    }
}