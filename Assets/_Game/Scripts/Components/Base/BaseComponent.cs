using System;

namespace _Game.Scripts.Components.Base
{
    public class BaseComponent
    {
        public Action OnEnd;

        public virtual void Start()
        {
        }

        protected virtual void End()
        {
            OnEnd?.Invoke();
        }
    }
}