using System;
using _Game.Scripts.ScriptableObjects;

namespace _Game.Scripts.Systems.Ads
{
    public abstract class AdWrapper
    {
        public Action<AdSystem.AdResult, int, string, string, string> OnRewardEnded;
        public Action<AdSystem.AdResult, int> OnInterEnded;
        public Action<string, string> OnRewardLoadFailed;
        public abstract void Init(ProjectSettings settings);
        public abstract void ShowReward();
        public abstract void ShowInter();
        public abstract void ShowBanner();
        public abstract void HideBanner();
        public abstract bool RewardAvailable();
        public abstract bool InterAvailable();
        public abstract void ShowDebugger();
    }
}