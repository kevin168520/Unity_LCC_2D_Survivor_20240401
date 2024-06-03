using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 升級跑步速度
    /// </summary>
    public class UpgradeMoveSpeed : UpgradePlayer
    {
        public override void InitializePlayerData(float value)
        {
            base.InitializePlayerData(value);
            dataPlayer.moveSpeed = value;
        }

        public override void Upgrade(float increase)
        {
            base.Upgrade(increase);
            dataPlayer.moveSpeed += increase;
        }
    }
}

