using System;
using System.Collections.Generic;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public class CharacterCardSaveData
    {
        public List<CharacterCardData> Items;

        public CharacterCardSaveData()
        {
            Items = new List<CharacterCardData>();
        }
    }

    [Serializable]
    public class CharacterCardData
    {
        public int Id;
        public int Amount;
        public int Level;
    }
}