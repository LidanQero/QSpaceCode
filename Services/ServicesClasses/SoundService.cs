using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using UnityEngine;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public class SoundService : Service, ISoundService, ISoundsKeeper
    {
        public SoundService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }

        private AudioSource uiAudioSource;
        private AudioSource musicAudioSource;

        public override void InitOnAwake()
        {
            base.InitOnAwake();

            uiAudioSource = new GameObject("UI Audio Source").AddComponent<AudioSource>();
            Object.DontDestroyOnLoad(uiAudioSource.gameObject);
            uiAudioSource.loop = false;
            uiAudioSource.ignoreListenerPause = true;
            uiAudioSource.outputAudioMixerGroup = Core.AudioConfig.UIMixer;
            uiAudioSource.spatialize = false;
            uiAudioSource.spatialBlend = 0;

            musicAudioSource = new GameObject("Music Audio Source").AddComponent<AudioSource>();
            Object.DontDestroyOnLoad(musicAudioSource.gameObject);
            musicAudioSource.loop = false;
            musicAudioSource.ignoreListenerPause = true;
            musicAudioSource.outputAudioMixerGroup = Core.AudioConfig.MusicMixer;
            musicAudioSource.spatialize = false;
            musicAudioSource.spatialBlend = 0;
        }

        public override void Runtime()
        {
            base.Runtime();
            if (!musicAudioSource.isPlaying) 
                PlayMusic(Core.AudioConfig.GetRandomMenuMusic());
        }

        public void PlayUiSound(AudioClip audioClip)
        {
            if (audioClip && uiAudioSource) uiAudioSource.PlayOneShot(audioClip);
        }

        public void PlayMusic(AudioClip audioClip)
        {
            if (audioClip && musicAudioSource)
            {
                musicAudioSource.clip = audioClip;
                musicAudioSource.Play();
            }
        }
    }
}