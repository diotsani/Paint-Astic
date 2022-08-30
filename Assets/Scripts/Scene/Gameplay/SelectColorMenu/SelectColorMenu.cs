using PaintAstic.Global;
using PaintAstic.Module.Message;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaintAstic.Module.Colors
{
    public class SelectColorMenu : MonoBehaviour
    {
        [SerializeField] private List<Color> _listColors;
        [SerializeField] private Image[] _colorImage;
        [SerializeField] private int[] _currentColor;

        public List<Color> ListColors => _listColors;
        public int[] currentColor => _currentColor;

        private void Reset()
        {
            _listColors = new List<Color>();
            _listColors.Add(Color.red);
            _listColors.Add(Color.green);
            _listColors.Add(Color.blue);
            _listColors.Add(Color.yellow);
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
            for (int i = 0; i < 2; i++)
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
                    nextColor = _listColors.Count - 1;
                }
                if (nextColor == _currentColor[i])
                {
                    nextColor -= 1;
                    if (nextColor < 0)
                    {
                        nextColor = _listColors.Count - 1;
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
                if (nextColor > _listColors.Count - 1)
                {
                    nextColor = 0;
                }
                if (nextColor == _currentColor[i])
                {
                    nextColor += 1;
                    if (nextColor > _listColors.Count - 1)
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