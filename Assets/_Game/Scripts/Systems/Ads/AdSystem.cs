using System;
using _Game.Scripts.Enums;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Systems.Currencies;
using _Game.Scripts.Tools;
using Zenject;

namespace _Game.Scripts.Systems.Ads
{
    public class AdSystem
    {
        public enum AdResult
        {
            Watched,
            Clicked,
            Canceled,
            ErrorLoaded,
            ErrorDisplay,
            Hidden
        }

        public Action<bool> OnCompleted;
        public Action OnAdDisplayed;

        [Inject] private ProjectSettings _settings;
        [Inject] private GlobalCurrencySystem _currency;
        [Inject] private GameFlags _flags;

        private AdWrapper _currentWrapper;
        private AdPlacement _currentPlacement;

        public bool AdIsShowing { get; private set; }
        private string _noAdStr;

        public void Init()
        {
            if (!_settings.Ads) return;
            _currentWrapper = new MaxWrapper();
            _currentWrapper.OnRewardEnded += OnRewardEnded;
            _currentWrapper.OnRewardLoadFailed += OnRewardLoadFailed;
            _currentWrapper.Init(_settings);
        }

        public void ShowDebugger()
        {
            _currentWrapper.ShowDebugger();
        }

        public bool AdIsAvailable()
        {
            if (!_settings.Ads) return true;
            return _flags.Has(GameFlag.AllAdsRemoved) || _currentWrapper.RewardAvailable();
        }

        public void ShowReward(AdPlacement placement, SpendCurrencyPlace place)
        {
            _currentPlacement = placement;
            
            if (_currency.Tickets.Value > 0)
            {
                OnCompleted?.Invoke(true);
                _currency.SpendCurrency(GameParamType.AdTickets, 1, place);
                return;
            }

#if UNITY_EDITOR
            OnCompleted?.Invoke(true);
            return;
#endif
            if (_flags.Has(GameFlag.AllAdsRemoved))
            {
                OnCompleted?.Invoke(true);
                AppEventProvider.TriggerEvent(AppEventType.Analytics, GameEvents.RewardGetWithoutAd, _currentPlacement);
                return;
            }

            if (!_currentWrapper.RewardAvailable())
            {
                OnCompleted?.Invoke(false);
                return;
            }
            
            AdIsShowing = true;
            _currentWrapper.ShowReward();
            AppEventProvider.TriggerEvent(AppEventType.Analytics, GameEvents.RewardStart, _currentPlacement);
        }

        private void OnRewardLoadFailed(string errorCode, string network)
        {
            OnCompleted?.Invoke(false);
            AppEventProvider.TriggerEvent(AppEventType.Analytics, GameEvents.RewardErrorLoaded, _currentPlacement, errorCode, network);
        }

        private void OnRewardEnded(AdResult result, int code, string network, string adIdentifier, string creativeId)
        {
            AdIsShowing = false;
            switch (result)
            {
                case AdResult.ErrorDisplay:
                    AppEventProvider.TriggerEvent(AppEventType.Analytics, GameEvents.RewardErrorDisplay, _currentPlacement, code, network);
                    OnCompleted?.Invoke(false);
                    break;
                
                case AdResult.Clicked:
                case AdResult.Watched:
                    AppEventProvider.TriggerEvent(AppEventType.Analytics, GameEvents.RewardFinish, _currentPlacement, adIdentifier, creativeId);
                    OnCompleted?.Invoke(true);
                    break;
                
                default:
                    OnCompleted?.Invoke(false);
                    break;
            }
        }

        public void SetShowingFlag()
        {
            AdIsShowing = true;
        }
        
        public void ResetShowingFlag()
        {
            AdIsShowing = false;
        }
    }
}