using System;
using System.Collections.Generic;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public class InAppSaveData
    {
        public List<InAppData> Items;

        public InAppSaveData()
        {
            Items = new List<InAppData>();
        }
    }

    [Serializable]
    public class InAppData
    {
        public int Id;
        public int Count;
        public float Timer;
    }
}