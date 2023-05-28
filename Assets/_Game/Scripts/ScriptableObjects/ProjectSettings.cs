using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.ScriptableObjects
{
    /// <summary>
    /// Используется хранения настроек проекта
    /// </summary>
    [CreateAssetMenu(fileName = "ProjectSettings", menuName = "_Game/ProjectSettings", order = 1)]
    public class ProjectSettings : ScriptableObjectInstaller
    {
        private static ProjectSettings _instance;
        public static ProjectSettings Instance
        {
            get
            {
                if (_instance == null) _instance = Resources.Load<ProjectSettings>(nameof(ProjectSettings));
                return _instance;
            }
        }
        
        [Header("Publish settings")] 
        public bool Internet;
        public bool CheckVersion;
        public bool Cheats;
        public bool Saves;
        public bool Tutorial;
        public bool UnlockFlags;
        public bool Analytics;
        public bool Ads;
        public bool GameServices;
        public bool ValidatePurchases;
        public bool PushNotifications;

        [Header("Game Settings")] 
        public int SaveVersion;
        public int MapId;
        
        [Header("Internet settings")]
        public string Host;
        public int InternetCheckConnectionDelay;
            
        [Header("Links")]
        public string TermsLink;
        public string PrivacyLink;
        public string AppStoreLink;
        public string GooglePlayLink;

        [Header("SDK Keys")] 
        public string AppsflyerKey;
        public string MaxAndroidKey;
        public string MaxIosKey;
        public string MaxAndroidRewardsId;
    }
}