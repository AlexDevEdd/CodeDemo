using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Systems.Save.SaveStructures;
using UnityEngine;

namespace _Game.Scripts.GameParams
{
    /// <summary>
    /// Класс хранения параметров объектов
    /// </summary>
    public class GameParamFactory : ILoadSystem
    {
        private readonly GameParam.Pool _paramsPool;

        private const string KEY = "GAME_PARAMS"; 
        
        public List<GameParam> GlobalParams { get; private set; } = new();
        public List<GameParam> LevelParams { get; private set; } = new();

        public GameParamFactory(GameParam.Pool paramsPool)
        {
            _paramsPool = paramsPool;
        }

        public GameParam CreateParam(GameParamOwnerType owner, GameParamType type, float value, bool global = true, bool redraw = true)
        {
            var param = global 
                ? GlobalParams.Find(p => p.Owner == owner && p.Type == type)
                : LevelParams.Find(p => p.Owner == owner && p.Type == type);

            if (param != null)
            {
                if (!redraw) return param;
                param.ResetGameLogic();
                param.SetValue(value);
                return param;
            }
            
            param = _paramsPool.Spawn(owner, type, value);

            if (global)
            {
                GlobalParams.Add(param);
            }
            else
            {
                LevelParams.Add(param);
            }
            
            return param;
        }

        public void RemoveParam(GameParam param)
        {
            param.ResetGameLogic();
            _paramsPool.Despawn(param);
            GlobalParams.Remove(param);
            LevelParams.Remove(param);
        }

        public void Remove(GameParamOwnerType owner, GameParamType type)
        {
            var param = GetParam(owner, type);
            if (param != null) RemoveParam(param);
        }

        public GameParam GetParam(GameParamOwnerType owner, GameParamType type)
        {
            var globalParam = GlobalParams.FirstOrDefault(p => p.Owner == owner && p.Type == type);
            if (globalParam != null) return globalParam;
            var param = LevelParams.FirstOrDefault(p => p.Owner == owner && p.Type == type);
            return param;
        }
        
        public float GetParamValue(GameParamOwnerType owner, GameParamType type)
        {
            var globalParam = GlobalParams.FirstOrDefault(p => p.Owner == owner && p.Type == type)?.Value;
            if (globalParam != null) return globalParam.Value;
            var param = LevelParams.FirstOrDefault(p => p.Owner == owner && p.Type == type)?.Value ?? 0;
            return param;
        }

        public void UpdateParamValue(GameParamOwnerType owner, GameParamType type, float value)
        {
            GetParam(owner, type)?.SetValue(value);
        }
        
        public void Load(bool clearData)
        {
            if (!PlayerPrefs.HasKey(KEY)) return;

            var json = PlayerPrefs.GetString(KEY);
            var data = JsonUtility.FromJson<GameParamsSaveData>(json);
            
            if (data == null || clearData) return;
            
            foreach (var paramData in data.Items)
            {
                var param = GetParam(paramData.Owner, paramData.Type);
                param?.SetValue(paramData.Value);
            }
        }

        public SaveData GetData()
        {
            var data = new GameParamsSaveData(); 
            foreach (var param in LevelParams)
            {
                data.Items.Add(new GameParamsData
                {
                    Owner = param.Owner,
                    Type = param.Type, 
                    Value = param.Value
                });
            }

            var json = JsonUtility.ToJson(data);
            return new SaveData { Key = KEY, JsonData = json };
        }
    }
}