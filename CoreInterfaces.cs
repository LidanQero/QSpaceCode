using System;
using System.Collections.Generic;
using Master.QSpaceCode.Game;
using Master.QSpaceCode.PlayerUi;
using Photon.Realtime;
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
        event Action<string> UpdateLoginEvent; 
        event Action<List<Player>> PlayersUpdateEvent;
        event Action<List<RoomInfo>> RoomsUpdateEvent;

        string GetCurrentLogin();
        string GetRoomName();
        public List<RoomInfo> GetRooms();
        public List<Player> GetPlayers();
    }

    public interface IUiStateKeeper
    {
        MainMenuState GetMainMenuState();
        event Action<MainMenuState> ChangeMainMenuStateEvent;

        MultiplayerMenuState GetMultiplayerMenuState();
        event Action<MultiplayerMenuState> ChangeMultiplayerMenuStateEvent;

        GameMenuState GetGameMenuState();
        event Action<GameMenuState> ChangeGameMenuStateEvent;
        
        bool HasWindowsStack { get; }
    }

    public interface IUiInputKeeper
    {
        SystemInputMap GetSystemInputMap();
        event Action<SystemInputMap> ChangeSystemInputMapEvent; 
        
        event Action InputCancelEvent;
        event Action InputPauseEvent;

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
        void RegisterPunObject(PunObject punObject);
        void DeletePunObject(PunObject punObject);
        void RegisterLocalObject(LocalObject localObject);
        void DeleteLocalObject(LocalObject localObject);
    }

    public interface ISoundsKeeper
    {
        void PlayUiSound(AudioClip audioClip);
    }
}