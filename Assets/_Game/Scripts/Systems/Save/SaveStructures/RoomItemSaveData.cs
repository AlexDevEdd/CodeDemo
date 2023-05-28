using System;
using System.Collections.Generic;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public class RoomItemSaveData
    {
        public List<RoomItemData> Items;
        
        public RoomItemSaveData()
        {
            Items = new List<RoomItemData>();
        }
    }

    [Serializable]
    public class RoomItemData
    {
        public int RoomId;
        public int Id;
        public int Level;
    }
}