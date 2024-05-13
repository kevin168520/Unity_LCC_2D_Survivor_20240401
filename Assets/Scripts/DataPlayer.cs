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
        public float hp;
    }

}
