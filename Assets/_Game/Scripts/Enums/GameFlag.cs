using System;

namespace _Game.Scripts.Enums
{
    [Serializable]
    public enum GameFlag
    {
        None, //0
        TutorialRunning,//1
        TutorialFinished,//2
        AllAdsRemoved,//3
        InAppWasBought,//4
        FreeBoxIsAvailable,//5
        StoreOpened,//6
        CharacterCardsOpened,
        SevenDayChallengeUnlocked,
        RandomTasksUnlocked,
        AdCashNowUnlocked,
        AdSpeedUpUnlocked
    }
}