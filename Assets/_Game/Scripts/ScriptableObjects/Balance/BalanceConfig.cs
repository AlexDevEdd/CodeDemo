using System;
using System.Collections.Generic;
using _Game.Scripts.Balance.Attributes;
using _Game.Scripts.Enums;
using _Game.Scripts.Systems;
using _Game.Scripts.Tools;
using UnityEngine;

namespace _Game.Scripts.ScriptableObjects.Balance
{
    [CreateAssetMenu(fileName = "BalanceConfig", menuName = "_Game/BalanceConfig", order = 0)]
    [TableSheetName("Constants")]
    public class BalanceConfig : BaseBalanceConfig, IParsable
    {
        [Header("CommonSettings")] 
        public GameParamType SoftCurrency;
        public int AutomateCharacterId;
        public float CameraSensitivityValue = 0.55f;

        public float BusTimer;
        public int BusPrisoners;
        public int PrestigeForPrisoner;
        
        public int CardCharacterNeedMinTime;
        public int CardCharacterNeedMaxTime;

        public float RegistrationWaitingTime;
        public float ReceptionVisitorTime;
        public float ReceptionSignatureChance;

        public float NeedTimeCycle;
        public float NeedTimeActivate;
        public float BriefingTimeCycle;

        [Header("UnlockSettings")] 
        [SerializeField] public int StoreOpenRegion;
        [SerializeField] public int CharacterCardsOpenRegion;
        [SerializeField] public int RoomCardsOpenRegion;
        [SerializeField] public int OrdersOpenedRegion;
        [SerializeField] public int PrisonerBossesOpenedRegion;
        [SerializeField] public int AutomateOpenRegion;
   
        [Header("HardPricesSettings")] 
        [SerializeField] public int ReturnWindowHardPrice;
        [SerializeField] public int OrdersHardPrice;
        
        [Header("OrdersSettings")] 
        [SerializeField] public int OrdersTimer;
        [SerializeField] public int OrdersMinValue;
        [SerializeField] public int OrdersMaxValue;

        [Header("ClickerRoomRewards")] 
        [SerializeField] public int TokensMin;
        [SerializeField] public int TokensMax;
        [SerializeField] public int RoomCardsMin;
        [SerializeField] public int RoomCardsMax;
        [SerializeField] public int CharacterCardsMin;
        [SerializeField] public int CharacterCardsMax;
        [SerializeField] public int HardCurrencyChance;
        [SerializeField] public int HardMin;
        [SerializeField] public int HardMax;

        [Header("TapGameSettings")] 
        [SerializeField] public int TapGameDuration;
        [SerializeField] public float TapGameCharacterSpeed;

        [Header("OfflineSettings")]
        public int MaxOfflineTime;
        public int ReturnWindowShowOfflineTime;

        [Header("AdPlacementSettings")] 
        public float AdButtonTime;
        public float CashNowStartGameTime;
        public float CashNowTime;
        public float CashNowValue;
        public float SpeedUpStartGameTime;
        public float SpeedUpTime;
        public float TokensNowStartGameTime;
        public float TokensNowTime;
        public float TokensNowValue;

