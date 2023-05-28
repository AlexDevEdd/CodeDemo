using System;
using _Game.Scripts.Ui.Base;
using UnityEngine;

namespace _Game.Scripts.Ui.WindowTabs
{
    public class WindowNavigationButton : BaseButton
    {
        public Action<int> OnPressed;

        [SerializeField] private int _id;

        protected override void OnClick()
        {
            base.OnClick();
            OnPressed?.Invoke(_id);
        }
    }
}
