using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 血量系統
    /// </summary>
    public class HpSystem : MonoBehaviour
    {
        [SerializeField, Header("畫布傷害值欲置物")]
        private GameObject prefabDamageCanvas;

        protected float hp;

        /// <summary>
        /// 受傷
        /// </summary>
        /// <param name="damage"></param>
        protected void Damage(float damage)
        {
            Instantiate(prefabDamageCanvas, transform.position,Quaternion.identity);
            hp -= damage;
            print($"<color=#f33>{gameObject.name}受傷，血量剩下:{hp}</color>");

            // 如果血量<=0死亡
            if (hp <= 0) Dead();
        }

        // virtual 虛擬關鍵字: 允許使用子類別使用關鍵字 override 覆寫
        /// <summary>
        /// 死亡
        /// </summary>
        protected virtual void Dead()
        {
            print($"<color=#f33>{gameObject.name}死亡</color>");
        }
    }
}

