namespace _Game.Scripts.Enums
{
    public enum GameEvents
    {
        None,
        //События рекламы
        RewardGetWithoutAd,
        RewardStart,
        RewardShowed,
        RewardClicked,
        RewardFinish,
        RewardCanceled,
        RewardErrorLoaded,
        RewardErrorDisplay,

        //События покупок
        FirstInAppPurchased,
        InAppPurchased,

        //События офферов
        ShowOffer,
        ClickOffer,
        PurchaseOffer,
        
        //Типы и события тасков

        //События аналитики
        Install,
        Launch,
        TutorialStep,
        TutorialFinish,
        LevelStart,
        LevelFinish,
        CameraFocused,
        BuildBuilding,
        CharacterUnlock,
        GetAnyCard,
        ItemUpgradeComplete,
        BuildTycoonRoom,
        Login,
        SpendCurrency,
        GetFastCash,
        CollectCurrency,
        OpenBox,
        TaskAvailable,
        TaskStart,
        TaskFinished,
        WatchAdVideo,
        TaskCompleted
    }
}
