namespace Master.QSpaceCode.Services.Mediator
{
    public sealed partial class ServicesMediator
    {
        public void FinishLoadingMenuScene()
        {
            uiService.OpenMainMenuTitle();
        }
        
        public void FinishLoadingLoadingScene()
        {
            
        }
        
        public void FinishLoadingGameScene()
        {
            uiService.OpenGameMain();
            gameLogicService.PrepareToGame();
        }
    }
}