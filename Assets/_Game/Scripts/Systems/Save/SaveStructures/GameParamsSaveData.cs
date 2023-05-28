using System;
using System.Collections.Generic;
using _Game.Scripts.Enums;
using _Game.Scripts.GameParams;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public class GameParamsSaveData
    {
        public List<GameParamsData> Items;
        
        public GameParamsSaveData()
        {
            Items = new List<GameParamsData>();
        }
    }

    [Serializable]
    public class GameParamsData
    {
        public GameParamOwnerType Owner;
        public GameParamType Type;
        public float Value;
    }
}