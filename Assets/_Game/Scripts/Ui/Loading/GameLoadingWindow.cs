using _Game.Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

namespace _Game.Scripts.Ui.Loading
{
    public class GameLoadingWindow : WindowLoader
    {
        [SerializeField] private ProceduralImage _progress;
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private TextMeshProUGUI _promptext;
        [SerializeField] private TextMeshProUGUI _progresstext;
        [SerializeField] private TextMeshProUGUI _appVersionText;

        protected override void StartLoading()
        {
            base.StartLoading();
            _appVersionText.text = $"v.{Application.version}";
            _loadingText.text = "";
        }

        protected override void UpdateProgress(float value)
        {
            _progress.fillAmount = value;
            _progresstext.text = $"{Mathf.RoundToInt(value * 100)}%";
            _loadingText.text = "LOADING".ToLocalized();
            _promptext.text = Resources.LoadingTexts.RandomValue().ToLocalized();
            base.UpdateProgress(value);
        }
    }
}
