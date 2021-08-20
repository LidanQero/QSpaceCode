namespace Master.QSpaceCode.Services.ServicesInterfaces
{
    public interface ISettingsService
    {
        void SetResolutionId(int id);
        void SetFullscreenModeId(int id);
        void SetQualityId(int id);
        void SetAliasingId(int id);
        void SetLanguageId(int id);
        void SetMusicVolume(float volume);
        void SetUiVolume(float volume);
        void SetGameVolume(float volume);
        void SaveSettings();
    }
}