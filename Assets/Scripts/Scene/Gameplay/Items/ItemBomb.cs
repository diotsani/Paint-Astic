using PaintAstic.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.Items
{
    public class ItemBomb : BaseItem
    {
        public override void OnCollided()
        {
            EventManager.TriggerEvent("BombMessage");
            EventManager.TriggerEvent("BombParticleMessage", transform.position);
            EventManager.TriggerEvent("RevertTilesMessage");
            gameObject.SetActive(false);
        }
    }
}

