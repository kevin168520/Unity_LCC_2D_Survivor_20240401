using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Kevin
{
    /// <summary>
    /// 經驗值管理器
    /// </summary>
    public class ExpManager : MonoBehaviour
    {
        public static ExpManager instance;
        /// <summary>
        /// 升級事件
        /// </summary>
        public event EventHandler onUpgrade;

        [SerializeField, Header("圖片經驗值")]
        private Image imgExp;
        [SerializeField, Header("文字經驗值")]
        private TMP_Text textExp;
        [SerializeField, Header("文字等級")]
        private TMP_Text textLv;

        /// <summary>
        /// 玩家目前的經驗值
        /// </summary>
        private float expCurrent;
        /// <summary>
        /// 經驗值需求:需要多少才會升級
        /// </summary>
        private float expNeed => expNeedTable[lv - 1];
        /// <summary>
        /// 等級
        /// </summary>
        private int lv = 1;
        /// <summary>
        /// 等級最大值
        /// </summary>
        private int lvMax = 100;

      

        /// <summary>
        /// 經驗值需求表格:所有等級經驗值需求
        /// </summary>
        [SerializeField]
        private float[] expNeedTable;

        private void Awake()
        {
            // 實體資料 = 此物件
            instance = this;
            UpdateUI();
        }

        private void Update()
        {
#if UNITY_EDITOR
            TestAddExp();
#endif
        }

        [ContextMenu("產生經驗值需求表格")]
        private void GeneratedExpNeedTable()
        {
            // 指定陣列的數量危等級最大值
            expNeedTable = new float[lvMax];

            for (int i = 0; i < lvMax; i++)
            {
                // 經驗值公式 等級需求為 等級 * 100
                expNeedTable[i] = (i + 1) * 100;
            }
        }

        /// <summary>
        /// 添加經驗值
        /// </summary>
        /// <param name="exp"></param>
        public void AddExp(float exp)
        {
            expCurrent += exp;

            if (expCurrent >= expNeed)
            {
                expCurrent -= expNeed;
                Upgrade();
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            imgExp.fillAmount = expCurrent / expNeed;
            textExp.text = $"{expCurrent}/{expNeed}";
        }

        /// <summary>
        /// 升級
        /// </summary>
        private void Upgrade()
        {
            lv++;
            textLv.text = $"Lv{lv}";
            onUpgrade?.Invoke(this, null);
        }
#if UNITY_EDITOR
        // #if UNITY_EDITOR 如果在編輯器內，此程式區塊才有作用
        /// <summary>
        /// 測試 : 添加經驗值
        /// </summary>
        private void TestAddExp()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddExp(100);
            }
        }
    }
#endif
}
