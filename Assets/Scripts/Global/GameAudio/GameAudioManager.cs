using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PaintAstic.Global.Config;

namespace PaintAstic.Global.GameAudio
{
    public class GameAudioManager : MonoBehaviour
    {
        [SerializeField] private ConfigData _configData;

        [SerializeField] private AudioSource _bgmSource;
        [SerializeField] private AudioSource _sfxSource;

        [SerializeField] private AudioClip _collectPointSound;
        [SerializeField] private AudioClip _bombSound;
        [SerializeField] private AudioClip _playerMoveSound;

        private UnityAction _onCollectPoint;
        private UnityAction _onBomb;
        private UnityAction _onPlayerMove;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _onCollectPoint = new UnityAction(OnCollectPoint);
            _onBomb = new UnityAction(OnBomb);
            _onPlayerMove = new UnityAction(OnPlayerMove);
        }

        private void OnEnable()
        {
            EventManager.StartListening("CollectedMessage", _onCollectPoint);
            EventManager.StartListening("BombMessage", _onBomb);
            EventManager.StartListening("Move", _onPlayerMove);
        }

        private void OnDisable()
        {
            EventManager.StopListening("CollectedMessage", _onCollectPoint);
            EventManager.StopListening("BombMessage", _onBomb);
            EventManager.StopListening("Move", _onPlayerMove);
        }

        private void Update()
        {
            _bgmSource.mute = !_configData.isBgmOn;
            _sfxSource.mute = !_configData.isSfxOn;
        }

        public void PlaySfx(AudioClip clip)
        {
            _sfxSource.PlayOneShot(clip);
            Debug.Log("SFX played!");
        }

        public void OnCollectPoint()
        {
            PlaySfx(_collectPointSound);
        }

        public void OnBomb()
        {
            PlaySfx(_bombSound);
        }

        public void OnPlayerMove()
        {
            PlaySfx(_playerMoveSound);
        }
    }
}


