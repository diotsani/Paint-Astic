using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PaintAstic.Global.Config;

namespace PaintAstic.Global.GameAudio
{
    public class GameAudioManager : MonoBehaviour
    {
        public static GameAudioManager audioInstance;

        //[SerializeField] private ConfigData _configData;

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
            if (audioInstance == null)
            {
                audioInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            _onCollectPoint = new UnityAction(OnCollectPoint);
            _onBomb = new UnityAction(OnBomb);
            _onPlayerMove = new UnityAction(OnPlayerMove);
        }
        
        private void OnEnable()
        {
            EventManager.StartListening("CollectedMessage", _onCollectPoint);
            EventManager.StartListening("BombMessage", _onBomb);
            EventManager.StartListening("PlayMoveMessage", _onPlayerMove);
        }

        private void OnDisable()
        {
            EventManager.StopListening("CollectedMessage", _onCollectPoint);
            EventManager.StopListening("BombMessage", _onBomb);
            EventManager.StopListening("PlayMoveMessage", _onPlayerMove);
        }


        private void Update()
        {
            _bgmSource.mute = !ConfigData.configInstance.isBgmOn;
            _sfxSource.mute = !ConfigData.configInstance.isSfxOn;
        }

        public void PlaySfx(AudioClip clip)
        {
            _sfxSource.PlayOneShot(clip);
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


