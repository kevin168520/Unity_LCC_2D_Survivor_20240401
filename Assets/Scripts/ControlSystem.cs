using UnityEngine;

namespace Kevin
{
    public class ControlSystem : MonoBehaviour
    {
        // private 私人資料顯示在面板上需要加上 SerializeField 序列化欄位
        // Range 數字範圍
        [SerializeField, Header("移動速度"), Range(0,10)]
        private float moveSpeed = 3.5f;
        [SerializeField, Header("剛體元件")]
        private Rigidbody2D rig;
        [SerializeField, Header("動畫控制元件")]
        private Animator ani;

        private string parMove = "移動數值";

        private void Awake()
        {
            print("給我動喔");
            print("<color=yellow>喚醒事件</color>");
        }

        private void Start()
        {
            print("<color=red>開始事件</color>");
        }

        private void Update()
        {
            print("<color=#f69>更新事件</color>");

            // Vertical 垂直軸 : W，S 與 上下
            // Horizontal 水平軸 : 獲得玩家水平按鍵的值
            // A、D 與 方向鍵
            // 按左 : -1
            // 按右 : +1
            // 沒按 : +0
            float h =  Input.GetAxis("Horizontal");

            // 剛體 的 加速度 = 新 二維向量(移動速度， 原本剛體的 Y 軸加速度)
            rig.velocity = new Vector2(moveSpeed * h, rig.velocity.y);
        }
    }

}
