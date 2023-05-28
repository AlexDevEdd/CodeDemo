using System;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public struct ImprovementData
    {
        public int Id;
        public float Time;
        public bool IsBought;
    }
}