using PaintAstic.Global;
using PaintAstic.Module.Colors;
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
        [SerializeField] private SelectColorMenu _selectColorMenu;

        private void Awake()
        {
            SetAllButtonListener();
            SetLeftButtonListener();
            SetRigthButtonListener();

            _selectColorMenu.DefaultColor();
        }

        private void SetPlayButtonListener(UnityAction listener) => SetButtonListener(_startButton, listener);

        private void SetLeftButtonListener()
        {
            for (int i = 0; i < _leftButton.Length; i++)
            {
                int tempIndex = i;
                _leftButton[i].onClick.AddListener(() => _selectColorMenu.OnSelectLeft(tempIndex));
            }
        }

        private void SetRigthButtonListener()
        {
            for (int i = 0; i < _rightButton.Length; i++)
            {
                int tempIndex = i;
                _rightButton[i].onClick.AddListener(() => _selectColorMenu.OnSelectRight(tempIndex));
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
            EventManager.TriggerEvent("ClickStartButtonMessage");

            for (int i = 0; i < 2; i++)
            {
                EventManager.TriggerEvent("UpdateColor", new UpdateColorMessage(i, _selectColorMenu.ListColors[_currentColor[i]]));
            }
        }
    }
}
