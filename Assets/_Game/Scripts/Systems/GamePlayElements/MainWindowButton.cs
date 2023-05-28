using _Game.Scripts.TextMessages;
using _Game.Scripts.Tools;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Systems.GamePlayElements
{
    public class MainWindowButton : BaseGamePlayElement
    {
        [Inject] private TextMessageFactory _textMessages;
        
        private bool _unlocked;
        private string _unlockTextStr;
        
        protected override void OnPressedButton()
        {
            //MOCK
        }

        public override void SetActive(bool value, Material material = null, bool hide = false)
        {
            _unlocked = value;
            base.SetActive(value, material, hide);
        }

        public override void UpdateLocalization()
        {
            _unlockTextStr = "PLAY_TO_UNLOCK".ToLocalized();
            base.UpdateLocalization();
        }
    }
}