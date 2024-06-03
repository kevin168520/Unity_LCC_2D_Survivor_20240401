using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 升級經驗值拾取範圍
    /// </summary>
    public class UpgradeGetExpRadius : UpgradePlayer
    {
        [SerializeField, Header("獲得經驗值系統 : 圖形碰撞器")]
        private CircleCollider2D getExpCollider;

        public override void InitializePlayerData(float value)
        {
            base.InitializePlayerData(value);
            dataPlayer.getExpRadius = value;
        }

        public override void Upgrade(float increase)
        {
            base.Upgrade(increase);
            dataPlayer.getExpRadius += increase;
            getExpCollider.radius = dataPlayer.getExpRadius;
        }
    }

}
