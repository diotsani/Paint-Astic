using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace PaintAstic.Scene.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _SettingButton;
        [SerializeField] private Button _ExitButton;

        private void Awake()
        {
            SetPlayButtonListener(OnClickPlayButton);
            SetSettingButtonListener(OnClickSettingButton);
            SetExitButtonListener(OnClickExitButton);
        }

        private void SetPlayButtonListener(UnityAction listener) => SetButtonListener(_playButton, listener);
        private void SetSettingButtonListener(UnityAction listener) => SetButtonListener(_SettingButton, listener);
        private void SetExitButtonListener(UnityAction listener) => SetButtonListener(_ExitButton, listener);

        private void SetButtonListener(Button button, UnityAction listener)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(listener);
        }

        private void OnClickPlayButton()
        {
            Debug.Log("Load to gameplay!");
            SceneManager.LoadScene("Gameplay");
        }

        private void OnClickSettingButton()
        {
            Debug.Log("Open setting button!");
        }

        private void OnClickExitButton()
        {
            Debug.Log("Quit game!");
        }
    }
}

