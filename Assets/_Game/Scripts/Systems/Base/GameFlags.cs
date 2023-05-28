using System;
using System.Collections.Generic;
using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Systems.Save.SaveStructures;
using UnityEngine;

namespace _Game.Scripts.Systems.Base
{
    public class GameFlags : ILoadSystem
    {
        private const string KEY = "FLAGS"; 
        
        public Action<GameFlag> OnFlagSet;
        public List<GameFlag> GlobalFlags { get; private set; } = new();
        public List<GameFlag> LevelFlags { get; private set; } = new();

        public void Set(GameFlag flag, bool global = true, bool updateFlags = true)
        {
            if (Has(flag)) return;
            if (global)
            {
                GlobalFlags.Add(flag);
            }
            else
            {
                LevelFlags.Add(flag);
            }
            if (updateFlags) OnFlagSet?.Invoke(flag);
        }

        public bool Has(GameFlag flag)
        {
            return GlobalFlags.Contains(flag) || LevelFlags.Contains(flag);
        }

        public void Remove(GameFlag flag, bool global = true)
        {
            if (global)
            {
                GlobalFlags.Remove(flag);
            }
            else
            {
                LevelFlags.Remove(flag);
            }
        }
        
        public void Load(bool clearData)
        {
            if (!PlayerPrefs.HasKey(KEY)) return;

            var json = PlayerPrefs.GetString(KEY);
            var data = JsonUtility.FromJson<GameFlagsSaveData>(json);
            
            if (data == null || clearData) return;

            foreach (var flag in data.Items)
            {
                Set(flag, false);
            }
        }

        public SaveData GetData()
        {
            var data = new GameFlagsSaveData(); 
            foreach (var newFlag in LevelFlags)
            {
                data.Items.Add(newFlag);
            }

            var json = JsonUtility.ToJson(data);
            return new SaveData { Key = KEY, JsonData = json };
        }
    }
}