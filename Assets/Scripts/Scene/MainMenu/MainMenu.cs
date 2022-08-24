using UnityEngine;
using System;

namespace PaintAstic.Scene.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public static event Action OnTesting;
        private void OnEnable() {
            
        }

        private void OnDisable() {
            
        }

        public void OnClickPlayButton()
        {
            Debug.Log("Load to gameplay!");
        }

        public void OnClickSettingButton()
        {
            Debug.Log("Open setting button!");
        }

        public void OnClickExitButton()
        {
            Debug.Log("Quit game!");
        }
    }
}

