using System.Collections;
using System.Collections.Generic;
using PaintAstic.Global;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _setupPage; // select player colors
        [SerializeField] private GameObject _tutorialPage; // tutorial gameplay

        public static GameManager Instance;

        enum PageState
        {
            None,
            Setup,
            Tutorial
        }

        private void OnEnable()
        {
            EventManager.StartListening("ClickStartButtonMessage", StartTheGame);
            EventManager.StartListening("CloseTutorialMessage", CloseTutorial);
        }

        private void OnDisable()
        {
            EventManager.StopListening("ClickStartButtonMessage", StartTheGame);
            EventManager.StopListening("CloseTutorialMessage", CloseTutorial);
        }

        private void Awake()
        {
            Instance = this;
        }

        void SetPageState(PageState state)
        {
            switch (state)
            {
                case PageState.None:
                    _setupPage.SetActive(false);
                    _tutorialPage.SetActive(false);
                    break;
                case PageState.Setup:
                    _setupPage.SetActive(true);
                    _tutorialPage.SetActive(false);
                    break;
                case PageState.Tutorial:
                    _setupPage.SetActive(false);
                    _tutorialPage.SetActive(true);
                    break;
            }
        }

        public void StartTheGame()
        {
            Debug.Log("Enjoy the game :D");
            SetPageState(PageState.Tutorial);
            
        }
        public void CloseTutorial()
        {
            Debug.Log("Close tutorial page!");
            SetPageState(PageState.None);
        }

    }
}


