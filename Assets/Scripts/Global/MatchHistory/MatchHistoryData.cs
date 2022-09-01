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

        public int[] winCount => _winCount;
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
                return;
            }
            _playerDatas[winIndex].winCount++;
            foreach (int milestone in _listMilestone)
            {
                if (_playerDatas[winIndex].winCount == milestone)
                {
                    _playerDatas[winIndex].availableColor += 1;
                }
            }

            Debug.Log("win count player " + _playerDatas[winIndex].winCount);
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
                    }
                }
                for (int i = 0; i < _playerDatas.Length; i++)
                {
                    if (_playerDatas[i].availableColor == 0)
                    {
                        _playerDatas[i].availableColor = 6;
                    }
                }
                Debug.Log(_playerDatas[0].exp);
                Debug.Log(_playerDatas[1].exp);
                Debug.Log(_playerDatas[2].exp);
                Debug.Log(_playerDatas[3].exp);
                
            }
            else
            {
                Save();
            }
        }
    }
}

