using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Master.QSpaceCode.Services.ServicesInterfaces
{
    public interface IScenesService
    {
        void LoadGameScene();
        void LoadMenuScene();
        void TestLoadingScenesForPun(Player targetPlayer, Hashtable changedProps);
    }
}