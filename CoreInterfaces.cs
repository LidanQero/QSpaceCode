using System;
using Master.QSpaceCode.PlayerUi;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Master.QSpaceCode
{
    public interface IGameInfoKeeper
    {
        
    }

    public interface ISettingsKeeper
    {
        event Action ChangeLocalizationEvent;
        string GetLocalizedText(string key);
        string[] GetLocalizationVariants();
        int GetLocalizationId();

        void SetCurrentPostProcess(PostProcessVolume volume, PostProcessLayer layer);
        
        string[] GetResolutionVariants();
        int GetResolutionId();

        string[] GetFullscreenVariants();
        int GetFullscreenId();
        
        string[] GetQualityVariants();
        int GetQualityId();
        
        string[] GetAliasingVariants();
        int GetAliasingId();

        float GetMusicVolume();
        float GetGameVolume();
        float GetUiVolume();
    }

    public interface IPunInfoKeeper
    {
        
    }

    public interface IUiStateKeeper
    {
        MainMenuState GetMainMenuState();
        event Action<MainMenuState> ChangeMainMenuStateEvent;

        MultiplayerMenuState GetMultiplayerMenuState();
        event Action<MultiplayerMenuState> ChangeMultiplayerMenuStateEvent;
    }

    public interface IUiInputKeeper
    {
        SystemInputMap GetSystemInputMap();
        event Action<SystemInputMap> ChangeSystemInputMapEvent; 
        
        event Action InputCancelEvent;

        void AddButton(UiButton uiButton);
        void RemoveButton(UiButton uiButton);

        void AddSlider(UiSlider uiSlider);
        void RemoveSlider(UiSlider uiSlider);

        void AddToggle(UiToggle uiToggle);
        void RemoveToggle(UiToggle uiToggle);

        void AddDropdown(UiDropdown uiDropdown);
        void RemoveDropdown(UiDropdown uiDropdown);
    }

    public interface IViewersKeeper
    {
        
    }

    public interface ISoundsKeeper
    {
        void PlayUiSound(AudioClip audioClip);
    }
}