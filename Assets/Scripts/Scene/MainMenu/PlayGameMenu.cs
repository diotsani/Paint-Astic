using PaintAstic.Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PaintAstic.Scene.MainMenu
{
    public class PlayGameMenu : MonoBehaviour
    {
        [SerializeField] private Button[] _playerButtons;
        [SerializeField] private int[] _playerCount = { 2, 3, 4 };

        private void Awake()
        {
            SetButtonListener();
        }

        private void SetButtonListener()
        {
            for (int i = 0; i < _playerButtons.Length; i++)
            {
                int tempIndex = i;
                _playerButtons[i].onClick.AddListener(() => OnClickPlayerButton(tempIndex));
            }
        }

        private void OnClickPlayerButton(int tempIndex)
        {
            EventManager.TriggerEvent("PlayerNumbersMessage", _playerCount[tempIndex]);
            SceneManager.LoadScene("Gameplay");
        }

    }
}