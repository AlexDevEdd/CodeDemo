using _Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Game.Scripts.Systems.Ads
{
    public class MaxWrapper : AdWrapper
    {
        private ProjectSettings _projectSettings;

        public override void Init(ProjectSettings settings)
        {
            _projectSettings = settings;
            InitSDK();
        }

        private void InitSDK()
        {
            LoadRewardedAd();
        }

        private void LoadRewardedAd()
        {
        }
        
        public override void ShowReward()
        {
            Debug.Log("3. ShowReward");
        }

        public override void ShowInter()
        {
        }

        public override void ShowBanner()
        {
        }
        
        public override void HideBanner()
        {
        }

        public override bool RewardAvailable()
        {
            return true;
        }

        public override bool InterAvailable()
        {
            return true;
        }

        public override void ShowDebugger()
        {
        }
        
        private void OnRewardedAdLoadedEvent(string arg1/*, MaxSdkBase.AdInfo arg2*/)
        {
        }
        
        private void OnRewardedAdLoadFailedEvent(string adUnitId/*, MaxSdkBase.ErrorInfo errorInfo*/)
        {
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId/*, MaxSdkBase.AdInfo adInfo*/)
        {
        }
       
        private void OnRewardedAdFailedToDisplayEvent(string adUnitId/*, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo*/)
        {
        }

        private void OnRewardedAdClickedEvent(string adUnitId/*, MaxSdkBase.AdInfo adInfo*/)
        {
        }
        
        private void OnRewardedAdHiddenEvent(string adUnitId/*, MaxSdkBase.AdInfo adInfo*/)
        {
        }
        
        private void OnRewardedAdReceivedRewardEvent(string adUnitId/*, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo*/)
        {
        }
        
        private void OnRewardedAdRevenuePaidEvent(string adUnitId/*, MaxSdkBase.AdInfo adInfo*/)
        {
        }
    }
}
