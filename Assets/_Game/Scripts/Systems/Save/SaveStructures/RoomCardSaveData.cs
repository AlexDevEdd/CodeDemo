using System;
using System.Collections.Generic;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public class RoomCardSaveData
    {
        public List<RoomCardData> Items;
        
        public RoomCardSaveData()
        {
            Items = new List<RoomCardData>();
        }
    }

    [Serializable]
    public class RoomCardData
    {
        public int CardId;
        public int Level;
        public int Amount;
    }
}