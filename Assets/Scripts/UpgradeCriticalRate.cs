using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 升級爆擊率
    /// </summary>
    public class UpgradeCriticalRate : UpgradePlayer
    {
        public static UpgradeCriticalRate instance
        {
            get
            {
                if (_instance == null) return FindAnyObjectByType<UpgradeCriticalRate>();
                return _instance;
            }
        }
        private static UpgradeCriticalRate _instance;

        public float criticalRate => dataPlayer.criticalRate;

        public override void InitializePlayerData(float value)
        {
            base.InitializePlayerData(value);
            dataPlayer.criticalRate = value;
        }

        public override void Upgrade(float increase)
        {
            base.Upgrade(increase);
            dataPlayer.criticalRate += increase;
        }
    }
}

