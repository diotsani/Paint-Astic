using PaintAstic.Global;
using PaintAstic.Global.Config;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaintAstic.Scene.Gameplay.Tutorial
{
    public class TutorialUI : MonoBehaviour, IButtonAble
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private GameObject[] _controlContainer;

        private void Awake()
        {
            SetAllButtonListener();
        }

        private void Start()
        {
            for (int i = 0; i < ConfigData.configInstance.playerNumbers; i++)
            {
                _controlContainer[i].SetActive(true);
            }
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
            EventManager.TriggerEvent("GameStartMessage");
        }

        public void SetAllButtonListener()
        {
            SetBackButtonListener(OnClickBackButton);
        }
    }

}

