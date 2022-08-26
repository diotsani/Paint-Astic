using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using PaintAstic.Global;

namespace PaintAstic.Scene.MainMenu
{
    public class Setting : MonoBehaviour, IButtonAble
    {
        [SerializeField] private Button _sfxButton;
        [SerializeField] private Button _bgmButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TextMeshProUGUI _textSfx;
        [SerializeField] private TextMeshProUGUI _textBgm;
        [SerializeField] private GameObject _menuPage;
        [SerializeField] private GameObject _settingPage;

        private void Awake()
        {
            SetAllButtonListener();
        }

        private void SetBackButtonListener(UnityAction listener) => SetButtonListener(_backButton, OnClickBackButton);

        public void SetButtonListener(Button button, UnityAction listener)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(listener);
        }

        private void OnClickBackButton()
        {
            _settingPage.SetActive(false);
            _menuPage.SetActive(true);
        }


        private void OnClickSfxButton()
        {
            // TODO: @faisal
        }

        private void OnClickBgmButton()
        {
            // TODO: @faisal
        }

        public void UpdateSfxState(bool isMuted) => _textSfx.SetText($"SFX {(isMuted ? "Off" : "On")}"); 
        public void UpdateBgmState(bool isMuted) => _textBgm.SetText($"BGM {(isMuted ? "Off" : "On")}"); 
        
        public void SetAllButtonListener()
        {
            SetBackButtonListener(OnClickBackButton);
        }
    }
}
