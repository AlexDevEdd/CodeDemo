using System.Linq;
using _Game.Scripts.Enums;
using _Game.Scripts.GameParams;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.ScriptableObjects.Balance;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Systems.GamePlayElements;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.Ui
{
    public class MainWindow : BaseWindow
    {
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _rankText;
        [SerializeField] private TextMeshProUGUI _expText;
        
        [Inject] private GameResources _resources;
        [Inject] private WindowsSystem _windows;
        [Inject] private GameFlags _flags;
        [Inject] private ICurrencySystem _currencySystem;
        [Inject] private GameBalanceConfigs _balance;
        [Inject] private GameParamFactory _paramFactory;
        
        //private CurrencySystem _currency;
        private BaseGamePlayElement[] _elements;
        private Material _grayscaleMat;

        private GameParam _rank;
        private string _rankStr;
        private string _expStr;

        public override void Init()
        {
            //MOCK

            base.Init();
        }

        private void OnUpdateRank()
        {
            _rankText.text = $"{_rankStr}: {_rank.Value}";
        }

        public override void UpdateLocalization()
        {
            _expStr = "EXP".ToLocalized();
            _rankStr = "RANK".ToLocalized();
            base.UpdateLocalization();
        }

        private void OnUpdatePrestige()
        {
            //MOCK
        }

        private void AnyWindowOpened(BaseWindow window)
        {
            if (window is ICurrencyWindow or MainWindow) return;
            if (window.RenderUiOnly) return;
            PlayCloseAnimation();
        }

        private void AllWindowClosed(BaseWindow window)
        {
            if (window is ICurrencyWindow or MainWindow || window.IgnoreAnimations) return;
            PlayOpenAnimation();
        }
        
        public override void ResetGameLogic()
        {
            _flags.OnFlagSet -= OnFlagSet;
            _windows.WindowOpenedEvent -= AnyWindowOpened;
            _windows.WindowClosedEvent -= AllWindowClosed;
            base.ResetGameLogic();
        }

        public override void Open(params object[] list)
        {
            ShowGamePlayElements();
            OnUpdateRank();
            OnUpdatePrestige();
            UpdatePlayerAvatar();
            base.Open(list);
        }

        public void UpdatePlayerAvatar()
        {
            //MOCK
        }

        private void OnFlagSet(GameFlag flag)
        {
            var type = GamePlayElement.None;
            switch (flag)
            {
                case GameFlag.StoreOpened:
                    type = GamePlayElement.StoreButton;
                    break;
            }

            if (type == GamePlayElement.None) return;
            var element = _elements.FirstOrDefault(e => e.Type == type);
            if (element) element.SetActive(true,null,true);
        }
        
        private void ShowGamePlayElements()
        {
            foreach (var element in _elements)
            {
                var flag = false;
                var hide = false;
                switch (element.Type)
                {
                    case GamePlayElement.StoreButton:
                        flag = _flags.Has(GameFlag.StoreOpened);
                        hide = true;
                        break;
                }
                
                var mat = !flag ? _grayscaleMat : null;
                element.SetActive(flag, mat, hide);
            }
        }
    }
}