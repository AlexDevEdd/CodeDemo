using System;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public struct GameSettingsData
    {
        public bool IsMuteMusic;
        public bool IsMuteSound;
        public bool IsDisablePushNotifications;
    }
}