using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.View.Fx
{
	public class TaskCompleteFxFactory
	{
		private readonly TaskCompleteFx.Pool _pool;
		private readonly List<TaskCompleteFx> _items = new();

		public TaskCompleteFxFactory(TaskCompleteFx.Pool pool)
		{
			_pool = pool;
		}
        
		public TaskCompleteFx Spawn(RectTransform rt)
		{
			var fx = _pool.Spawn();
			fx.transform.SetParent(rt);
			fx.RectTransform.anchoredPosition = Vector2.zero;
			_items.Add(fx);
			
			return fx;
		}
        
		public void Remove(TaskCompleteFx fx)
		{
			_items.Remove(fx);
			
			if (_pool.InactiveItems.Contains(fx)) return;
			
			_pool.Despawn(fx);
		}
	}
}