        public List<LevelConfig> LevelConfigs;
        public List<TimelineEventConfig> GameEvents;
        public List<CurrencyConfig> Currencies;
        public List<ProgressionConfig> Progressions;
        public List<RoomCardConfig> RoomsCards;
        public List<RoomCardUpgradeConfig> RoomCardUpgradeConfigs;
        public List<RoomViewConfig> ClickerRoomViews;
        public List<BuildingItemConfig> TycoonBuildings;
        public List<RoomViewConfig> TycoonRoomViews;
        public List<RoomViewConfig> ArcadeRoomViews;
        public List<RoomViewCapConfig> RoomViewCaps;
        public List<TycoonRoomItemConfig> TycoonRoomItems;
        public List<RoomViewMapConfig> RoomViewMapConfigs;
        public List<CharacterCardConfig> CharacterCards;
        public List<CharacterCardUpgradeConfig> CharacterCardUpgradeConfigs;
        public List<BoostConfig> Boosts;
        public List<TaskConfig> Tasks;
        public List<TaskConfig> RandomTasks;
        public List<TutorialConfig> Tutorial;
        public List<InAppsConfig> InApps;
        public List<BoxConfig> Boxes;
        public List<MiniGameConfig> MiniGames;
        public List<BossInfoConfig> BossConfigs;
        public List<MailboxConfig> MailboxConfigs;
        public List<TycoonRoomValueConfig> TycoonRoomValues;
        public List<TaskConfig> SevenDayChallengeTasks;
        public List<RankConfig> RankConfigs;

        public void OnParsed()
        {
            foreach (var task in Tasks) task.OnParsed();
            foreach (var task in RandomTasks) task.OnParsed();
            foreach (var games in MiniGames) games.OnParsed();
            foreach (var mail in MailboxConfigs ) mail.OnParsed();
            foreach (var task in SevenDayChallengeTasks) task.OnParsed();
        }
    }

    [Serializable]
    public class BossInfoConfig
    {
        public int Id;
        public string Name;
        public string Nickname;
        public string DateOfBirth;
        public string Height;
        public string Weight;
        public string Signs;
        public string Sprite;
    }

    [Serializable]
    public class LevelConfig
    {
        public int Id;
        public string SceneName;
        public int TutorialId;
        public string LevelPlacement;
    }

    [Serializable]
    public class TimelineEventConfig
    {
        public string Name;
        public float Time;
    }

    [Serializable]
    public class CurrencyConfig
    {
        public GameParamType ParamType;
        public int Value;
    }

    [Serializable]
    public class ProgressionConfig
    {
        public GameParamType ParamType;
        public float BaseValue;
        public float K1;
        public float K2;
    }

    [Serializable]
    public class RoomCardConfig
    {
        public int CardId;
        public int RegionUnlocked;
        public GameParamType PriceType;
        public float Price;
        public int RoomRegionLevel;
        public int RoomWeight;
        public string Sprite;
    }

    [Serializable]
    public class BuildingItemConfig
    {
        public int Region;
        public int BuildingId;
        public int BuildTime;
        public int Price;
        public GameParamType PriceType;
        public TaskAction Action;
        public string ActionParam1;
        public string ActionParam2;
    }

    [Serializable]
    public class RoomViewConfig
    {
        public int RoomId;
        public int BuildingId;
        public bool Automated;
        public GameParamType UnlockPriceType;
        public float UnlockPrice;
        public GameParamType PriceType;
        public float Price;
        public float PriceUpgrade1;
        public float PriceUpgrade2;
        public float UpgradeTimer1;
        public float UpgradeTimer2;
        public int StartLevel;
        public float CycleDelay;
        public float ProductionCycle;
        public GameParamType IncomeType;
        public float Income;
        public GameParamType ResourcesType;
        public int ResourcesIn;
        public int ResourcesOut;
        public GameParamType RewardType;
        public int RewardValue;
        public string RewardSprite;
        public bool ShowQueue;
        public int BuildTime;
        public int ReqRank;
        public TaskAction Action;
        public string ActionParam1;
        public string ActionParam2;
    }

    [Serializable]
    public class RoomViewCapConfig
    {
        public int Cap;
        public float K;
    }

    [Serializable]
    public class TycoonRoomItemConfig
    {
        public int RoomId;
        public int ItemId;
        public int StartLevel;
        public int RequiredLevel;
        public int CapLevel;
        public GameParamType ParamType;
        public float Value;
        public GameParamType ParamType1;
        public float Value1;
        public GameParamType ParamType2;
        public float Value2;
        public GameParamType PriceType;
        public float Price;
        public GameParamType PriceParamType;
        public string Name;
        public string ParamSprite;
        public bool ShowUI;
        public bool ShowView;
        public bool UseItemCap;
        public CharacterSkin CharacterSkin;
        public int ItemStep1;
        public int ItemStep2;
        public int ItemStep3;
    }

