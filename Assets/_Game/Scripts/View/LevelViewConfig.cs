using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.View
{
    public class LevelViewConfig : MonoBehaviour
    {
        [SerializeField] private Transform _boundsMin;
        [SerializeField] private Transform _boundsMax;
        [SerializeField] private Vector3 _defaultPos;
        [SerializeField] private float _deltaBounds = 0.5f;
        [SerializeField] private bool _zoomCamOnStartLevel = true;
        [SerializeField] private List<Environments> _environmentViews;
        
        public Vector3 BoundsMin => _boundsMin.localPosition;
        public Vector3 BoundsMax => _boundsMax.localPosition;
        public Vector3 DefaultPos => _defaultPos;
        public float DeltaBounds => _deltaBounds;
        public bool ZoomCamOnStartLevel => _zoomCamOnStartLevel;
        public List<Environments> EnvironmentViews => _environmentViews;

        [Serializable]
        public class Environments
        {
            public int MinCount;
            public int MaxCount;
        }
    }
}