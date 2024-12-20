﻿using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 敵人血量系統
    /// </summary>
    public class HpEnemy : HpSystem
    {
        [SerializeField, Header("敵人資料")]
        private DataEnemy dataEnemy;
        [SerializeField, Header("爆炸特效")]
        private GameObject prefabExplosion;

        private string weaponName = "武器";

        protected virtual void Awake()
        {
            hp = dataEnemy.hp;
        }

        // OnCollisionXXX
        // 條件:兩個物件都有碰撞器並且都沒有勾選 IsTrigger

        // OnTriggerXXX
        // 條件:兩個物件都有碰撞器並且其中一個勾選 IsTrigger

        // OnCollisionEnter 碰撞開始執行一次
        // OnCollisionStay 碰撞時持續執行:約60FPS
        // OnCollisionExit 碰撞結束執行一次

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //print($"<color=#f63>碰到的物件:{collision.gameObject.name}</color>");

            // 如果碰到的物件名稱有"武器"就受傷
            if (collision.gameObject.name.Contains(weaponName))
            {
                Damage(collision.gameObject.GetComponent<Weapon>().attack);
            }

        }

        public override void Damage(float damage)
        {
            base.Damage(damage);
            SoundManager.instance.PlaySound(SoundType.EnemyHit, 0.8f, 1.4f);
        }

        // 使用關鍵字 override 可以覆寫父類別有 virtual 的成員
        protected override void Dead()
        {
            // base 父類別原有的內容，可刪除
            base.Dead();

            //print("<color=#f66>生成爆炸特效</color>");
            GameObject tempExplosion = Instantiate(prefabExplosion, transform.position, Quaternion.identity);

            // 刪除此物件
            Destroy(gameObject);
            Destroy(tempExplosion, 1.5f);

            DropExpObject();
            SoundManager.instance.PlaySound(SoundType.EnemyDead, 0.8f, 1.4f);
        }

        /// <summary>
        /// 掉落經驗值物件
        /// </summary>
        private void DropExpObject()
        {
            // 如果 隨機值 < 掉落機率
            if (Random.value < dataEnemy.expProbability)
            {
                // 生成 經驗值物件 在敵人身上
                Instantiate(dataEnemy.prefabExp, transform.position, Quaternion.identity);
            }
        }
    }

}
