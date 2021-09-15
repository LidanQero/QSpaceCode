using System;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesClasses.SettingsServiceSubclasses;
using Master.QSpaceCode.Services.ServicesInterfaces;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using WebSocketSharp;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class SettingsService : Service, ISettingsKeeper, ISettingsService
    {
        public SettingsService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }

        public event Action ChangeLocalizationEvent;

        private PlayerPrefsStorage playerPrefsStorage = new PlayerPrefsStorage();

        private PostProcessLayer postProcessLayer;
        private PostProcessVolume postProcessVolume;
        
        private const string QualityPrefs = "QualityPrefs";
        private const string AliasingPrefs = "AliasingPrefs";
        private const string LanguagePrefs = "LanguagePrefs";
        private const string MusicVolumePrefs = "MusicVolumePrefs";
        private const string GameVolumePrefs = "GameVolumePrefs";
        private const string UiVolumePrefs = "UiVolumePrefs";

        private float currentMusicVolume;
        private float currentGameVolume;
        private float currentUiVolume;

        public override void InitOnAwake()
        {
            base.InitOnAwake();

            Application.targetFrameRate = 60;

            LocalizationManager.Read();

            int localizationId = 0;
            if (Application.systemLanguage == SystemLanguage.Russian) localizationId = 1;
            
            playerPrefsStorage.CheckDefaultInt(QualityPrefs, GetQualityId());
            playerPrefsStorage.CheckDefaultInt(AliasingPrefs, GetAliasingId());
            playerPrefsStorage.CheckDefaultInt(LanguagePrefs, localizationId);
            playerPrefsStorage.CheckDefaultFloat(MusicVolumePrefs, 0.6f);
            playerPrefsStorage.CheckDefaultFloat(UiVolumePrefs, 0.8f);
            playerPrefsStorage.CheckDefaultFloat(GameVolumePrefs, 0.8f);
            playerPrefsStorage.Save();
            
            SetQualityId(playerPrefsStorage.GetInt(QualityPrefs));
            SetAliasingId(playerPrefsStorage.GetInt(AliasingPrefs));
            SetLanguageId(playerPrefsStorage.GetInt(LanguagePrefs));
        }

        public override void InitOnStart()
        {
            base.InitOnStart();
            SetMusicVolume(playerPrefsStorage.GetFloat(MusicVolumePrefs));
            SetGameVolume(playerPrefsStorage.GetFloat(GameVolumePrefs));
            SetUiVolume(playerPrefsStorage.GetFloat(UiVolumePrefs));
        }

        public string GetLocalizedText(string key)
        {
            if (key.IsNullOrEmpty()) return string.Empty;
            return LocalizationManager.Localize(key);
        }

        public string[] GetLocalizationVariants()
        {
            return new[] {"English", "Русский"};
        }

        public int GetLocalizationId()
        {
            switch (LocalizationManager.Language)
            {
                case "Russian": return 1;
                default: return 0;
            }
        }

        public void SetCurrentPostProcess(PostProcessVolume volume, PostProcessLayer layer)
        {
            postProcessVolume = volume;
            postProcessLayer = layer;
        }

        public string[] GetResolutionVariants()
        {
            var resolutions = GetUniqueResolutions();
            var variants = new string[resolutions.Length];
            for (int i = 0; i < resolutions.Length; i++)
            {
                Resolution res = resolutions[i];
                variants[i] = $"{res.width} x {res.height}";
            }

            return variants;
        }

        public int GetResolutionId()
        {
            var resolutions = GetUniqueResolutions();
            var current = new Resolution
            {
                height = Screen.height,
                width = Screen.width
            };
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].height == current.height &&
                    resolutions[i].width == current.width) return i;
            }

            return 0;
        }

        public string[] GetFullscreenVariants()
        {
            return new[] {"Windowed", "Fullscreen Window", "Exclusive Fullscreen"};
        }

        public int GetFullscreenId()
        {
            switch (Screen.fullScreenMode)
            {
                case FullScreenMode.FullScreenWindow: return 1;
                case FullScreenMode.ExclusiveFullScreen: return 2;
                default: return 0;
            }
        }

        public string[] GetQualityVariants()
        {
            return QualitySettings.names;
        }

        public int GetQualityId()
        {
            return QualitySettings.GetQualityLevel();
        }

        public string[] GetAliasingVariants()
        {
            return new[] {"Low", "Medium", "High"};
        }

        public int GetAliasingId()
        {
            if (postProcessLayer) 
                return (int) postProcessLayer.subpixelMorphologicalAntialiasing.quality;
            return 0;
        }

        public float GetMusicVolume()
        {
            return currentMusicVolume;
        }

        public float GetGameVolume()
        {
            return currentGameVolume;
        }

        public float GetUiVolume()
        {
            return currentUiVolume;
        }

        public void SetResolutionId(int id)
        {
            var resolutions = GetUniqueResolutions();
            Screen.SetResolution(
                resolutions[id].width,
                resolutions[id].height,
                Screen.fullScreenMode);
        }

        public void SetFullscreenModeId(int id)
        {
            var current = new Resolution
            {
                height = Screen.height,
                width = Screen.width,
                refreshRate = Screen.currentResolution.refreshRate
            };

            FullScreenMode fullScreenMode = id switch
            {
                1 => FullScreenMode.FullScreenWindow,
                2 => FullScreenMode.ExclusiveFullScreen,
                _ => FullScreenMode.Windowed
            };

            Screen.SetResolution(
                current.width,
                current.height,
                fullScreenMode,
                current.refreshRate);
        }

        public void SetQualityId(int id)
        {
            QualitySettings.SetQualityLevel(id);
        }

        public void SetAliasingId(int id)
        {
            if (postProcessLayer) postProcessLayer.subpixelMorphologicalAntialiasing.quality =
                (SubpixelMorphologicalAntialiasing.Quality) id;
        }

        public void SetLanguageId(int id)
        {
            switch (id)
            {
                case 1:
                    ChangeLanguage("Russian");
                    break;
                default:
                    ChangeLanguage("English");
                    break;
            }
        }
        
        private void ChangeLanguage(string value)
        {
            LocalizationManager.Language = value;
            ChangeLocalizationEvent?.Invoke();
        }

        public void SetMusicVolume(float volume)
        {
            currentMusicVolume = volume;
            CurrentConfigs.AudioConfig.MusicMixer.audioMixer.SetFloat(
                "MusicVolume", Mathf.Log10(volume) * 20);
        }

        public void SetUiVolume(float volume)
        {
            currentUiVolume = volume;
            CurrentConfigs.AudioConfig.MusicMixer.audioMixer.SetFloat(
                "UiVolume", Mathf.Log10(volume) * 20);
        }

        public void SetGameVolume(float volume)
        {
            currentGameVolume = volume;
            CurrentConfigs.AudioConfig.MusicMixer.audioMixer.SetFloat(
                "GameVolume", Mathf.Log10(volume) * 20);
        }

        public void SaveSettings()
        {
            playerPrefsStorage.SetInt(QualityPrefs, GetQualityId());
            playerPrefsStorage.SetInt(AliasingPrefs, GetAliasingId());
            playerPrefsStorage.SetInt(LanguagePrefs, GetLocalizationId());
            playerPrefsStorage.SetFloat(MusicVolumePrefs, GetMusicVolume());
            playerPrefsStorage.SetFloat(UiVolumePrefs, GetUiVolume());
            playerPrefsStorage.SetFloat(GameVolumePrefs, GetGameVolume());
            playerPrefsStorage.Save();
        }

        private Resolution[] GetUniqueResolutions()
        {
            var resolutions = Screen.resolutions;
            var uniqueResolutions = new List<Resolution>();
            foreach (var resolution in resolutions)
            {
                bool contain = false;
                foreach (var uniqueResolution in uniqueResolutions)
                {
                    if (uniqueResolution.height == resolution.height &&
                        uniqueResolution.width == resolution.width)
                    {
                        contain = true;
                        break;
                    }
                }
                if (contain) continue;
                uniqueResolutions.Add(resolution);
            }

            return uniqueResolutions.ToArray();
        }
    }
}