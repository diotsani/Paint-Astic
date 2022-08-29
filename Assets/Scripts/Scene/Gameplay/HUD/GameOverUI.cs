using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PaintAstic.Module.HUD
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button _rematchButton;
        [SerializeField] private Button _menuButton;

        private void Awake()
        {
            _rematchButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();

            _rematchButton.onClick.AddListener(OnClickRematch);
            _menuButton.onClick.AddListener(OnClickMainMenu);
        }
        void OnClickRematch()
        {
            SceneManager.LoadScene("Gameplay");
        }
        void OnClickMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}

