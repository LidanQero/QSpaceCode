using System;
using Master.QSpaceCode.PlayerUi;

namespace Master.QSpaceCode
{
    public interface IGameInfoKeeper
    {
        event Action ChangeLocalizationEvent;
        string GetLocalizedText(string key);
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
    }

    public interface IViewersKeeper
    {
        
    }
}