    [Serializable]
    public class RoomViewMapConfig
    {
        public int RegionId;
        public int RoomId;
        public int Cap;
        public int AutomateLevel;
        public float AutomatePrice;
        public int PrevRoomId;
    }

    [Serializable]
    public class CharacterCardConfig
    {
        public int CardId;
        public int RegionId;
        public int RegionUnlocked;
        public CharacterCardQuality Quality;
        public int RoomId;
        public int StartLevel;
        public int Box1Weight;
        public int Box2Weight;
        public int Box3Weight;
        public int RoomRegionLevel;
        public int RoomWeight;
        public CharacterCardGroup CharacterGroup;
        public CharacterSkin Skin;
        public float MoveSpeed;
    }

    [Serializable]
    public class CharacterCardUpgradeConfig
    {
        public CharacterCardQuality Quality;
        public int Level;
        public int Price;
        public int Cards;
    }
    
    [Serializable]
    public class RoomCardUpgradeConfig
    {
        public int Level;
        public int Price;
        public int Cards;
    }

    [Serializable]
    public enum CharacterCardQuality
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    [Serializable]
    public enum CharacterCardGroup
    {
        Legendary,
        Epic,
        TappingPower,
        Room
    }

    [Serializable]
    public class BoostConfig
    {
        public int Id;
        public BoostSourceType BoostSourceType;
        public int SourceId;
        public int TargetId;
        public int Sort;
        public GameParamType ParamType;
        public float BaseValue;
        public float K;
        public float Duration;
        public float NextDurationDelta;
        public int MaxCount;
        public bool IncludePrevDuration;
        public bool Global;
        public bool ClearMigrates;
    }

    [Serializable]
    public enum BoostSourceType
    {
        Ad,
        RoomCard,
        CharacterCard,
        ClickerRoomView
    }

    [Serializable]
    public class TaskConfig : IParsable
    {
        public int Region;
        public int ID;
        public TaskClass Class;
        public GameEvents TaskType;
        public float Count;
        public string Var1;
        public string Var2;
        public string RewardText;
        public string Token;
        public string Sprite;
        public TaskAction ActionStart;
        public TaskAction ActionCompleted;
        public TaskAction ActionFinish;
        public string ActionParam1;
        public string ActionParam2;
        [SkipTableValue] public List<Reward> Rewards = new();

        public void OnParsed()
        {
            if (string.IsNullOrEmpty(RewardText)) return;

            var parseRewards = RewardText.Split(';');
            foreach (var rewardStr in parseRewards)
            {
                if (rewardStr.IsNullOrEmpty()) continue;
                var parse = rewardStr.Split('-');
                Enum.TryParse(parse[0], true, out GameParamType type);
                float.TryParse(parse[1], out var amount);

                var id = 0;
                if (parse.Length >= 3) int.TryParse(parse[2], out id);
                if (type == GameParamType.None || amount == 0) return;

                var reward = new Reward
                {
                    Type = type,
                    Amount = amount,
                    Id = id
                };
                Rewards.Add(reward);
            }
        }
    }

    [Serializable]
    public enum TaskClass
    {
        Task,
        DailyQuest
    }

    [Serializable]
    public enum TaskAction
    {
        None,
        RateGame,
        StartTutorial,
        SetFlags
    }

    [Serializable]
    public class DailyQuestsRewardConfig
    {
        public GameParamType ParamType;
        public int Value;
    }

    [Serializable]
    public class TutorialConfig
    {
        public int Region;
        public int StepId;
        public TutorialStepAction StepAction;
        public string StepActionParam1;
        public string StepActionParam2;
        public int SpeakerId;
        public TutorialSpeakerPosition Position;
        public string AnalyticsStep;
        public bool Save;
        public bool CloseWindows;
        public bool ResetCamera;
    }

