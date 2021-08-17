using System;

namespace Master.QSpaceCode
{
    public interface IGameInfoKeeper
    {
        
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
        
    }

    public interface IViewersKeeper
    {
        
    }
}