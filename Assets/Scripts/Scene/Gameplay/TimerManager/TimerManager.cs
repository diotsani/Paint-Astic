using PaintAstic.Global;
using PaintAstic.Module.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace PaintAstic.Module.Timer
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private float _timeLeft;
        [SerializeField] private bool isPlayGame;

        private void OnEnable()
        {
            EventManager.StartListening("PlayGameMessage", IsPlayGame);
            EventManager.StartListening("PauseGameMessage", OnGamePause);
            EventManager.StartListening("ContinueGameMessage", OnGameContinue);
        }
        private void OnDisable()
        {
            EventManager.StopListening("PlayGameMessage", IsPlayGame);
            EventManager.StopListening("PauseGameMessage", OnGamePause);
            EventManager.StopListening("ContinueGameMessage", OnGameContinue);
        }
        private void Update()
        {
            GameStart();
        }
        void GameStart()
        {
            if(isPlayGame)
            {
                if(_timeLeft > 0)
                {
                    _timeLeft -= Time.deltaTime;
                    UpdateTimer(_timeLeft);
                }
                else
                {
                    _timeLeft = 0;
                    isPlayGame = false;
                    EventManager.TriggerEvent("OnGameOverMessage");
                }
            }
        }
        void UpdateTimer(float timeUpdate)
        {
            timeUpdate += 1;
            float minutes = Mathf.FloorToInt(timeUpdate / 60);
            float seconds = Mathf.FloorToInt(timeUpdate % 60);

            timerText.text = string.Format("{0 : 00} : {1 : 00}", minutes, seconds);
        }
        void OnGamePause()
        {
            Time.timeScale = 0;
        }
        void OnGameContinue()
        {
            Time.timeScale = 1;
        }
        void IsPlayGame()
        {
            isPlayGame = true;
        }
    }
}

