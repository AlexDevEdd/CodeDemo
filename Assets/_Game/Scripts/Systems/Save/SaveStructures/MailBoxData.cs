using System;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public struct MailBoxData
    {
        public int Id;
        public bool Read;
        public bool Claim;
        public bool Removed;
    }
}