    [Serializable]
    public enum TutorialStepAction
    {
        None,
        ShowTips,
        HideGamePlayElement,
        ShowGamePlayElement,
        SetCameraScale,
        FocusGamePlayElement,
        TutorialFinish,
        RestorePurchases,
        NewTaskWindowOpen,
        FocusOnTycoonRoom,
        FocusOnClickerRoom,
        TycoonRoomWindowOpen,
        TycoonRoomWindowPressButton,
        TycoonRoomWindowClose,
        CompletedTaskListItemPressButton,
        FinishGameTask,
        BlockPrisoners,
        UnblockPrisoners,
        BlockWarden,
        FocusOnWarden,
        BuildClickerRoom,
        PressClickerRoomBubbleButton,
        FirstClickerCashReady,
        TutorialPlayerStart,
        TutorialPlayerFinish,
        ClickerRoomWindowOpen,
        ClickerRoomWindowPressButton,
        ClickerRoomWindowClose,
        LeaveLocationWindowPressButton,
        ShowFingerInTapGame,
        ChooseSkinWindowOpen,
        StartTutorial,
        ShowClickerRoomTablet,
        LeaveLocationWindowOpen,
        MiniGameLoaded,
        ClickerRoomWindowPressAutomateButton,
        AutomateWindowOpened,
        AutomateWindowPressButton,
        AutomateWindowPressClose,
        SoftCurrencyCollected,
        ClickerRoomWindowPressMaxButton,
        ClickerRoomBuilt,
        FocusOnClickerRoomTablet,
        CharacterCardsWindowOpen,
        CharacterCardsInfoWindowOpen,
        CharacterCardsInfoWindowPressButton,
        RoomCardsWindowOpen,
        RoomCardsWindowPressButton,
        ShopWindowOpen,
        ShopWindowPressTokensButton,
        ShopWindowPressCashButton,
        SetGameFlag,
        BoxOpeningWindowClosed,
        CharacterCardsWindowPressCharacterButton,
        CharacterCardsInfoWindowClose,
        OrderWindowOpen,
        FirstPrisonerArrived,
        FirstPrisonerArrivedToWaitingRoom,
        SpeedUpPoliceCars,
        RestoreSpeedPoliceCars,
        FocusOnDepartureWarden,
        FirstPrisonerCameToBlockC,
        ShowBonusX2,
        TutorialScene,
        FirstPrisonerTycoonRoom1StartCycle,
        SpawnPoliceCar,
        PausePrisonerProgress,
        PlayFirstPrisonerProgress,
        FocusOnFirstPrisonerToTycoonRoom2,
        CycleEndPrisonersTycoonRoom1,
        CycleEndPrisonersTycoonRoom2,
        HideWorldScreenArrow,
        ArrivedFirstPrisonerToTycoonRoom1,
        FocusOnFirstPrisonerToClickerRoom1,
        PlayPrisonerTimer,
        ShowTasks,
        HideTutorialArrow,
        ShowTipsInRunner,
        StartCameraFocused,
        BlockAllInput,
        UnblockInput,
        BossAppear,
        FocusOnBossCar,
        FocusOnBoss,
        FocusOnLoadPrisonersPoint,
        FollowWarden,
        FocusOnBubble,
        RegisterPrisoners,
        UpdateBadge,
        BlockAllButtons,
        UnlockButtons,
        CheckAvalaiblePrisoners,
        BlockScroll,
        UnblockScroll,
        BlockTap,
        UnblockTap,
        ResetFocus,
        ShowSoftTutorCollectTask,
        PrisonerArrivedToTycoonRoom,
        EndPrisonerTimer,
        PausePrisonerTimer,
        ShowFingerOnTaskItem,
        RemoveCameraTarget,
        StartPoliceCarTimer,
        SpawnSwat,
        SwatLeft,
        ShowTycoonRoom,
        TycoonRoomWindowPressAutomateButton,
        TycoonRoomWindowShowButton,
        CharacterCardsWindowActivateHorizontaLayout,
        FocusOnPrisoner,
        CheckCashForUpgradeTycoonItem,
        LockTycoonRoom,
        UnlockTycoonRoom,
        FocusChallengeButton,
        FocusOnCar,
        PauseGarageDriver,
        FocusOnReturnCar,
        FollowCar,
        FocusOnCriminal,
        PauseRegistrationOfficer,
        FocusRegistrationOfficer,
        FocusOnPrisonerToWardenPoint,
        ShowTask,
        CameraFocused,
        TycoonRoomWindowChooseItem,
        TycoonRoomWindowEnableGrid,
        ShopWindowPressFreeBoxButton,
        AutomateWindowPressAssignCharacter,
        AutomateWindowCloseButton,
        FocusOnPrisonerToCell,
        EndWardenCycle,
        FocusOnSwat,
        ResetCarSpeed,
        EndRegistrationOfficer,
        FocusOnKitchenQueue,
        ArrivedPrisonerToWarden,
        CheckStartTutorial300,
        FinishTutorial300,
        X2WindowOpen,
        X2WindowPressButton,
        X2WindowClose,
        FocusOnAssignCharacter,
        CheckRoomUpgraded,
        TycoonRoomWindowPressShowUpgradeButton,
        TycoonRoomWindowPressUpgradeButton,
        EndTutorial,
        PauseSwatDrivers,
        PlaySwatDrivers,
        EndSwatDrivers,
        ResetSwatCarSpeed,
        FocusOnBuilding,
        FocusOnReturnSwatCar,
        SendAnalyticEvent,
        TapProgressBar,
        PauseProgressBar,
        UnPauseProgressBar,
        MapWindowOpen,
        MapWindowPressButton,
        FocusOnHungryRegistrationOfficer,
        FollowToHungryRegistrationOfficer,
        SevenDayWindowOpen,
        SevenDayWindowPressButton,
        FocusOnSevenDayButton
    }

