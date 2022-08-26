using PaintAstic.Global;
using PaintAstic.Scene.Gameplay.Particles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PaintAstic.Scene.Gameplay.ParticleSpawner
{
    public class ParticleSpawner : MonoBehaviour
    {
        [SerializeField] private BaseParticle _particleCollectPointPrefab;
        [SerializeField] private BaseParticle _particleBombPrefab;

        private List<BaseParticle> _particleCollectPointPool = new List<BaseParticle>();
        private List<BaseParticle> _particleBombPool = new List<BaseParticle>();

        private void OnEnable()
        {
            EventManager.StartListening("CollectPointParticleMessage", OnCollectPointParticle);
            EventManager.StartListening("BombParticleMessage", OnBombParticle);
        }

        private void OnDisable()
        {
            EventManager.StopListening("CollectPointParticleMessage", OnCollectPointParticle);
            EventManager.StopListening("BombParticleMessage", OnBombParticle);
        }

        private void OnCollectPointParticle(object data)
        {
            Vector3 position = (Vector3)data;
            InstantiateParticle(_particleCollectPointPrefab, _particleCollectPointPool, position);
        }

        private void OnBombParticle(object data)
        {
            Vector3 position = (Vector3)data;
            InstantiateParticle(_particleBombPrefab, _particleBombPool, position);
        }

        private void InstantiateParticle(BaseParticle prefab, List<BaseParticle> pool, Vector3 position)
        {
            BaseParticle baseParticle = pool.Find(i => !i.gameObject.activeSelf);
            if (baseParticle == null)
            {
                baseParticle = Instantiate(prefab, transform);
                pool.Add(baseParticle);
            }
            ConfigSpawnedParticle(baseParticle, position);
        }

        private void ConfigSpawnedParticle(BaseParticle baseParticle, Vector3 position)
        {
            baseParticle.transform.position = position;
            baseParticle.gameObject.SetActive(true);
        }
    }
}

