using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Systems
{
    public class SceneSystem : MonoBehaviour
    {
        private AsyncOperation _operation = new();
        
        public void LoadScene(string sceneName, Action callback = null)
        {
            StartCoroutine(AsyncLoading(sceneName, callback));
        }
        
        private IEnumerator AsyncLoading(string aSceneName, Action callback)
        {
            var sceneName = "_Game/Scenes/Levels/" + aSceneName;
            _operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            _operation.allowSceneActivation = false;

            while (!_operation.isDone)
            {
                if (_operation.progress >= 0.9f)
                {
                    _operation.allowSceneActivation = true;
                }
				
                yield return null;
            }
            
            callback?.Invoke();
        }

        public void UnloadScene(string sceneName, Action callback = null)
        {
            StartCoroutine(AsyncUnloading(sceneName, callback));
        }

        private IEnumerator AsyncUnloading(string aSceneName, Action callback)
        { 
            _operation = SceneManager.UnloadSceneAsync("_Game/Scenes/Levels/" + aSceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            _operation.allowSceneActivation = false;

            while (!_operation.isDone)
            {
                if (_operation.progress >= 0.9f)
                {
                    _operation.allowSceneActivation = true;
                }

                yield return null;
            }
            callback?.Invoke();
        }
    }
}