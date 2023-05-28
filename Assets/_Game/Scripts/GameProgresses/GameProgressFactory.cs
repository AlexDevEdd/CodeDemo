using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Systems.Save.SaveStructures;
using UnityEngine;

namespace _Game.Scripts.GameProgresses
{
    public class GameProgressFactory : ITickableProjectSystem, IResetGameLogic, ILoadSystem
    {
        private readonly GameProgress.Pool _progressPool;
        
        private const string KEY = "PROGRESSES"; 
        
        public List<GameProgress> GlobalProgresses { get; private set; } = new();
        public List<GameProgress> LevelProgresses { get; private set;} = new();

        public GameProgressFactory(GameProgress.Pool progressPool)
        {
            _progressPool = progressPool;
        }
        
        public void ResetGameLogic()
        {
            foreach (var progress in LevelProgresses)
            {
                progress.ResetCallbacks();
                progress.Clear();
                _progressPool.Despawn(progress);
            }
            LevelProgresses.Clear();
        }

        public GameProgress CreateProgress(GameProgressOwnerType owner,
            GameParamType type,
            float target,
            int id = 1,
            bool looped = true, 
            bool updatable = true, 
            bool global = false,
            bool save = false,
            bool resetIfExist = true)
        {
            var progress = global 
                ? GlobalProgresses.Find(p => p.Owner == owner && p.Type == type && p.OwnerId == id)
                : LevelProgresses.Find(p => p.Owner == owner && p.Type == type && p.OwnerId == id);

            if (progress != null)
            {
                if (resetIfExist) progress.Reset();
                return progress;
            }
            progress = _progressPool.Spawn(owner, type, target, id, looped, updatable, save);

            if (global)
            {
                GlobalProgresses.Add(progress);
            }
            else
            {
                LevelProgresses.Add(progress);
            }
            
            return progress;
        }

        public void RemoveProgress(GameProgress progress)
        {
            progress.Reset();

            if (!_progressPool.InactiveItems.Contains(progress))
            {
                _progressPool.Despawn(progress);
            }
            GlobalProgresses.Remove(progress);
            LevelProgresses.Remove(progress);
        }

        public GameProgress GetProgress(GameProgressOwnerType owner, GameParamType type, int id = 1)
        {
            var savedParam = GlobalProgresses.FirstOrDefault(p => p.Owner == owner && p.Type == type && p.OwnerId == id);
            if (savedParam != null) return savedParam;
            var param = LevelProgresses.FirstOrDefault(p => p.Owner == owner && p.Type == type && p.OwnerId == id);
            return param;
        }

        public void Tick(float deltaTime)
        {
            for (var i = 0; i < GlobalProgresses.Count; i++)
            {
                GlobalProgresses[i].Tick(deltaTime);
            }

            for (var i = 0; i < LevelProgresses.Count; i++)
            {
                LevelProgresses[i].Tick(deltaTime);
            }
        }
        
        public void Load(bool clearData)
        {
            if (!PlayerPrefs.HasKey(KEY)) return;

            var json = PlayerPrefs.GetString(KEY);
            var data = JsonUtility.FromJson<GameProgressSaveData>(json);
            
            if (data == null || clearData) return;

            foreach (var progressData in data.Items)
            {
                var progress = GetProgress(progressData.Owner, progressData.Type, progressData.OwnerId) ??
                               CreateProgress(progressData.Owner, progressData.Type, progressData.Target, progressData.OwnerId);
                progress?.SetData(progressData.Current, progressData.Target, progressData.State, progressData.Looped, progressData.Updatable, progressData.Save);
            }
        }

        public SaveData GetData()
        {
            var data = new GameProgressSaveData(); 
            foreach (var progress in LevelProgresses.Where(p => p.Save))
            {
                data.Items.Add(new GameProgressData
                {
                    Owner = progress.Owner,
                    OwnerId = progress.OwnerId,
                    Type = progress.Type,
                    State = progress.State,
                    Target = progress.TargetValue,
                    Current = progress.CurrentValue,
                    Looped = progress.Looped,
                    Updatable = progress.Updatable,
                    Save = progress.Save
                });
            }

            var json = JsonUtility.ToJson(data);
            return new SaveData { Key = KEY, JsonData = json };
        }
    }
}
