using System;
using _Game.Scripts.Systems.Base;
using Zenject;

namespace _Game.Scripts.View.Fx.CashVfx
{
    public class CashFx : BaseFx
    {
        private Action<CashFx> _callback;
        
        public class Pool : MonoMemoryPool<CashFx>
        {
            protected override void Reinitialize(CashFx view)
            {
                view.Init();
            }
        }

        public override void Init()
        {
            InvokeSystem.StartInvoke(Remove, 2f);
        }

        private void Remove()
        {
            _callback?.Invoke(this);
        }

        public void SetCallback(Action<CashFx> callback)
        {
            _callback = callback;
        }
    }
}