using PaintAstic.Global;
using PaintAstic.Global.MatchHistory;
using PaintAstic.Module.Message;
using PaintAstic.Module.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaintAstic.Module.Colors
{
    public class SelectColorMenu : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _spawnPlayer;
        [SerializeField] private List<Color> _listColors;
        [SerializeField] private Image[] _colorImage;
        [SerializeField] private int[] _currentColor = { 0, 1 };
        [SerializeField] private int[] _avaliableColorIndex = { 6, 6 };
        [SerializeField] private List<int> _listMilestone = new List<int>();

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
            for (int i = 0; i < _spawnPlayer.maxPlayer; i++)
            {
                foreach (int milestone in _listMilestone)
                {
                    if (MatchHistoryData.historyInstance.winCount[i] > milestone)
                    {
                        _avaliableColorIndex[i] += 1;
                    }
                }
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
            _listColors.Add(Color.white);
            _listColors.Add(Color.magenta);
            Color c = new Color(0.1f, 0.2f, 0.3f);
            _listColors.Add(c);
            c = new Color(0.9f, 0.7f, 0f);
            _listColors.Add(c);
            c = new Color(0.9f, 0.1f, 0.7f);
            _listColors.Add(c);
        }

        public void DefaultColor()
        {
            for (int i = 0; i < _colorImage.Length; i++)
            {
                OnChangeColor(i, _currentColor[i]);
            }
        }

        public void OnPlayColor()
        {
            for (int i = 0; i < _spawnPlayer.maxPlayer; i++)
            {
                EventManager.TriggerEvent("UpdateColor", new UpdateColorMessage(i, ListColors[_currentColor[i]]));
            }
        }

        public void OnSelectLeft(int indexButton)
        {
            int nextColor = _currentColor[indexButton];
            nextColor--;

            for (int i = 0; i < _currentColor.Length; i++)
            {
                if (nextColor < 0)
                {
                    nextColor = _avaliableColorIndex[indexButton] - 1;
                }
                if (nextColor == _currentColor[i])
                {
                    nextColor -= 1;
                    if (nextColor < 0)
                    {
                        nextColor = _avaliableColorIndex[indexButton] - 1;
                    }
                }
            }
            _currentColor[indexButton] = nextColor;
            OnChangeColor(indexButton, _currentColor[indexButton]);
        }
        public void OnSelectRight(int indexButton)
        {
            int nextColor = _currentColor[indexButton];
            nextColor++;

            for (int i = 0; i < _currentColor.Length; i++)
            {
                if (nextColor > _avaliableColorIndex[indexButton] - 1)
                {
                    nextColor = 0;
                }
                if (nextColor == _currentColor[i])
                {
                    nextColor += 1;
                    if (nextColor > _avaliableColorIndex[indexButton] - 1)
                    {
                        nextColor = 0;
                    }
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