    [Serializable]
    public enum TutorialSpeakerPosition
    {
        Left,
        Right
    }

    [Serializable]
    public class InAppsConfig
    {
        public int Region;
        public int Id;
        public bool Update;
        public bool Save;
        public bool NonConsumable;
        public GameParamType Type;
        public int Amount;
        public GameParamType PriceType;
        public int Price;
        public int Count;
        public int Timer;
        public string SKUGroup;
        public string SKU;
        public string ShopId;
        public string ShopIdFull;
    }

    [Serializable]
    public class BoxConfig
    {
        public int Id;
        public float CoinChance;
        public int CoinMin;
        public int CoinMax;
        public float CommonCardsChance;
        public int CommonCardsMin;
        public int CommonCardsMax;
        public float RareCardsChance;
        public int RareCardsMin;
        public int RareCardsMax;
        public float EpicCardsChance;
        public int EpicCardsMin;
        public int EpicCardsMax;
    }

    [Serializable]
    public class MiniGameConfig
    {
        public int Id;
        public int Difficulty;
        public string RewardsText;
        public int BossId;

        [SkipTableValue] public List<MiniGameStageConfig> Stages = new();
        [SkipTableValue] public List<Reward> Rewards = new();

        public void OnParsed()
        {
            if (string.IsNullOrEmpty(RewardsText)) return;

            var rows = RewardsText.Split(';');
            var stageId = 1;

            foreach (var rowStr in rows)
            {
                if (rowStr.IsNullOrEmpty()) continue;

                var newStageConfig = new MiniGameStageConfig
                {
                    StageId = stageId
                };

                Stages.Add(newStageConfig);

                var rewards = rowStr.Split(',');
                foreach (var rewardStr in rewards)
                {
                    var parse = rewardStr.Split('-');
                    Enum.TryParse(parse[0], true, out GameParamType type);
                    float.TryParse(parse[1], out var amount);

                    var id = 0;
                    if (parse.Length >= 3) int.TryParse(parse[2], out id);
                    if (type == GameParamType.None || amount == 0) return;

                    var reward = new Reward
                    {
                        Type = type,
                        Amount = amount,
                        Id = id
                    };

                    newStageConfig.Rewards.Add(reward);
                    Rewards.Add(reward);
                }

                stageId++;
            }
        }
    }

