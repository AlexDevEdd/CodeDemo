using UnityEngine;
using Zenject;

namespace _Game.Scripts.View.Fx
{
	public class TaskCompleteFx : BaseFx
	{
		[SerializeField] private RectTransform _rectTransform;

		public RectTransform RectTransform => _rectTransform;
		
		public class Pool : MonoMemoryPool<TaskCompleteFx>
		{
		}
	}
}