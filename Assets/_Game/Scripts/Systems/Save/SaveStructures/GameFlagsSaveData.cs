using System;
using System.Collections.Generic;
using _Game.Scripts.Enums;

namespace _Game.Scripts.Systems.Save.SaveStructures
{
	[Serializable]
	public class GameFlagsSaveData
	{
		public List<GameFlag> Items;

		public GameFlagsSaveData()
		{
			Items = new List<GameFlag>();
		}
	}
}