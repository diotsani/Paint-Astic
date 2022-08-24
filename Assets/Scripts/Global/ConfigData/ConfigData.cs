using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PaintAstic.Global.ConfigData
{
    public class ConfigData : MonoBehaviour
    {
        private bool isBgmOn;
        private bool isSfxOn;
        private UnityAction onSwitchBgmValue;
        private UnityAction onSwitchSfxValue;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            onSwitchBgmValue = new UnityAction(ToggleMusic);
            onSwitchSfxValue = new UnityAction(ToggleEffect);
        }

        private void OnEnable()
        {
            LoadData();
            EventManager.StartListening("SwitchBgmValueMessage", onSwitchBgmValue);
            EventManager.StartListening("SwitchSfxValueMessage", onSwitchSfxValue);
        }

        private void OnDisable()
        {
            EventManager.StopListening("SwitchBgmValueMessage", onSwitchBgmValue);
            EventManager.StopListening("SwitchSfxValueMessage", onSwitchSfxValue);
        }

        private void ToggleMusic()
        {
            isBgmOn = !isBgmOn;
            if (isBgmOn)
            {
                PlayerPrefs.SetInt("BGM", 1); //BGM on
                Debug.Log("BGM On");
            }
            else
            {
                PlayerPrefs.SetInt("BGM", 0); //BGM off
                Debug.Log("BGM Off");
            }
            PlayerPrefs.Save();
            Debug.Log("BGM status: " + isBgmOn);
        }

        private void ToggleEffect()
        {
            isSfxOn = !isSfxOn;
            if (isSfxOn)
            {
                PlayerPrefs.SetInt("SFX", 1); //SFX on
                Debug.Log("SFX On");
            }
            else
            {
                PlayerPrefs.SetInt("SFX", 0); //SFX off
                Debug.Log("SFX Off");
            }
            PlayerPrefs.Save();
            Debug.Log("SFX status: " + isSfxOn);
        }

        private void LoadData()
        {
            int bgmDataHolder = PlayerPrefs.GetInt("BGM");
            if (bgmDataHolder == 1)
            {
                isBgmOn = true;
            }
            else
            {
                isBgmOn = false;
            }
            int sfxDataHolder = PlayerPrefs.GetInt("SFX");
            if (sfxDataHolder == 1)
            {
                isSfxOn = true;
            }
            else
            {
                isSfxOn = false;
            }
        }

        
    }

}
