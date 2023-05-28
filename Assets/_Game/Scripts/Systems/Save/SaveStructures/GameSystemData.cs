using System;
using System.Collections.Generic;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
    [Serializable]
    public class GameSystemData
    {
        public List<GameParamsSaveData> Params;
        public List<GameProgressSaveData> Progresses;
    }
}