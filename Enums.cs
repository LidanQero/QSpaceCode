namespace Master.QSpaceCode
{
    public enum PunState
    {
        Login, ConnectedToLobby, ConnectedToRoom, Other
    }

    public enum MainMenuState
    {
        Title, Singleplayer, Multiplayer, ShipEditor, GameSettings, GraphicSettings, Exit
    }

    public enum MultiplayerMenuState
    {
        Connection, Login, Lobby, RoomSettings, Room
    }

    public enum SystemInputMap
    {
        Keyboard, Xbox, PS
    }
}