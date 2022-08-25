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
        [SerializeField] private ParticleCollectPoint _particleCollectPointPrefab;
        [SerializeField] private ParticleBomb _particleBombPrefab;

        private List<ParticleCollectPoint> _particleCollectPointPool = new List<ParticleCollectPoint>();
        private List<ParticleBomb> _particleBombPool = new List<ParticleBomb>();

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
            SpawnParticleCollectPoint(position);
        }

        private void OnBombParticle(object data)
        {
            Vector3 position = (Vector3)data;
            SpawnParticleBomb(position);
        }

        private void SpawnParticleCollectPoint(Vector3 position)
        {
            ParticleCollectPoint particleCollectPoint = _particleCollectPointPool.Find(i => !i.gameObject.activeSelf);
            if (particleCollectPoint == null)
            {
                particleCollectPoint = InstantiateParticle(_particleCollectPointPrefab, _particleCollectPointPool);
            }

            ConfigSpawnedParticle(particleCollectPoint, position);
        }

        private void SpawnParticleBomb(Vector3 position)
        {
            ParticleBomb particleBomb = _particleBombPool.Find(i => !i.gameObject.activeSelf);
            if (particleBomb == null)
            {
                particleBomb = InstantiateParticle(_particleBombPrefab, _particleBombPool);
            }

            ConfigSpawnedParticle(particleBomb, position);
        }

        private T InstantiateParticle<T>(T prefab, List<T> pool) where T : BaseParticle
        {
            T baseParticle = Instantiate(prefab, transform);
            pool.Add(baseParticle);

            return baseParticle;
        }

        private void ConfigSpawnedParticle(BaseParticle baseParticle, Vector3 position)
        {
            baseParticle.transform.position = position;
            baseParticle.gameObject.SetActive(true);
        }
    }
}

