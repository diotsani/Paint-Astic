using PaintAstic.Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using PaintAstic.Global.MatchHistory;

namespace PaintAstic.Scene.MainMenu
{
    public class MatchHistoryMenu : MonoBehaviour, IButtonAble
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private TextMeshProUGUI[] _textHistory;
        [SerializeField] private TextMeshProUGUI _textLastWinner;

        private void Awake()
        {
            SetAllButtonListener();
        }

        private void Start()
        {
            for (int i = 0; i < _textHistory.Length; i++)
            {
                _textHistory[i].text = "Player " + (i+1) + "\nExp: " + MatchHistoryData.historyInstance.playerDatas[i].exp.ToString()
                    + "\nLevel: " + MatchHistoryData.historyInstance.playerDatas[i].level.ToString();
            }

            _textLastWinner.text = "Last winner: player " + MatchHistoryData.historyInstance.lastWinner.ToString();
        }

        private void SetBackButtonListener(UnityAction listener) => SetButtonListener(_backButton, OnClickBackButton);

        public void SetAllButtonListener()
        {
            SetBackButtonListener(OnClickBackButton);
        }

        public void SetButtonListener(Button button, UnityAction listener)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(listener);
        }

        public virtual void OnClickBackButton()
        {
            gameObject.SetActive(false);
        }
    }

}

