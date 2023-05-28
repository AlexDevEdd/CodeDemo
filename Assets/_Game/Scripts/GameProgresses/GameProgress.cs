using System;
using _Game.Scripts.Enums;
using Zenject;

namespace _Game.Scripts.GameProgresses
{
    public enum GameProgressState
    {
        None,
        Active,
        Completed,
        Paused
    }
    
    public class GameProgress
    {
        public event Action PlayEvent, UpdatedEvent, CompletedEvent;

        private float _delta;
        private bool _looped;
        private bool _updatable;

        public GameProgressOwnerType Owner { get; private set; }
        public int OwnerId{ get; private set; }
        public GameProgressState State { get; private set; }
        public GameParamType Type { get; private set; }

        public bool IsActive => State == GameProgressState.Active;
        public bool IsCompleted => State == GameProgressState.Completed;
        public bool IsPaused => State == GameProgressState.Paused;

        public bool Save { get; private set; }
        public float CurrentValue { get; private set; }

        public float TargetValue { get; private set; }
        public float ProgressValue => CurrentValue != 0 ? CurrentValue / TargetValue : 0;

        public float LeftValue => TargetValue - CurrentValue;

        public bool Looped => _looped;
        public bool Updatable => _updatable;

        public class Pool : MemoryPool<GameProgressOwnerType, GameParamType, float, int, bool, bool, bool, GameProgress>
        {
            protected override void Reinitialize(GameProgressOwnerType owner, 
                GameParamType type,
                float target,
                int id,
                bool looped,
                bool updatable,
                bool save,
                GameProgress progress)
            {
                progress?.Init(owner, type, target, id, looped, updatable, save);
            }
        }
        
        private void Init(GameProgressOwnerType owner, GameParamType type, float target, int id, bool looped, bool updatable, bool save)
        {
            Owner = owner;
            OwnerId = id;
            Type = type;
            TargetValue = target;
            State = GameProgressState.Active;
            _looped = looped;
            _updatable = updatable;
            Save = save;
        }
        
        public GameProgress Play()
        {
            if (State is GameProgressState.Paused or GameProgressState.Completed)
            {
                SetState(GameProgressState.Active);
                PlayEvent?.Invoke();
            }

            return this;
        }

        public GameProgress Pause()
        {
            if (State == GameProgressState.Active)
            {
                SetState(GameProgressState.Paused);
            }

            return this;
        }
        
        public void Tick(float deltaTime)
        {
            if (State != GameProgressState.Active || !_updatable) return;
            Change(deltaTime);
        }

        public void Change(float delta, bool checkProgress = true)
        {
            CurrentValue += delta;
            UpdatedEvent?.Invoke();
            if (checkProgress) CheckProgress();
        }

        public void SetCurrent(float current, bool checkProgress = true)
        {
            CurrentValue = current;
            if (checkProgress) CheckProgress();
        }

        public void SetTarget(float target)
        {
            TargetValue = target;
            CheckProgress();
        }

        private void CheckProgress()
        {
            if (State != GameProgressState.Active) return;
            if (CurrentValue < TargetValue)
            {
                return;
            }

            if (_looped)
            {
                CurrentValue = 0;
            }
            else
            {
                CurrentValue = TargetValue;
                SetState(GameProgressState.Completed);
            }
            
            CompletedEvent?.Invoke();
        }

        public GameProgress Reset(bool checkProgress = true)
        {
            State = GameProgressState.Active;
            CurrentValue = 0;
            if (!checkProgress) return this;
            CheckProgress();
            UpdatedEvent?.Invoke();
            return this;
        }
        
        private void SetState(GameProgressState state)
        {
            if (State == state) return;
            State = state;
        }

        public override string ToString()
        {
            return $"{CurrentValue} / {TargetValue}";
        }

        public void Clear()
        {
            Owner = GameProgressOwnerType.None;
            State = GameProgressState.None;
            Type = GameParamType.None;
            CurrentValue = 0;
            TargetValue = 0;
            Save = false;
            _looped = false;
            _updatable = false;
        }

        public void ResetCallbacks()
        {
            PlayEvent = null;
            UpdatedEvent = null;
            CompletedEvent = null;
        }
        
        public void SetData(float current, float target, GameProgressState state, 
            bool looped = true, 
            bool updatable = true, 
            bool save = false)
        {
            CurrentValue = current;
            TargetValue = target;
            State = state;
            Save = save;
            _looped = looped;
            _updatable = updatable;
        }
    }
}