using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PaintAstic.Global;

namespace PaintAstic.Scene.MainMenu
{
    public class MainMenu : MonoBehaviour, IButtonAble
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _SettingButton;
        [SerializeField] private Button _ExitButton;
        [SerializeField] private GameObject _playerPage;
        [SerializeField] private GameObject _settingPage;
        [SerializeField] private GameObject _menuPage;

        private void Awake()
        {
            SetAllButtonListener();
        }
        
        private void SetPlayButtonListener(UnityAction listener) => SetButtonListener(_playButton, listener);
        private void SetSettingButtonListener(UnityAction listener) => SetButtonListener(_SettingButton, listener);
        private void SetExitButtonListener(UnityAction listener) => SetButtonListener(_ExitButton, listener);

        public void SetButtonListener(Button button, UnityAction listener)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(listener);
        }

        private void OnClickPlayButton()
        {
            _playerPage.SetActive(true);
            
        }

        private void OnClickSettingButton()
        {         
            _settingPage.SetActive(true);
        }

        private void OnClickExitButton()
        {
            Debug.Log("Quit game!");
            Application.Quit();
        }

        public void SetAllButtonListener()
        {
            SetPlayButtonListener(OnClickPlayButton);
            SetSettingButtonListener(OnClickSettingButton);
            SetExitButtonListener(OnClickExitButton);
        }
    }
}

