using System;
using UnityEngine;

namespace _Game.Scripts.View.Fx
{
    public class CustomFXAnchor : MonoBehaviour
    {
        public FXAnchor Anchor;
    }

    [Serializable]
    public class FXAnchor
    {
        public Vector3 Scale = Vector3.one;
        public Vector3 Position = Vector3.zero;
        public Vector3 Rotation = Vector3.zero;
    }
}