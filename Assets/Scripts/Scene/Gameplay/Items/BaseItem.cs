using PaintAstic.Global;
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
                OnCollided();
            }
        }

        public abstract void OnCollided();
    }

}

