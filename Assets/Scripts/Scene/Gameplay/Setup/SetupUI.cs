using PaintAstic.Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaintAstic.Scene.Gameplay.Setup
{

    public class SetupUI : MonoBehaviour, IButtonAble
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button[] _leftButton;
        [SerializeField] private Button[] _rigthButton;

        private void Awake()
        {
            SetAllButtonListener();
            SetLeftButtonListener();
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

        private void SetRigthButtonListener(UnityAction listener)
        {
            for (int i = 0; i < _rigthButton.Length; i++)
            {
                SetButtonListener(_rigthButton[i], listener);
            }
        }

        public void SetAllButtonListener()
        {
            SetPlayButtonListener(OnClickStartButton);
            // SetLeftButtonListener(OnClickLeftButton);
            SetRigthButtonListener(OnClickRigthButton);
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

        private void OnClickLeftButton(int indexButton)
        {
            // TODO:@Abdul
            Debug.Log("Change Colors " + indexButton);
        }

        private void OnClickRigthButton()
        {
            // TODO:@Abdul
            Debug.Log("Change Colors ");
        }

    }

}
