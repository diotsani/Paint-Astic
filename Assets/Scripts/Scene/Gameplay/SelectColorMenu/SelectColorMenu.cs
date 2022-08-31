using PaintAstic.Global;
using PaintAstic.Global.Config;
using PaintAstic.Global.MatchHistory;
using PaintAstic.Module.Message;
using PaintAstic.Module.Player;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PaintAstic.Module.Colors
{
    public class SelectColorMenu : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _spawnPlayer;
        [SerializeField] private List<Color> _listColors;
        [SerializeField] private List<int> _listMilestone = new List<int>();
        [SerializeField] private Image[] _colorImage;
        [SerializeField] private TextMeshProUGUI[] _textMilestone;
        [SerializeField] private int[] _currentColor = { 0, 1, 2, 3 };
        [SerializeField] private int[] _avaliableColorIndex = { 6, 6, 6, 6 };

        private int _playerNumber;

        public List<Color> ListColors => _listColors;
        public int[] currentColor => _currentColor;

        private void OnEnable()
        {
            EventManager.StartListening("ClickStartButtonMessage", OnPlayColor);
        }

        private void OnDisable()
        {
            EventManager.StopListening("ClickStartButtonMessage", OnPlayColor);
        }
        private void Start()
        {
            _playerNumber = ConfigData.configInstance.playerNumbers;
            for (int i = 0; i < _spawnPlayer.maxPlayer; i++)
            {
                foreach (int milestone in _listMilestone)
                {
                    if (MatchHistoryData.historyInstance.winCount[i] > milestone)
                    {
                        _avaliableColorIndex[i] += 1;
                    }
                }

                _textMilestone[i].text = "Total win: " + MatchHistoryData.historyInstance.winCount[i].ToString();
            }
        }

        private void Reset()
        {
            _listColors = new List<Color>();
            _listColors.Add(Color.red);
            _listColors.Add(Color.green);
            _listColors.Add(Color.blue);
            _listColors.Add(Color.yellow);
            _listColors.Add(Color.cyan);
            _listColors.Add(Color.magenta);
            Color c = new Color(0.1f, 0.2f, 0.3f);
            _listColors.Add(c);
            c = new Color(0.4f, 0.7f, 0f);
            _listColors.Add(c);
            c = new Color(0.4f, 0.2f, 0f);
            _listColors.Add(c);
            c = new Color(1f, 0.65f, 0);
            _listColors.Add(c);
        }

        public void DefaultColor()
        {
            for (int i = 0; i < _spawnPlayer.maxPlayer; i++)
            {
                OnChangeColor(i, _currentColor[i]);
            }
        }

        public void OnPlayColor()
        {
            for (int i = 0; i < _spawnPlayer.maxPlayer; i++)
            {
                EventManager.TriggerEvent("UpdateColor", new UpdateColorMessage(i, ListColors[_currentColor[i]]));
                EventManager.TriggerEvent("UpdateColorSpawn", new UpdateColorMessage(i, ListColors[_currentColor[i]]));
            }
        }

        public void OnSelectLeft(int indexButton)
        {
            int nextColor = _currentColor[indexButton];
            nextColor--;
            int refNextColor;
            bool isColorAvaliable = false;

            while (!isColorAvaliable)
            {
                refNextColor = nextColor;
                if (nextColor < 0)
                {
                    nextColor = _avaliableColorIndex[indexButton] - 1;
                }
                for (int i = 0; i < _playerNumber; i++)
                {
                    if (nextColor == _currentColor[i])
                    {
                        nextColor--;
                        if (nextColor < 0)
                        {
                            nextColor = _avaliableColorIndex[indexButton] - 1;
                        }
                    }
                }
                if (refNextColor == nextColor)
                {
                    isColorAvaliable = true;
                }
            }
            _currentColor[indexButton] = nextColor;
            OnChangeColor(indexButton, _currentColor[indexButton]);
        }
        public void OnSelectRight(int indexButton)
        {
            int nextColor = _currentColor[indexButton];
            nextColor++;
            int refNextColor;
            bool isColorAvaliable = false;

            while (!isColorAvaliable)
            {
                refNextColor = nextColor;
                if (nextColor > _avaliableColorIndex[indexButton] - 1)
                {
                    nextColor = 0;
                }
                for (int i = 0; i < _playerNumber; i++)
                {
                    if (nextColor == _currentColor[i])
                    {
                        nextColor++;
                        if (nextColor > _avaliableColorIndex[indexButton] - 1)
                        {
                            nextColor = 0;
                        }
                    }
                }
                if (refNextColor == nextColor)
                {
                    isColorAvaliable = true;
                }
            }
            _currentColor[indexButton] = nextColor;
            OnChangeColor(indexButton, _currentColor[indexButton]);
        }

        private void OnChangeColor(int indexPlayer, int indexColor)
        {
            _colorImage[indexPlayer].color = ListColors[indexColor];

        }
    }
}