    [Serializable]
    public class MiniGameStageConfig
    {
        public int StageId;
        [SkipTableValue] public List<Reward> Rewards = new();
    }

    [Serializable]
    public class MapEventPointConfig
    {
        public int Id;
        public string Name;
        public float Price;
        public int BuildingTime;
        public int CycleDelay;
        public int StartLevel;
        public int Cap;
    }

    [Serializable]
    public class MapConfig
    {
        public int Id;
        public int Corpse;
    }

    [Serializable]
    public class DailyRewardConfig
    {
        public int Day;
        public string DayText;
        public string RewardText;

        [SkipTableValue] public Reward Reward;

        public void OnParsed()
        {
            if (string.IsNullOrEmpty(RewardText)) return;

            var parse = RewardText.Split('-');
            Enum.TryParse(parse[0], true, out GameParamType type);
            float.TryParse(parse[1], out var amount);

            var id = 0;
            if (parse.Length >= 3) int.TryParse(parse[2], out id);

            if (type == GameParamType.None || amount == 0) return;

            Reward = new Reward
            {
                Type = type,
                Amount = amount,
                Id = id
            };
        }
    }

    [Serializable]
    public class ImprovementsConfig
    {
        public int Id;
        public int PrevId;
        public float Price;
        public float Time;
        public int BoostId;
        public int Level;
        public string Token;
        public string TokenDesc;
        public string Icon;
        public string Group;
    }

    [Serializable]
    public class RoomItemCapConfig
    {
        public int RoomId;
        public int Id;
        public int CharacterLevel;
        public int LevelCap;
    }

    [Serializable]
    public enum MailboxTrigger
    {
        None,
        RemoveLastSaves   
    }

    [Serializable]
    public class MailboxConfig
    {
        public int Id;
        public string Title;
        public string Text;
        public MailboxTrigger Trigger;
        public string RewardText;

        [SkipTableValue] public List<Reward> Rewards = new();
        
        //TODO сделать единый метод парса для всех конфигов баланса
        public void OnParsed()
        {
            if (string.IsNullOrEmpty(RewardText)) return;

            var parseRewards = RewardText.Split(';');
            foreach (var rewardStr in parseRewards)
            {
                if (rewardStr.IsNullOrEmpty()) continue;
                var parse = rewardStr.Split('-');
                Enum.TryParse(parse[0], true, out GameParamType type);
                float.TryParse(parse[1], out var amount);

                var id = 0;
                if (parse.Length >= 3) int.TryParse(parse[2], out id);
                if (type == GameParamType.None || amount == 0) return;

                var reward = new Reward
                {
                    Type = type,
                    Amount = amount,
                    Id = id
                };
                Rewards.Add(reward);
            }
        }
    }
    
    [Serializable]
    public class TycoonRoomValueConfig
    {
        public int RoomId;
        public int ItemId;
        public int Level;
        public int Price;
        public float Value;
        public int PrestigeNeed;
        public GameParamType RewardType;
        public int RewardValue;
        public string RewardSprite;
    }
        
    [Serializable]
    public class CollectionItemConfig
    {
        public int Region;
        public int Id;
        public string Token;
        public string TokenClosed;
    }

    [Serializable]
    public class RankConfig
    {
        public int Id;
        public int Exp;
    }
}