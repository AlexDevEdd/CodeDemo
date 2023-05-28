using System;
using System.Collections.Generic;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public class CollectionsData
    {
        public List<CollectionItemData> Items;

        public CollectionsData()
        {
            Items = new List<CollectionItemData>();
        }
    }
    
    [Serializable]
    public struct CollectionItemData
    {
        public int Id;
        public int Region;
    }
}