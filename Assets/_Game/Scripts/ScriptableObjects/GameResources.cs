using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.ScriptableObjects
{
    /// <summary>
    /// Используется для хранения ресурсов
    /// </summary>
    [CreateAssetMenu(fileName = "GameResource", menuName = "_Game/GameResources", order = 1)]
    public class GameResources : ScriptableObjectInstaller
    {
        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private List<RewardSpriteConfig> _rewardIcons;

        [SerializeField] private Sprite _placeholder;
        [SerializeField] private Sprite _placeholderAvatar;
        [SerializeField] private Material _grayscaleSpriteMaterial;
        
        public string[] LoadingTexts;
        
        [Header("Character Text Emotions")]
        public string[] QueueTextEmotions;
        public string[] TouchReactionTexts;
        public Material GrayscaleSpriteMaterial => _grayscaleSpriteMaterial;

        private static GameResources _instance;
        public static GameResources Instance
        {
            get
            {
                if (_instance == null) _instance = Resources.Load<GameResources>(nameof(GameResources));
                return _instance;
            }
        }
        
        public Sprite GetSprite<T>(T type) where T: Enum
        {
            var result = _sprites.FirstOrDefault(s => s.name.Equals(type.ToString()));
            return result == null ? _placeholder : result;
        }
        
        public Sprite GetSprite(string key)
        {
            var result = _sprites.FirstOrDefault(s => s.name == key);
            return result == null ? _placeholder : result;
        }
        
        public Sprite GetAvatarIconSprite(string key)
        {
            var result = _sprites.FirstOrDefault(s => s.name == $"Character{key}Icon");
            return result == null ? _placeholderAvatar : result;
        }
        
        public Sprite GetAvatarSprite(string key)
        {
            var result = _sprites.FirstOrDefault(s => s.name == $"Character{key}");
            return result == null ? _placeholderAvatar : result;
        }

        public RewardSpriteConfig GetRewardSpriteConfig(GameParamType type)
        {
            return _rewardIcons.FirstOrDefault(c => c.Type == type);
        }

        [Button("Parse")]
        public void Parse()
        {
            _sprites = Resources.LoadAll<Sprite>("Sprites").ToList();

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        [Serializable]
        public class RewardSpriteConfig
        {
            public GameParamType Type;
            public Sprite Sprite1;
            public Sprite Sprite2;
        }
    }
}