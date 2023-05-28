using System;
using System.Collections.Generic;
using _Game.Scripts.Enums;
using _Game.Scripts.View.Fx;
using UnityEngine;

namespace _Game.Scripts.View
{
    public class VisualView : MonoBehaviour
    {
        //MOCK
    }

    [Serializable]
    public class VisualConfig
    {
        public CharacterSkin CharacterSkin;
        public VehicleType VehicleType;
        public Mesh Mesh;
        public Material[] Materials;
        public List<VisualAnimationConfig> Animations;
        public int ID = 0;
        public GameObject Object;
        public FXAnchor Anchor;
        public Material TransparentMaterial;
    }

    [Serializable]
    public class VisualAnimationConfig
    {
        //public CharacterAnimationType Type;
        public string Animation;

        public int Hash
        {
            get
            {
                if (_hash == -1) _hash = Animator.StringToHash(Animation);
                return _hash;
            }
        }

        private int _hash = -1;
    }
}