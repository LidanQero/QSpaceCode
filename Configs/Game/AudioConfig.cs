using UnityEngine;
using UnityEngine.Audio;

namespace Master.QSpaceCode.Configs.Game
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Game Configs/Audio Config", order = 0)]
    public sealed class AudioConfig : ScriptableObject
    {
        [SerializeField] private AudioMixerGroup uiMixer;
        [SerializeField] private AudioMixerGroup musicMixer;
        [Space] [SerializeField] private AudioClip[] menuMusics;
        [Space] [SerializeField] private AudioClip buttonSelected;
        [SerializeField] private AudioClip buttonPressed;
        [SerializeField] private AudioClip windowOpened;
        [SerializeField] private AudioClip sliderChanged;
        [SerializeField] private AudioClip toggleChanged;
        [SerializeField] private AudioClip dropdownChanged;

        public AudioMixerGroup UIMixer => uiMixer;

        public AudioMixerGroup MusicMixer => musicMixer;

        public AudioClip ButtonSelected => buttonSelected;
        public AudioClip ButtonPressed => buttonPressed;
        public AudioClip WindowOpened => windowOpened;
        public AudioClip SliderChanged => sliderChanged;
        public AudioClip ToggleChanged => toggleChanged;
        public AudioClip DropdownChanged => dropdownChanged;


        public AudioClip[] MenuMusics => menuMusics;

        public AudioClip GetRandomMenuMusic()
        {
            int r = Random.Range(0, menuMusics.Length);
            return menuMusics[r];
        }
    }
}