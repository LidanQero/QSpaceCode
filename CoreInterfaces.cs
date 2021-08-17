using System;

namespace Master.QSpaceCode
{
    public interface IGameInfoStorage
    {
        
    }

    public interface IPunInfoStorage
    {
        
    }

    public interface IUiStateKeeper
    {
        MainMenuState GetMainMenuState();
        event Action<MainMenuState> ChangeMainMenuStateEvent;

        MultiplayerMenuState GetMultiplayerMenuState();
        event Action<MultiplayerMenuState> ChangeMultiplayerMenuStateEvent;
    }

    public interface IUiInputListener
    {
        
    }

    public interface IViewersManager
    {
        
    }
}