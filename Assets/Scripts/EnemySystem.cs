using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 敵人系統
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        [SerializeField, Header("敵人資料")]
        private DataEnemy data;

        private Rigidbody2D rig;
        private Animator ani;
        private Transform playerPosition;

        private void Awake()
        {
            rig = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();

            // 透過名稱尋找遊戲物件 GameObject.Find(物件名稱)
            // 玩家變形 = 尋找場景上名稱為"玩家_巫師"的物件 的 變形元件
            playerPosition = GameObject.Find(GameManager.playerName).transform;
        }

        // Update 一秒執行約 60 次
        // FixedUpdate 固定更新事件:一秒執行約 50 次 (每偵 0.02 秒) 建議執行物理
        private void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// 移動方法
        /// </summary>
        private void Move()
        {
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
    }
}

