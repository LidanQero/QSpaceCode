using UnityEngine;

namespace Master.QSpaceCode.Services.Mediator
{
    public sealed partial class ServicesMediator
    {
        public void ChangeResolution(int id)
        {
            settingsService.SetResolutionId(id);
        }

        public void ChangeFullscreenMode(int id)
        {
            settingsService.SetFullscreenModeId(id);
        }

        public void ChangeQuality(int id)
        {
            settingsService.SetQualityId(id);
        }

        public void ChangeAliasing(int id)
        {
            settingsService.SetAliasingId(id);
        }

        public void ChangeMusicVolume(float volume)
        {
            settingsService.SetMusicVolume(volume);
        }

        public void ChangeUiVolume(float volume)
        {
            settingsService.SetUiVolume(volume);
        }

        public void ChangeGameVolume(float volume)
        {
            settingsService.SetGameVolume(volume);
        }

        public void ChangeLocalization(int id)
        {
            settingsService.SetLanguageId(id);
        }

        public void SavePlayerSettings()
        {
            settingsService.SaveSettings();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}