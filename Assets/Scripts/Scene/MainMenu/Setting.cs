using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using PaintAstic.Global;
using PaintAstic.Global.Config;

namespace PaintAstic.Scene.MainMenu
{
    public class Setting : MonoBehaviour, IButtonAble
    {
        [SerializeField] private Button _sfxButton;
        [SerializeField] private Button _bgmButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TextMeshProUGUI _textSfx;
        [SerializeField] private TextMeshProUGUI _textBgm;
        [SerializeField] private GameObject _settingPage;

        //[SerializeField] private ConfigData _configData;


        private void Awake()
        {
            SetAllButtonListener();
            LoadAllState();
        }

        private void SetBackButtonListener(UnityAction listener) => SetButtonListener(_backButton, OnClickBackButton);
        private void SetSfxButtonListener(UnityAction listener) => SetButtonListener(_sfxButton, OnClickSfxButton);
        private void SetBgmButtonListener(UnityAction listener) => SetButtonListener(_bgmButton, OnClickBgmButton);

        public void SetButtonListener(Button button, UnityAction listener)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(listener);
        }

        public virtual void OnClickBackButton()
        {
            _settingPage.SetActive(false);
        }

        private void OnClickSfxButton()
        {
            EventManager.TriggerEvent("SwitchSfxValueMessage");
            UpdateSfxState(ConfigData.configInstance.isSfxOn);
        }

        private void OnClickBgmButton()
        {
            EventManager.TriggerEvent("SwitchBgmValueMessage");
            UpdateBgmState(ConfigData.configInstance.isBgmOn);
        }

        public void UpdateSfxState(bool isMuted) => _textSfx.SetText($"Sfx {(isMuted ? "On" : "Off")}");
        public void UpdateBgmState(bool isMuted) => _textBgm.SetText($"Bgm {(isMuted ? "On" : "Off")}");

        public void LoadAllState()
        {
            UpdateSfxState(ConfigData.configInstance.isSfxOn);
            UpdateBgmState(ConfigData.configInstance.isBgmOn);
        }

        public void SetAllButtonListener()
        {
            SetBackButtonListener(OnClickBackButton);
            SetSfxButtonListener(OnClickSfxButton);
            SetBgmButtonListener(OnClickBgmButton);
        }

    }
}
