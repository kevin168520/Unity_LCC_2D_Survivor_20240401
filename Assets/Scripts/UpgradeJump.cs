﻿using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 升級跳躍距離
    /// </summary>
    public class UpgradeJump : UpgradePlayer
    {
        public override void InitializePlayerData(float value)
        {
            base.InitializePlayerData(value);
            dataPlayer.jump = value;
        }

        public override void Upgrade(float increase)
        {
            base.Upgrade(increase);
            dataPlayer.jump += increase;
        }
    }

}
