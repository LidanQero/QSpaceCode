namespace Master.QSpaceCode.Services.Mediator
{
    public sealed partial class ServicesMediator
    {
        public void StartGame()
        {
            punService.CheckPunForGame();
        }
    }
}