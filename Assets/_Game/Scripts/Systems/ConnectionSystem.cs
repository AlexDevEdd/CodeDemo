using System;
using System.Collections;
using System.Globalization;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace _Game.Scripts.Systems
{
	/// <summary>
	/// Класс для проверки интернет соединения при запуске и во время игры
	/// </summary>
	public class ConnectionSystem : MonoBehaviour, ITickableProjectSystem
	{
		public Action<bool> Connected;

		public DateTime ServerTime { get; private set; }

		private ProjectSettings _projectSettings;
		private float _checkInternetTimer;

		[Inject]
		public void Construct(ProjectSettings projectSettings)
		{
			_projectSettings = projectSettings;
		}

		public void RunCheckConnection(bool setTime = true)
		{
			if (_projectSettings == null)
			{
				Connected?.Invoke(false);
				return;
			}
			
			StartCoroutine(GetRequest(setTime));
		}

		private IEnumerator GetRequest(bool setTime)
		{
			using var webRequest = UnityWebRequest.Get(_projectSettings.Host);
			yield return webRequest.SendWebRequest();

			var result = false;
			if(webRequest.isDone)
			{
				if (setTime)
				{
					var results = webRequest.GetResponseHeader("date");
					if (results != null)
					{
						ServerTime = DateTime.ParseExact(results,
							"ddd, dd MMM yyyy HH:mm:ss 'GMT'",
							CultureInfo.InvariantCulture.DateTimeFormat,
							DateTimeStyles.AssumeUniversal);

						result = true;
					}
				}
			}
			
			Connected?.Invoke(result);
		}

		public void Tick(float deltaTime)
		{
			//TODO почему то не инитится
			if (_projectSettings == null) return;
			if (!_projectSettings.Internet) return;
#if UNITY_EDITOR
			ServerTime = ServerTime.AddSeconds(deltaTime / Time.timeScale);
#else
			ServerTime = ServerTime.AddSeconds(deltaTime);
#endif
		}
	}
}