using PaintAstic.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.Items
{
    public class ItemCollectPoint : BaseItem
    {
        public override void OnCollided(int playerIndex)
        {
            Debug.Log(playerIndex);
            EventManager.TriggerEvent("CollectPointMessage", playerIndex);
            EventManager.TriggerEvent("CollectPointParticleMessage", transform.position);
            EventManager.TriggerEvent("CollectedMessage");
            gameObject.SetActive(false);
        }
    }
}

