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
        event Action<string> OnUpdateLogin; 
        event Action<List<Player>> OnPlayersUpdate;
        event Action<List<RoomInfo>> OnRoomUpdate;

        string CurrentLogin { get; }
        string RoomName { get; }
        public List<RoomInfo> Rooms { get; }
        public List<Player> Players { get; }
    }

    public interface ISettingsKeeper
    {
        event Action OnChangeLocalization;
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

    public interface IUiStateKeeper
    {
        MainMenuState GetMainMenuState();
        event Action<MainMenuState> OnChangeMainMenuState;

        MultiplayerMenuState GetMultiplayerMenuState();
        event Action<MultiplayerMenuState> OnChangeMultiplayerMenuState;

        GameMenuState GetGameMenuState();
        event Action<GameMenuState> OnChangeGameMenuState;
        
        bool HasWindowsStack { get; }
    }

    public interface IUiInputKeeper
    {
        SystemInputMap GetSystemInputMap();
        
        event Action<SystemInputMap> OnChangeSystemInputMap; 
        
        event Action OnInputCancel;
        event Action OnInputPause;
        
        Vector2 MoveVector { get; }
        float Rotation { get; }

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
        void RegisterGameCamera(GameCamera gameCamera);
        void RegisterMinimapCamera(MinimapCamera minimapCamera);
    }

    public interface ISoundsKeeper
    {
        void PlayUiSound(AudioClip audioClip);
    }
}