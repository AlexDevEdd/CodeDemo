using System;
using System.Collections.Generic;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public class BuildingsViewSaveData
    {
        public List<BuildingViewData> Items;
        
        public BuildingsViewSaveData()
        {
            Items = new List<BuildingViewData>();
        }
    }

    [Serializable]
    public class BuildingViewData
    {
        public int Region;
        public int BuildingId;
        public bool IsUnlocked;
    }
}