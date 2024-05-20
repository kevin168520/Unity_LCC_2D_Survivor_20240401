using System;
using UnityEngine;
using System.Collections;


namespace Kevin
{
    /// <summary>
    /// 遊戲管理器
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region 常數資料
        // const 常數 : 不變的資料
        public const string playerName = "玩家_巫師";
        /// <summary>
        /// 動畫控制器參數 : 移動數值
        /// </summary>
        public const string parMove = "移動數值";
        /// <summary>
        /// 動畫控制器參數 : 觸發攻擊
        /// </summary>
        public const string parAttack = "觸發攻擊";
        #endregion

        #region 一般資料
        [SerializeField, Header("玩家血量系統")]
        private HpPlayer hpPlayer;
        [SerializeField, Header("畫布結束畫面")]
        private CanvasGroup groupFinal; 
        #endregion

        private void Awake()
        {
            hpPlayer.onDead += ShowFinalCanvas; 
        }

        /// <summary>
        /// 顯示結束畫面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ShowFinalCanvas(object sender, EventArgs e)
        {
            StartCoroutine(FadeCanvas());
        }

        private IEnumerator FadeCanvas()
        {
            for (int i = 0; i < 10; i++)
            {
                // 透明度 遞增 0.1
                groupFinal.alpha += 0.1f;
                yield return new WaitForSeconds(0.05f);
            }

            // 畫布群組勾選互動 與 射線遮擋
            groupFinal.interactable = true;
            groupFinal.blocksRaycasts = true;
        }
    }
}

