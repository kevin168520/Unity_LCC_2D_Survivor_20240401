using UnityEngine;
using System.Collections;

namespace Kevin
{
    /// <summary>
    /// 敵人系統
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        #region 資料區域
        [SerializeField, Header("敵人資料")]
        private DataEnemy data;
        [SerializeField, Header("玩家圖層")]
        private LayerMask playerLayer;

        private Rigidbody2D rig;
        private Animator ani;
        private Transform playerPosition;
        /// <summary>
        /// 是否在攻擊中
        /// </summary>
        private bool isAttacking;
        #endregion

        #region 事件區域
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.8f, 0.6f, 0.5f, 0.3f);
            Gizmos.DrawSphere(transform.position, data.attackRange);

            Gizmos.color = new Color(1, 0.3f, 0.3f, 0.3f);
            Gizmos.DrawCube(
                transform.position +
                transform.TransformDirection(data.attackAreaOffset),
                data.attackAreaSize);
        }

        private void Awake()
        {
            rig = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();

            // 透過名稱尋找遊戲物件 GameObject.Find(物件名稱)
            // 玩家變形 = 尋找場景上名稱為"玩家_巫師"的物件 的 變形元件
            playerPosition = GameObject.Find(GameManager.playerName).transform;
        }



        // Update 一秒執行約 60 次
        private void Update()
        {
            Flip();
            Attack();
        }

        // FixedUpdate 固定更新事件:一秒執行約 50 次 (每偵 0.02 秒) 建議執行物理
        private void FixedUpdate()
        {
            Move();
        } 
        #endregion

        #region 方法區域
        /// <summary>
        /// 移動方法
        /// </summary>
        private void Move()
        {
            // 如果 距離 小於 攻擊範圍就跳出
            if (CheckDistance() < data.attackRange) return;
            // 如果 攻擊中 就 跳出
            if (isAttacking) return;

            // 目前座標 = 敵人鋼體的座標
            Vector2 currentPoint = rig.position;
            // 玩家座標 = 玩家座標
            Vector2 playerPoint = playerPosition.position;
            // 玩家座標的Y = 目前座標的Y
            playerPoint.y = currentPoint.y;
            // 方向 = 玩家座標 - 目前座標
            Vector2 direction = playerPoint - currentPoint;
            // 要前往的座標 = 目前座標 + 方向 * 移動速度 * 每固定偵的時間
            Vector2 movePosition = currentPoint + direction * data.moveSpeed * Time.fixedDeltaTime;

            // 鋼體移動座標(要前往的座標)
            rig.MovePosition(movePosition);
        }

        /// <summary>
        /// 翻面, 如果敵人的 X 大於玩家的 X
        /// </summary>
        private void Flip()
        {
            float angle = transform.position.x > playerPosition.position.x ? 180 : 0;
            transform.eulerAngles = new Vector3(0, angle, 0);
        }

        /// <summary>
        /// 檢查與玩家距離
        /// </summary>
        /// <returns></returns>
        private float CheckDistance()
        {
            // 距離 = 二維向量.距離(A,B點)
            float dis = Vector2.Distance(transform.position, playerPosition.position);
            //print($"<color=f96>距離:{dis}</color>");
            return dis;
        }

        /// <summary>
        /// 攻擊
        /// </summary>
        private void Attack()
        {
            // 如果距離大於等於攻擊範圍就跳出
            if (CheckDistance() >= data.attackRange) return;
            // 如果正在攻擊中就跳出
            if(isAttacking) return;

            StartCoroutine(StartAttack());
        } 

        private IEnumerator StartAttack()
        {
            //正在攻擊中
            isAttacking = true;
            print("<color=#f33>開始攻擊</color>");
            ani.SetTrigger(GameManager.parAttack);
            yield return new WaitForSeconds(data.attackBefore);
            print("<color=#f33>前搖結束</color>");
            AttackPlayer();
            yield return new WaitForSeconds(data.attackAfter);
            print("<color=#f33>後搖結束</color>");
            // 恢復沒有在攻擊中
            isAttacking = false;
        }
        #endregion

        private void AttackPlayer()
        {
            Collider2D hit = Physics2D.OverlapBox(
                transform.position +
                transform.TransformDirection(data.attackAreaOffset),
                data.attackAreaSize, 0, playerLayer);

            print($"<color=#f69>攻擊到的物件:{hit?.name}</color>");
        }
    }
}

