using System;
using Master.QSpaceCode.Game.Player;

namespace Master.QSpaceCode
{
    [Serializable] public struct ShipConfig
    {
        public ShipShell shell;
        public ShipWeapon firstWeapon;
        public ShipWeapon secondWeapon;
        public ShipWeapon thirdWeapon;
        public ShipShield shield;
        public ShipCore core;
    }
}