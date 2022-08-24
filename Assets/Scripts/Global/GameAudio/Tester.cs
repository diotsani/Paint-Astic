using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Global.GameAudio
{
    public class Tester : MonoBehaviour
    {
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                EventManager.TriggerEvent("CollectPointMessage");
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                EventManager.TriggerEvent("BombMessage");
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                EventManager.TriggerEvent("SwitchSfxValueMessage");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                EventManager.TriggerEvent("SwitchBgmValueMessage");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                EventManager.TriggerEvent("ConvertTilesMessage");
            }
        }
    }

}
