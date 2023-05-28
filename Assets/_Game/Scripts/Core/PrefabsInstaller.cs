using UnityEngine;
using Zenject;

namespace _Game.Scripts.Core
{
    public class PrefabsInstaller : MonoInstaller
    {
        [SerializeField] private MonoBehaviour[] _installPrefabs;
        
        public override void InstallBindings()
        {
            foreach (var prefab in _installPrefabs)
            {
                var type = prefab.GetType();
                Container.BindInterfacesAndSelfTo(type).FromInstance(prefab).AsSingle().NonLazy();
            }
        }
    }
}