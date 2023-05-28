using System;
using System.Collections.Generic;
using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Systems.Currencies;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public class Reward
    {
        public GameParamType Type;
        public int Id;
        public float Amount;
    }

    public class RewardSystem : IInitGameLogic
    {
        [Inject] private GlobalCurrencySystem _globalCurrency;
        [Inject] private WindowsSystem _windows;
        [Inject] private GameBalanceConfigs _balance;

        public void InitGameLogic()
        {
            //MOCK
        }

        public void ClaimRewards(List<Reward> rewards, GetRewardPlace place, bool box = true)
        {
            if (rewards.Count == 1)
            {
                ClaimReward(rewards[0], place);
            }
            else
            {
                foreach (var reward in rewards)
                    ClaimReward(reward, place);
            }
        }

        public void ClaimReward(Reward reward, GetRewardPlace place)
        {
            //MOCK
        }
    }
}