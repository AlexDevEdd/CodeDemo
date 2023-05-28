using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Systems.Save.SaveStructures;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Buildings
{
    public class TycoonBuildingsFactory : IInitGameLogic, ILoadSystem
    {
        [Inject] private GameBalanceConfigs _balance;
        [Inject] private LevelSystem _levelSystem;
        
        private const string KEY = "BUILDINGS"; 
        
        public List<TycoonBuildingView> Views { get; private set; } = new();
        
        public void InitGameLogic()
        {
            //MOCK
        }

        public TycoonBuildingView GetById(int id)
        {
            var res = Views.FirstOrDefault(r => r.Config.BuildingId == id);
            return res;
        }
        
        public void Unlock(BaseBuildingView view)
        {
            view.Unlock();
            // AppEventProvider.TriggerEvent(GameEvents.BuildTycoonBuilding, view.Config.BuildingId);
        }
        
        public void Load(bool clearData)
        {
            if (!PlayerPrefs.HasKey(KEY)) return;

            var json = PlayerPrefs.GetString(KEY);
            var data = JsonUtility.FromJson<BuildingsViewSaveData>(json);
            
            if (data == null || clearData) return;

            foreach (var viewData in data.Items)
            {
                var view = GetById(viewData.BuildingId);
                view?.SetData(viewData.IsUnlocked);
            }
        }

        public SaveData GetData()
        {
            var lvlId = _levelSystem.GetLevelId();
            var data = new BuildingsViewSaveData();
            
            foreach (var view in Views)
            {
                data.Items.Add(new BuildingViewData
                {
                    Region = lvlId,
                    BuildingId = view.BuildingId,
                    IsUnlocked = view.IsUnlocked
                });
            }
        
            var json = JsonUtility.ToJson(data);
            return new SaveData { Key = KEY, JsonData = json };
        }
    }
}