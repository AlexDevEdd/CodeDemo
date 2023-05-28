using System;
using System.Collections.Generic;
using _Game.Scripts.Enums;
using _Game.Scripts.GameProgresses;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public class GameProgressSaveData
    {
        public List<GameProgressData> Items;
        
        public GameProgressSaveData()
        {
            Items = new List<GameProgressData>();
        }
    }

    [Serializable]
    public class GameProgressData
    {
        public GameProgressOwnerType Owner;
        public int OwnerId;
        public GameProgressState State;
        public GameParamType Type;
        public float Target;
        public float Current;
        public bool Looped;
        public bool Updatable;
        public bool Save;
    }
}