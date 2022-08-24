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

        protected void Update()
        {
            //_despawnDelayTimer += Time.deltaTime;
            //if (_despawnDelayTimer > _despawnDelay)
            //{
            //    gameObject.SetActive(false);
            //    _despawnDelayTimer = 0;
            //}
        }

        protected void OnTriggerEnter(Collider other)
        {
            Debug.Log(other);
            if (other.gameObject.CompareTag("Player"))
            {
                OnCollided();
                Debug.Log("Masuk if");
            }
        }

        public abstract void OnCollided();
    }

}

