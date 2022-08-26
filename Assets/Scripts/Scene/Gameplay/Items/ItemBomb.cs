using PaintAstic.Global;
using PaintAstic.Module.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.Items
{
    public class ItemBomb : BaseItem
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                int targetPlayerIndex = player.playerIndex;
                OnCollided(targetPlayerIndex);
            }
        }

        public void OnCollided(int playerIndex)
        {
            EventManager.TriggerEvent("RevertTilesMessage", playerIndex);
            EventManager.TriggerEvent("BombParticleMessage", transform.position);
            EventManager.TriggerEvent("BombMessage");
            gameObject.SetActive(false);
        }
    }
}

