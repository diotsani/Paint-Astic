using PaintAstic.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.Items
{
    public class ItemBomb : BaseItem
    {
        public override void OnCollided(int playerIndex)
        {
            Debug.Log(playerIndex);
            EventManager.TriggerEvent("RevertTilesMessage", playerIndex);
            EventManager.TriggerEvent("BombParticleMessage", transform.position);
            EventManager.TriggerEvent("BombMessage");
            gameObject.SetActive(false);
        }
    }
}

