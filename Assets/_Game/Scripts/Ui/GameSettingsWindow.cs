using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Systems.Ads;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Ui
{
	public class GameSettingsWindow : BaseWindow
	{
		[SerializeField] private TextMeshProUGUI _title;
		[SerializeField] private TextMeshProUGUI _musicText;
		[SerializeField] private TextMeshProUGUI _soundText;
		[SerializeField] private TextMeshProUGUI _notificationsText;
		[SerializeField] private TextMeshProUGUI _rateGameText;
		[SerializeField] private TextMeshProUGUI _purchasesText;
		[SerializeField] private TextMeshProUGUI _versionText;
		
		[SerializeField] private BaseButton _musicButton;
		[SerializeField] private BaseButton _soundButton;
		[SerializeField] private BaseButton _termsButton;
		[SerializeField] private BaseButton _privacyButton;
		[SerializeField] private BaseButton _notificationsButton;
		[SerializeField] private BaseButton _adDebuggerButton;
		[SerializeField] private BaseButton _cheatsButton;
		[SerializeField] private BaseButton _rateUsButton;
		[SerializeField] private BaseButton _restorePurchasesButton;

		[Inject] private ProjectSettings _settings;
		[Inject] private GameSettings _gameSettings;
		[Inject] private WindowsSystem _windows;
		[Inject] private AdSystem _ad;
		
		private int _tapsAdDebugger;
		private int _tapsCheats;

		private string _onStr;
		private string _offStr;
		private string _enableStr;
		private string _disableStr;
		
		public override void Init()
		{
			_musicButton.SetCallback(OnPressedMusicButton);
			_soundButton.SetCallback(OnPressedSoundButton);
			_notificationsButton.SetCallback(OnPressedNotifications);
			_adDebuggerButton.SetCallback(OnPressedAdDebugger);
			_cheatsButton.SetCallback(OnPressedCheats);
			_rateUsButton.SetCallback(OnPressedRateUs);
			
			_termsButton.SetText("TERMS_OF_USE".ToLocalized());
			_privacyButton.SetText("PRIVACY_POLICY".ToLocalized());
			_termsButton.Callback += () => Application.OpenURL(_settings.TermsLink);
			_privacyButton.Callback += () => Application.OpenURL(_settings.PrivacyLink);

			_versionText.text = $"v.{Application.version}";
			
			base.Init();
		}

		public override void UpdateLocalization()
		{
			_title.SetText("SETTINGS".ToLocalized());
			_musicText.SetText("MUSIC".ToLocalized());
			_soundText.SetText("SOUND".ToLocalized());
			_notificationsText.SetText("NOTIFICATIONS".ToLocalized());
			_rateGameText.SetText("RATE_GAMES".ToLocalized());
			_rateUsButton.SetText("RATE_GAMES".ToLocalized());
			_purchasesText.SetText("PURCHASES".ToLocalized());

			_onStr = "ON".ToLocalized();
			_offStr = "OFF".ToLocalized();
			_enableStr = "ENABLED".ToLocalized();
			_disableStr = "DISABLED".ToLocalized();
			
			base.UpdateLocalization();
		}

		public override void Open(params object[] list)
		{
			base.Open(list);
			Redraw();
		}

		private void Redraw()
		{
			_musicButton.SetText(_gameSettings.MuteMusic ? _offStr : _onStr);
			_musicButton.SetColors(!_gameSettings.MuteMusic);
			_soundButton.SetText(_gameSettings.MuteSound ? _offStr : _onStr);
			_soundButton.SetColors(!_gameSettings.MuteSound);
			_notificationsButton.SetText(_gameSettings.DisablePushNotifications ? _disableStr : _enableStr);
			_notificationsButton.SetColors(!_gameSettings.DisablePushNotifications);
		}

		private void OnPressedMusicButton()
		{
			_gameSettings.MuteMusic = !_gameSettings.IsMuteMusic;
			Redraw();
		}
		
		private void OnPressedSoundButton()
		{
			_gameSettings.MuteSound = !_gameSettings.IsMuteSound;
			Redraw();
		}
		
		private void OnPressedNotifications()
		{
			_gameSettings.DisablePushNotifications = !_gameSettings.IsDisablePushNotifications;
			Redraw();
		}
		
		private void OnPressedAdDebugger()
		{
			if (!_settings.Ads) return;
			_tapsAdDebugger++;
			if (_tapsAdDebugger < 3) return;
			_tapsAdDebugger = 0;
			_ad.ShowDebugger();
		}
		
		private void OnPressedCheats()
		{
			if (!_settings.Cheats) return;
			_tapsCheats++;
			if (_tapsCheats < 3) return;
			_tapsCheats = 0;
			_windows.OpenWindow<CheatsWindow>();
		}
		
		private void OnPressedRateUs()
		{
#if UNITY_ANDROID
			Application.OpenURL(_settings.GooglePlayLink);
#elif UNITY_IPHONE || UNITY_IOS

            Application.OpenURL(_settings.AppStoreLink);
#endif
			PlayerPrefs.SetString("IsAppRated", "");
		}
	}
}