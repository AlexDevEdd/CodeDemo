using System;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public struct ClickerCycleData
    {
        public int RoomId;
        public float Amount;
        public int ResourceIn;
        public int ResourceOut;
    }
}