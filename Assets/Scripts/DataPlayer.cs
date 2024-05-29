using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 玩家資料
    /// </summary>
    [CreateAssetMenu(menuName ="Kevin/Player")]
    public class DataPlayer : ScriptableObject
    {
        [Header("血量"), Range(0, 500000)]
        public float hp = 100;
        [Header("移動速度"), Range(0, 20)]
        public float moveSpeed = 4.5f;
        [Header("跳躍力道"), Range(0, 1500)]
        public float jump = 650;
        [Header("獲得經驗值範圍"), Range(0, 20)]
        public float getExpRadius = 3;
        [Header("爆擊率"), Range(0, 1)]
        public float criticalRate;
        
    }

}
