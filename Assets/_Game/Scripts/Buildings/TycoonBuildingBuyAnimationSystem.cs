using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.View.Fx;
using Zenject;

namespace _Game.Scripts.Buildings
{
	public class TycoonBuildingBuyAnimationSystem : IInitSceneObjects
	{
		[Inject] private GameCamera _camera;
		[Inject] private TycoonBuildingsFactory _buildingsFactory;
		[Inject] private WindowsSystem _windowsSystem;

		public void InitSceneObjects()
		{
			foreach (var view in _buildingsFactory.Views.Where(x => !x.IsUnlocked))
			{
				view.UnlockEvent += PlayBuyAnimation;
			}
		}

		public void PlayBuyAnimation(BaseBuildingView view)
		{
			view.UnlockEvent -= PlayBuyAnimation;
			_windowsSystem.CloseAllWindows();

			_camera.Focus(view.transform.position, 10f, true, saveSize: true, callback: () =>
			{
				InvokeSystem.StartInvoke(_camera.SetLastScale, 1.0f);
			});
		}
	}
}