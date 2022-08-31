using PaintAstic.Global;
using PaintAstic.Global.Config;
using PaintAstic.Module.Colors;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaintAstic.Scene.Gameplay.Setup
{

    public class SetupUI : MonoBehaviour, IButtonAble
    {
        [SerializeField] private GameObject[] _playerSelectMenu;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button[] _leftButton;
        [SerializeField] private Button[] _rightButton;
        [SerializeField] private SelectColorMenu _selectColorMenu;

        private void Awake()
        {
            for (int i = 0; i < ConfigData.configInstance.playerNumbers; i++)
            {
                _playerSelectMenu[i].SetActive(true);
            }
            
            SetAllButtonListener();
            SetLeftButtonListener();
            SetRigthButtonListener();
        }

        private void Start()
        {
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
        }
    }
}
