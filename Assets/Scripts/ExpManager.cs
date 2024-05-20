using UnityEngine;


namespace Kevin
{
    /// <summary>
    /// 經驗值管理器
    /// </summary>
    public class ExpManager : MonoBehaviour
    {
        /// <summary>
        /// 玩家目前的經驗值
        /// </summary>
        private float expCurrent;
        /// <summary>
        /// 經驗值需求:需要多少才會升級
        /// </summary>
        private float expNeed;
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
    }

}
