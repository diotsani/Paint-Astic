using PaintAstic.Global.Config;
using PaintAstic.Module.Message;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Global.MatchHistory
{
    public class MatchHistoryData : MonoBehaviour
    {
        public static MatchHistoryData historyInstance;

        private const string _prefsKey = "MatchHistoryData";

        [SerializeField] private int[] _winCount = { 0, 0, 0, 0 };
        [SerializeField] private PlayerData[] _playerDatas;
        [SerializeField] private List<int> _listMilestone = new List<int>();
        [SerializeField] private int _lastWinner;

        public int[] winCount => _winCount;
        public int lastWinner => _lastWinner;
        public PlayerData[] playerDatas => _playerDatas;

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
            bool isDraw = winMessage.isDraw;
            if (isDraw)
            {
                for (int i = 0; i < ConfigData.configInstance.playerNumbers; i++)
                {
                    _playerDatas[i].exp += 50;
                }
                return;
            }
            for (int i = 0; i < ConfigData.configInstance.playerNumbers; i++)
            {
                if (i == winIndex)
                {
                    _playerDatas[i].exp += 100;
                }
                else
                {
                    _playerDatas[i].exp += 50;
                }
            }
            for (int i = 0; i < ConfigData.configInstance.playerNumbers; i++)
            {
                if (_playerDatas[i].exp >= 500)
                {
                    _playerDatas[i].level++;
                    _playerDatas[i].exp -= 500;
                    if (_playerDatas[i].level % 2 == 0)
                    {
                        _playerDatas[i].availableColor += 1;
                    }
                }
            }
            _lastWinner = winIndex + 1;
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
                for (int i = 0; i < _winCount.Length; i++)
                {
                    if (_winCount[i] != 0)
                    {
                        _playerDatas[i].winCount = _winCount[i];
                        _playerDatas[i].availableColor = 6;
                        foreach (int milestone in _listMilestone)
                        {
                            if (_playerDatas[i].winCount >= milestone)
                            {
                                _playerDatas[i].availableColor += 1;
                            }
                        }
                        _winCount[i] = 0;
                    }
                    Debug.Log("win count player " + _playerDatas[i].winCount);
                }
                for (int i = 0; i < _playerDatas.Length; i++)
                {
                    if (_playerDatas[i].winCount != 0)
                    {
                        _playerDatas[i].exp += _playerDatas[i].winCount * 100;
                        for (int j = 0; j < _playerDatas.Length; j++)
                        {
                            if (j != i)
                            {
                                _playerDatas[j].exp += _playerDatas[i].winCount * 50;
                            }
                        }
                        _playerDatas[i].availableColor = 6;
                        foreach (int milestone in _listMilestone)
                        {
                            if (_playerDatas[i].winCount >= milestone)
                            {
                                _playerDatas[i].availableColor += 1;
                            }
                        }
                        _playerDatas[i].winCount = 0;
                    }
                }
                for (int i = 0; i < _playerDatas.Length; i++)
                {
                    if (_playerDatas[i].availableColor == 0)
                    {
                        _playerDatas[i].availableColor = 6;
                    }
                }
                for (int i = 0; i < _playerDatas.Length; i++)
                {
                    while (_playerDatas[i].exp / 500 > 0)
                    {
                        _playerDatas[i].level++;
                        _playerDatas[i].exp -= 500;
                    }
                }


            }
            else
            {
                Save();
            }
        }
    }
}

