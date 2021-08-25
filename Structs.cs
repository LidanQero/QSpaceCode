using System;
using Master.QSpaceCode.Game.Player;

namespace Master.QSpaceCode
{
    [Serializable] public struct ShipConfig
    {
        public string shell;
        public string firstWeapon;
        public string secondWeapon;
        public string thirdWeapon;
        public string shield;
        public string core;
    }
}