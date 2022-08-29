using PaintAstic.Global;
using PaintAstic.Module.Colors;
using PaintAstic.Module.Message;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaintAstic.Scene.Gameplay.Setup
{

    public class SetupUI : MonoBehaviour, IButtonAble
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button[] _leftButton;
        [SerializeField] private Button[] _rightButton;
        [SerializeField] private Image[] _colorImage;

        [SerializeField] private SelectColorMenu _selectColorMenu;
        [SerializeField] private int[] _currentColor;

        private void Awake()
        {
            SetAllButtonListener();
            SetLeftButtonListener();
            SetRigthButtonListener();

            for (int i = 0; i < _colorImage.Length; i++)
            {
                OnChangeColor(i, _currentColor[i]);
            }
        }

        private void SetPlayButtonListener(UnityAction listener) => SetButtonListener(_startButton, listener);

        private void SetLeftButtonListener()
        {
            for (int i = 0; i < _leftButton.Length; i++)
            {
                int tempIndex = i;
                _leftButton[i].onClick.AddListener(() => OnClickLeftButton(tempIndex));
            }
        }

        private void SetRigthButtonListener()
        {
            for (int i = 0; i < _rightButton.Length; i++)
            {
                int tempIndex = i;
                _rightButton[i].onClick.AddListener(() => OnClickRightButton(tempIndex));
            }
        }

        public void SetAllButtonListener()
        {
            SetPlayButtonListener(OnClickStartButton);
        }

        public void SetButtonListener(Button button, UnityAction listener)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(listener);
        }

        private void OnClickStartButton()
        {
            EventManager.TriggerEvent("ClickPlayButtonMessage");

            for (int i = 0; i < 2; i++)
            {
                EventManager.TriggerEvent("UpdateColor", new UpdateColorMessage(i, _selectColorMenu.ListColors[_currentColor[i]]));
            }
        }

        private void OnClickLeftButton(int indexButton)
        {
            if (_currentColor[indexButton] == 0)
            {
                _currentColor[indexButton] = 3;
            }
            else
            {
                _currentColor[indexButton]--;
            }
            OnChangeColor(indexButton, _currentColor[indexButton]);
        }

        private void OnClickRightButton(int indexButton)
        {
            if (_currentColor[indexButton] == 3)
            {
                _currentColor[indexButton] = 0;
            }
            else
            {
                _currentColor[indexButton]++;
            }
            
            OnChangeColor(indexButton, _currentColor[indexButton]);
        }

        private void OnChangeColor(int indexPlayer, int indexColor)
        {
            _colorImage[indexPlayer].color = _selectColorMenu.ListColors[indexColor];
            
        }
    }

}
