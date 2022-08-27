using PaintAstic.Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaintAstic.Scene.Gameplay.Tutorial
{
    public class TutorialUI : MonoBehaviour, IButtonAble
    {
        [SerializeField] private Button _backButton;

        private void Awake()
        {
            SetAllButtonListener();
        }

        private void SetBackButtonListener(UnityAction listener) => SetButtonListener(_backButton, listener);

        public void SetButtonListener(Button button, UnityAction listener)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(listener);
        }

        private void OnClickBackButton()
        {
            EventManager.TriggerEvent("CloseTutorialMessage");
        }

        public void SetAllButtonListener()
        {
            SetBackButtonListener(OnClickBackButton);
        }
    }

}

