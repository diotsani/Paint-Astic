using PaintAstic.Global;
using PaintAstic.Module.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using PaintAstic.Global.Config;

namespace PaintAstic.Module.HUD
{
    public class GameplayUI : MonoBehaviour
    {
        [Header("Winner")]
        [SerializeField] private TextMeshProUGUI winPlayerText;
        [SerializeField] private TextMeshProUGUI winPlayerPointText;
        [SerializeField] private Image panelGameOver;
        [SerializeField] private Button _optionButton;

        [SerializeField] private GameObject _optionPage;

        [SerializeField] private TextMeshProUGUI[] scorePlayerText;
        [SerializeField] private int[] playerName;

        private void OnEnable()
        {
            EventManager.StartListening("UpdatePointMessage", AddPointToString);
            EventManager.StartListening("WinnerMessage", GetPlayerWinner);
        }

        private void OnDisable()
        {
            EventManager.StopListening("UpdatePointMessage", AddPointToString);
            EventManager.StopListening("WinnerMessage", GetPlayerWinner);
        }

        private void Awake()
        {
            for (int i = 0; i < ConfigData.configInstance.playerNumbers; i++)
            {
                scorePlayerText[i].gameObject.SetActive(true);
            }

            SetOptionButtonListener(_optionButton, OnClickOptionButton);
        }

        private void SetOptionButtonListener(Button button, UnityAction listener)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(listener);
        }

        private void OnClickOptionButton()
        {
            Debug.Log("Open Option menu!");
            _optionPage.SetActive(true);
            Time.timeScale = 0;
        }

        void AddPointToString(object pointData)
        {
            UpdatePointMessage message = (UpdatePointMessage)pointData;
            //int PlayerName = playerName[message.playerIndex] = message.playerIndex + 1;
            int PlayerName = message.playerIndex + 1;
            scorePlayerText[message.playerIndex].text = "Player " + PlayerName.ToString() + " Point : " + message.point.ToString();
            //scorePlayerText[message.playerIndex].text = "Player " + message.playerIndex + 1.ToString() + " Point : " + message.point.ToString();
        }

        void GetPlayerWinner(object winData)
        {
            WinnerMessage message = (WinnerMessage)winData;
            int WinPlayerName = message.playerIndex + 1;
            winPlayerText.text = "Player " + WinPlayerName.ToString() + " WIN";
            winPlayerPointText.text = "Your Point " + message.point.ToString();

            panelGameOver.gameObject.SetActive(true);

            Time.timeScale = 0;
        }
    }
}

