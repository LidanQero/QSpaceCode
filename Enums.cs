namespace Master.QSpaceCode
{
    public enum PunState
    {
        ConnectedToMaster, ConnectedToLobby, ConnectedToRoom, Other
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