using PaintAstic.Module.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Global.MatchHistory
{
    public class MatchHistoryData : MonoBehaviour
    {
        public static MatchHistoryData historyInstance;

        private const string _prefsKey = "MatchHistoryData";

        [SerializeField] private int[] _winCount = { 0, 0, 0, 0 };

        public int[] winCount => _winCount;

        private void Awake()
        {
            if (historyInstance == null)
            {
                historyInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            Load();
        }

        private void OnEnable()
        {
            EventManager.StartListening("WinnerMessage", UpdateWinCount);
        }

        private void OnDisable()
        {
            EventManager.StopListening("WinnerMessage", UpdateWinCount);
        }

        private void UpdateWinCount(object winnerData)
        {
            WinnerMessage winMessage = (WinnerMessage)winnerData;
            int winIndex = winMessage.playerIndex;
            _winCount[winIndex]++;
            Debug.Log("win count player " + _winCount[winIndex]);
            Save();
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(_prefsKey, json);
            Debug.Log(json);
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey(_prefsKey))
            {
                string json = PlayerPrefs.GetString(_prefsKey);
                JsonUtility.FromJsonOverwrite(json, this);
            }
            else
            {
                Save();
            }
        }
    }

}

