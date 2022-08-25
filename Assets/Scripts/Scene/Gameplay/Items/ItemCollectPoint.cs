using PaintAstic.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.Items
{
    public class ItemCollectPoint : BaseItem
    {
        public override void OnCollided()
        {
            EventManager.TriggerEvent("CollectPointMessage");
            EventManager.TriggerEvent("CollectPointParticleMessage", transform.position);
            EventManager.TriggerEvent("CollectedMessage");
            gameObject.SetActive(false);
        }
    }
}

