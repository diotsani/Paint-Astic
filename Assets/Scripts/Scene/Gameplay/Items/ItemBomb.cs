using PaintAstic.Global;
using PaintAstic.Module.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.Items
{
    public class ItemBomb : BaseItem
    {
        public void OnCollided(int playerIndex)
        {
            EventManager.TriggerEvent("RevertTilesMessage", playerIndex);
            EventManager.TriggerEvent("BombParticleMessage", transform.position);
            EventManager.TriggerEvent("BombMessage");
            gameObject.SetActive(false);
        }
    }
}

