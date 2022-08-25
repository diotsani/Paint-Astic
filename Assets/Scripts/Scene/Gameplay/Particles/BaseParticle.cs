using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.Particles
{
    public abstract class BaseParticle : MonoBehaviour
    {
        [SerializeField] protected float _despawnDelay = 2f;
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
    }
}


