using System.Collections.Generic;
using _Game.Scripts.Ui.ResourceBubbles;
using UnityEngine;

namespace _Game.Scripts.ResourceBubbles
{
    public class ResourceBubbleConfigs : MonoBehaviour
    {
        [SerializeField] private List<ResourceBubbleSetup> _configs;

        public List<ResourceBubbleSetup> Configs => _configs;
    }
}