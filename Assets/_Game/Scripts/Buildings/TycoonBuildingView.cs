using System;
using _Game.Scripts.Systems.Input;

namespace _Game.Scripts.Buildings
{
	public class TycoonBuildingView : BaseBuildingView
	{
		public Action<TycoonBuildingView> OnSelected;
		public Action<TycoonBuildingView> OnPressed;

		protected override void ResultClickBuilding(ButtonCollider button)
		{
			if (IsUnlocked) return;
			base.ResultClickBuilding(button);
		}
	}
}