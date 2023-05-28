using System;
using _Game.Scripts.Enums;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public struct MapEventData
    {
        public int Id;
        public bool Bought;
        public int CorpsesQueue;
        public int CorpsesCalled;
        public int BuildingTime;
    }
}
