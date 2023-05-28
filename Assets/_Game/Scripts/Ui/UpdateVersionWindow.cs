using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Ui
{
    public class UpdateVersionWindow : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private BaseButton _updateButton;
        
        [Inject] private ProjectSettings _settings;
        
        public override void Init()
        {
            _updateButton.SetCallback(OnPressedUpdate);
            base.Init();
        }

        private void OnPressedUpdate()
        {
#if UNITY_ANDROID
            Application.OpenURL(_settings.GooglePlayLink);
#elif UNITY_IPHONE || UNITY_IOS
            //Application.OpenURL(_settings.AppStoreLink);
#endif
        }

        public override void UpdateLocalization()
        {
            _title.text = "UPDATE_VERSION_WINDOW_TITLE".ToLocalized();
            _text.text = "UPDATE_VERSION_WINDOW_TEXT".ToLocalized();
            _updateButton.SetText("UPDATE_VERSION_WINDOW_BUTTON".ToLocalized());
            base.UpdateLocalization();
        }
    }
}