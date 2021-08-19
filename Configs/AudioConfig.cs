﻿using UnityEngine;
using UnityEngine.Audio;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "GameConfigs/AudioConfig", order = 0)]
    public class AudioConfig : ScriptableObject
    {
        [SerializeField] private AudioMixerGroup uiMixer;
        [SerializeField] private AudioMixerGroup musicMixer;
        [Space] [SerializeField] private AudioClip[] menuMusics;
        [Space] [SerializeField] private AudioClip buttonSelected;
        [SerializeField] private AudioClip buttonPressed;
        [SerializeField] private AudioClip windowOpened;

        public AudioMixerGroup UIMixer => uiMixer;

        public AudioMixerGroup MusicMixer => musicMixer;

        public AudioClip ButtonSelected => buttonSelected;

        public AudioClip ButtonPressed => buttonPressed;
        public AudioClip WindowOpened => windowOpened;

        public AudioClip[] MenuMusics => menuMusics;

        public AudioClip GetRandomMenuMusic()
        {
            int r = Random.Range(0, menuMusics.Length);
            return menuMusics[r];
        }
    }
}