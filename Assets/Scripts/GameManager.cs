using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


namespace Kevin
{
    /// <summary>
    /// 遊戲管理器
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

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

        private Button btnReplay, btnQuit;
        private TMP_Text textFinalTitle;
        #endregion

        private void Awake()
        {
            instance = this;

            textFinalTitle = GameObject.Find("文字結束標題").GetComponent<TMP_Text>();
            btnReplay = GameObject.Find("按鈕重新遊戲").GetComponent<Button>();
            btnQuit = GameObject.Find("按鈕結束遊戲").GetComponent <Button>();
            btnReplay.onClick.AddListener(Replay);
            btnQuit.onClick.AddListener(Quit);

            hpPlayer.onDead += ShowLoseCanvas; 
           // HpBoss.instance.onDead += ShowWinCanvas;
        }

        /// <summary>
        /// 顯示勝利畫面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ShowWinCanvas()
        {
            textFinalTitle.text = "恭喜你活下來^^";
            StartCoroutine(FadeCanvas());
            SoundManager.instance.PlaySound(SoundType.Win);
        }


        /// <summary>
        /// 顯示結束畫面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ShowLoseCanvas(object sender, EventArgs e)
        {
            textFinalTitle.text = "挑戰失敗";
            StartCoroutine(FadeCanvas());
            SoundManager.instance.PlaySound(SoundType.Lose, 1.2f, 1.3f);
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

        private void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Quit()
        {
            // 應用程式.離開 - 僅在執行檔有作用 (Exe 或 APK)
            Application.Quit();
        }
    }
}

