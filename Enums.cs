namespace Master.QSpaceCode
{
    public enum PunState
    {
        ConnectedToMaster, ConnectedToLobby, ConnectedToRoom, Other
    }

    public enum MainMenuState
    {
        Title, Singleplayer, Multiplayer, ShipEditor
    }

    public enum MultiplayerMenuState
    {
        Connection, Login, Lobby, RoomSettings, Room
    }
}