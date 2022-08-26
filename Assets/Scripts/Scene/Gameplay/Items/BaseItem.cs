using PaintAstic.Global;
using PaintAstic.Module.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.Items
{
    public abstract class BaseItem : MonoBehaviour, ICollidable
    {
        [SerializeField] protected float _despawnDelay = 6f;
        protected float _despawnDelayTimer;

        protected void OnEnable()
        {
            _despawnDelayTimer = 0f;
            EventManager.StartListening("OnGamePauseMessage", OnGamePause);
            EventManager.StartListening("OnGameContinueMessage", OnGameContinue);
        }

        private void OnDisable()
        {
            EventManager.StopListening("OnGamePauseMessage", OnGamePause);
            EventManager.StopListening("OnGameContinueMessage", OnGameContinue);
        }

        protected void Update()
        {
            _despawnDelayTimer += Time.deltaTime;
            if (_despawnDelayTimer > _despawnDelay)
            {
                gameObject.SetActive(false);
                _despawnDelayTimer = 0f;
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                int targetPlayerIndex = other.gameObject.GetComponent<PlayerController>().playerIndex;
                OnCollided(targetPlayerIndex);
            }
        }

        private void OnGamePause()
        {
            Time.timeScale = 0f;
        }

        private void OnGameContinue()
        {
            Time.timeScale = 1f;
        }

        public abstract void OnCollided(int playerIndex);
    }

}

