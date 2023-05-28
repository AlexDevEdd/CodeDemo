using System;
using System.Collections.Generic;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Systems.Input;
using _Game.Scripts.Ui.Base;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Systems
{
    public enum GameSoundType
    {
        None,
        Tap,
        ButtonClick,
        Purchase,
        RewardReceive,
        Window,
        Music,
        Tick,
        TapGameMusic,
        TapGameFinishSound
    }
    
    [Serializable]
    public class GameSound
    {
        public GameSoundType Type;
        public AudioClip Clip;
    }
    
    public class SoundSystem : MonoBehaviour
    {
        [SerializeField] private List<GameSound> _sounds;
        [SerializeField] private AudioSource _sourceMusic;
        [SerializeField] private AudioSource _sourceSound;
        
        [Inject] private GameSettings _settings;

        [Inject]
        private void Construct()
        {
            BaseButton.ClickSoundEvent += _ => PlaySound((GameSoundType)_);
            InputSystem.ClickSoundEvent += _ => PlaySound((GameSoundType)_);
            BaseWindow.SoundEvent += _ => PlaySound((GameSoundType)_);
             _settings.SoundChangedEvent += UpdateSoundsSettings;
        }

        private void UpdateSoundsSettings()
        {
             _sourceMusic.mute = _settings.IsMuteMusic;
             _sourceSound.mute = _settings.IsMuteSound;
        }

        public void PlayMusic(GameSoundType gameSoundType, bool loop = true)
        {
            if (_settings.IsMuteMusic) return;
            var clip = _sounds.Find(s => s.Type == gameSoundType)?.Clip;
            if (clip == null) return;
            _sourceMusic.clip = clip;
            _sourceMusic.loop = loop;
            _sourceMusic.time = 0f;
            _sourceMusic.Play();
        }
		
        public void PlaySound(GameSoundType gameSoundType)
        {
            if (_settings.IsMuteSound) return;
            var clip = _sounds.Find(s => s.Type == gameSoundType)?.Clip;
            if (clip != null)
            {
                _sourceSound.PlayOneShot(clip);
            }
        }

        public void StopMusic()
        {
            _sourceMusic.Stop();
        }
    }
}