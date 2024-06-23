using UnityEngine;

namespace Kevin
{
    public class ControlSystem : MonoBehaviour
    {
        #region 基本資料區域
        // private 私人資料顯示在面板上需要加上 SerializeField 序列化欄位
        // Range 數字範圍
        [SerializeField, Header("玩家資料")]
        private DataPlayer dataPlayer;
        [SerializeField, Header("剛體元件")]
        private Rigidbody2D rig;
        [SerializeField, Header("動畫控制元件")]
        private Animator ani;
       

        private string parMove = "移動數值";
        private string parJump = "觸發跳躍";

        public bool canMove { get; set; } = false;
        public bool canJump { get; set; } = false;

        [SerializeField, Header("檢查地板位移")]
        private Vector3 checkGroundOffset;
        [SerializeField, Header("檢查地板尺寸")]
        private Vector3 checkGroundSize = Vector3.one;
        [SerializeField, Header("可跳躍圖層")]
        private LayerMask layerCanJump;
        [SerializeField, Header("玩家血量系統")]
        private HpPlayer hpPlayer;

        #endregion

        #region 事件區域
        // 繪製圖示事件
        private void OnDrawGizmos()
        {
            // 1. 決定顏色 Color(紅，綠，藍，透明度) 數值 0~1
            Gizmos.color = new Color(0.2f, 1, 0.3f, 0.5f);
            // 2. 繪製圖示
            // 繪製方塊(座標，尺寸)
            // transform.position 此角色座標
            Gizmos.DrawCube(transform.position + checkGroundOffset, checkGroundSize);
        }

        private void Awake()
        {
            hpPlayer.onDead += CloseControlSystem;
        }

        private void CloseControlSystem(object sender, System.EventArgs e)
        {
            enabled = false;
        }

        private void Start()
        {
            //print("<color=red>開始事件</color>");
        }

        private void Update()
        {
            //print("<color=#f69>更新事件</color>");

            // Vertical 垂直軸 : W，S 與 上下
            // Horizontal 水平軸 : 獲得玩家水平按鍵的值
            // A、D 與 方向鍵
            // 按左 : -1
            // 按右 : +1
            // 沒按 : +0
            float h = Input.GetAxis("Horizontal");

            Move(h);

            Flip(h);

            IsGround();

            Jump();

        }
        #endregion

        #region 方法區域
        /// <summary>
        /// 翻面功能
        /// </summary>
        /// <param name="h">玩家水平按鍵值</param>
        private void Flip(float h)
        {
            // 如果 h 取絕對值小於 0.1 就跳出
            // Mathf.Abs(數值) 對小括號內的數值取絕對值
            if (Mathf.Abs(h) < 0.1f) return;

            // 如果 h > 0 角度0 , h < 0 角度設定 180
            // 三源運算子 - 布林值 ? 布林值 true : 布林值 false
            float angle = h > 0 ? 0 : 180;
            // 此角色變形元件 的 歐拉角度 = 新 三維向量(0, 角度, 0)
            transform.eulerAngles = new Vector3(0, angle, 0);
        }

        /// <summary>
        /// 移動方法
        /// </summary>
        /// <param name="h">玩家水平按鍵值</param>
        private void Move(float h)
        {
            if(!canMove) return;

            // 剛體 的 加速度 = 新 二維向量(移動速度， 原本剛體的 Y 軸加速度)
            rig.velocity = new Vector2(dataPlayer.moveSpeed * h, rig.velocity.y);

            // 動畫控制元件 的 設定浮點數(參數名稱，浮點數)
            ani.SetFloat(parMove, rig.velocity.magnitude / dataPlayer.moveSpeed);
        }

        /// <summary>
        /// 是否在地板上
        /// </summary>
        /// <returns>是否在地板上布林值，true 是，false 否</returns>
        private bool IsGround()
        {
            // 2D物理.覆蓋方塊(座標，尺寸，角度)
            Collider2D hit = Physics2D.OverlapBox(transform.position + checkGroundOffset, checkGroundSize, 0, layerCanJump);
            print($"<color=f3d>碰到的物件:{hit?.gameObject.name}</color>");
            // 傳回是否有碰到物件
            return hit;
        }

        private void Jump()
        {
            if( !canJump ) return;

            // 如果 在地板上 並且 按下空白鍵 就 跳躍
            // && 並且 Shift + 鍵盤左邊的 7
            // Input.GetKeyDown() 玩家是否按下按鍵
            // KeyCode.Space 空白按鍵
            // KeyCode.UpArrow
            // KeyCode.W
            if (IsGround() && Input.GetKeyDown(KeyCode.Space))
            {
                // 鋼體推力(向上推)
                rig.AddForce(new Vector2(0, dataPlayer.jump));
                // 動畫的設定觸發參數(跳躍參數)
                ani.SetTrigger(parJump);
            }
        }
        #endregion
    }


}
