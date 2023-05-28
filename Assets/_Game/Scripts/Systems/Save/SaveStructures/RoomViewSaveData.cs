using System;
using System.Collections.Generic;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public class RoomViewSaveData
    {
        public List<RoomViewData> Items;
        
        public RoomViewSaveData()
        {
            Items = new List<RoomViewData>();
        }
    }

    [Serializable]
    public class RoomViewData
    {
        public int RoomId;
        public int Level;
        public int AutomateId;
        public float CurrentPrice;
    }
}