using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.View.Fx.CashVfx
{
    public class CashFxFactory
    {
        private readonly CashFx.Pool _pool;
        private readonly List<CashFx> _vfx = new();
        
        public CashFxFactory(CashFx.Pool pool)
        {
            _pool = pool;
        }
        
        public void Spawn(Transform transform)
        {
            var view = _pool.Spawn();
            view.Show(transform);
            view.SetCallback(Remove);
            _vfx.Add(view);
        }
        
        private void Remove(CashFx text)
        {
            _pool.Despawn(text);
            _vfx.Remove(text);
        }
    }
}