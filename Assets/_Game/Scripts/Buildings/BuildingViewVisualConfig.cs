using _Game.Scripts.Tools;
using _Game.Scripts.View.Base;
using UnityEngine;

namespace _Game.Scripts.Buildings
{
    public class BuildingViewVisualConfig : BaseView
    {
        [SerializeField] private GameObject _activeContainer;
        [SerializeField] private GameObject _inactiveContainer;
        
        [SerializeField] private MeshRenderer[] _meshRenders;
        [SerializeField] private Material _activeMaterial;
        [SerializeField] private Material _inActiveMaterial;

        public override void Init()
        {
            //MOCK
        }

        public void ShowActive()
        {
            _activeContainer.Activate();
            _inactiveContainer.Deactivate();
            foreach (var meshRender in _meshRenders)
            {
                meshRender.material = _activeMaterial;
            }
        }
        
        public void ShowInactive()
        {
            _activeContainer.Deactivate();
            _inactiveContainer.Activate();
            foreach (var meshRender in _meshRenders)
            {
                meshRender.material = _inActiveMaterial;
            }
        }
    }
}