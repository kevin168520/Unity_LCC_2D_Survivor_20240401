using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 武器物件
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        // Hide In Inspector 在屬性面板隱藏，可以將不想要顯示的資料隱藏
        [HideInInspector]
        public float attack;

        private void Start () 
        {
            attack = Random.value <= UpgradeCriticalRate.instance.criticalRate ? attack * 2 : attack;
            // 浮動攻擊力 10 % 
            attack += Mathf.Floor(Random.Range(0, attack * 0.1f));
        }
    }
}

