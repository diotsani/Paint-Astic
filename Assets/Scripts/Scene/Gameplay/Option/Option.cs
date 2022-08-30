using System.Collections;
using System.Collections.Generic;
using PaintAstic.Global;
using PaintAstic.Scene.MainMenu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PaintAstic.Module.Option
{

    public class Option : Setting
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            SetAllButtonListener();
            SetHomeButtonListener(OnClickHomeButton);
            SetRestartButtonListener(OnClickRestartButton);
        }

        public  void OnClickHomeButton()
        {
            Debug.Log("Back To main menu!");
            SceneManager.LoadScene("MainMenu");
        }

        public override void OnClickBackButton()
        {
            base.OnClickBackButton();
            Time.timeScale = 1;
        }

        private void OnClickRestartButton()
        {
            Debug.Log("Restart The Game!");
            SceneManager.LoadScene("Gameplay");
        }

        private void SetHomeButtonListener(UnityAction listener) => SetButtonListener(_homeButton, OnClickHomeButton);
        private void SetRestartButtonListener(UnityAction listener) => SetButtonListener(_restartButton, OnClickRestartButton);
    }
}

