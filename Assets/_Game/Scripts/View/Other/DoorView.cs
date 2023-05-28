using _Game.Scripts.Systems.Base;
using UnityEngine;

namespace _Game.Scripts.View.Other
{
    public class DoorView : MonoBehaviour
    {
        [SerializeField] private string _anim_prefix = "door_";
        
        private Animation _animation;
        private bool _opened;

        private void Init()
        {
            _animation = GetComponent<Animation>();
        }
        
        public void Open()
        {
            if(!_animation) Init();
            InvokeSystem.CancelInvoke(Close);
            InvokeSystem.StartInvoke(Close, 2f);
            if(_opened) return;
            _animation.Play($"{_anim_prefix}open");
            _opened = true;
        }

        private void Close()
        {
            if(_opened == false) return;
            if(!_animation) return;
            _animation.Play($"{_anim_prefix}close");
            _opened = false;
        }
    }
}
