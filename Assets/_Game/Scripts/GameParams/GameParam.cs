using System;
using _Game.Scripts.Enums;
using Zenject;

namespace _Game.Scripts.GameParams
{
    public class GameParam
    {
        public event Action UpdatedEvent;
        
        public GameParamOwnerType Owner { get; private set; }
        public GameParamType Type { get; private set; }
        public float Value { get; private set; }

        public class Pool : MemoryPool<GameParamOwnerType, GameParamType, float, GameParam>
        {
            protected override void Reinitialize(GameParamOwnerType owner, 
                GameParamType type, 
                float target,
                GameParam param)
            {
                param.Init(owner, type, target);
            }
        }

        private void Init(GameParamOwnerType owner, GameParamType type, float value)
        {
            Owner = owner;
            Type = type;
            Value = value;
        }

        public void Change(float value)
        {
            Value += value;
            UpdatedEvent?.Invoke();
        }

        public virtual void SetValue(float value, bool updateEvent = true)
        {
            Value = value;
            if (updateEvent) UpdatedEvent?.Invoke();
        }

        public void ResetGameLogic()
        {
            Value = 0;
            UpdatedEvent = null;
        }
    }

    public enum GameParamOwnerType
    {
        InAppSystem,
        LevelSystem,
        CurrencySystem,
        GameSystem,
        GameTaskSystem,
        TutorialSystem,
        InAppOffersSystem,
        SevenDayChallengeSystem
    }
}