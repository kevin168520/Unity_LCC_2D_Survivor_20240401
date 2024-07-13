using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Kevin
{
    /// <summary>
    /// 技能管理器 : 技能資料與技能介面管理
    /// </summary>
    // 預設執行順序 : 腳本預設執行順序，數字越大越慢執行
    [DefaultExecutionOrder(100)]
    public class SkillManager : MonoBehaviour
    {
        #region 資料區域
        [SerializeField, Header("技能資料")]
        private DataSkill[] dataSkills;
        [SerializeField, Header("畫布升級畫面")]
        private CanvasGroup groupUpgrade;
        [SerializeField, Header("按鈕技能 1 ~ 3")]
        private Button[] btnSkills;

        private string nameTextSkill = "文字技能名稱";
        private string nameImageSkill = "圖片技能圖片";
        private string nameTextUpgradeDescription = "文字技能提昇說明";
        private string nameTextDescription = "文字技能描述";
        private string nameGroupLv = "圖片等級";
        private string nameImageLv = "圖片等級_";

        /// <summary>
        /// 技能圖片
        /// </summary>
        [SerializeField]
        private List<DataSkill> dataSkillShuffle;
        [SerializeField, Header("圖片等級 :　顯示的顏色")]
        private Color colorSkillLvShow;
        [SerializeField, Header("圖片等級 :　隱藏的顏色")]
        private Color colorSkillLvHide;

        #endregion
        /// <summary>
        /// 玩家點擊按鈕的編號
        /// </summary>
        private int btnPlayerClickIndex;

        [SerializeField, Header("升級系統1 ~ 7")]
        private GameObject[] goUpgradeSystem;
        [SerializeField, Header("按鈕技能全滿提示")]
        private Button btnAllSkillFull;

        private void Awake()
        {
            InitializePlayerData();
            ExpManager.instance.onUpgrade += PlayerUpgrade;
            ResetSkillLv();
            ButtonClickEvent();
            btnAllSkillFull.onClick.AddListener(() =>
            {
                StartCoroutine(FadeGroupUpgrade(false));
                Time.timeScale = 1;
            });
        }

        #region 方法
        /// <summary>
        /// 玩家升級
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerUpgrade(object sender, EventArgs e)
        {
            // 時間暫停 : 時間尺寸 = 0 (時間暫停)
            Time.timeScale = 0;
            ShuffleAndGetSkillData();
            UpdateSkillUI();
            StartCoroutine(FadeGroupUpgrade());
        }

        /// <summary>
        /// 淡入淡出升級畫面
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeGroupUpgrade(bool fadeIn = true)
        {
            float increase = fadeIn ? +0.1f : -0.1f;

            for (int i = 0; i < 10; i++)
            {
                groupUpgrade.alpha += increase;
                // WaitForSecondsRealTime 等待真實時間不被暫停
                yield return new WaitForSecondsRealtime(0.07f);
            }

            groupUpgrade.interactable = fadeIn;
            groupUpgrade.blocksRaycasts = fadeIn;
        }

        /// <summary>
        /// 洗牌並獲得技能資料 : 獲得等級 10 以下的資料
        /// </summary>
        private void ShuffleAndGetSkillData()
        {
            // 獲得等級 10 以下的資料
            var dataSkillLess10 = dataSkills.Where(skill => skill.skillLv < 10);
            // 洗牌
            dataSkillShuffle = dataSkillLess10.OrderBy(skill => Random.Range(0, 999)).ToList();

        }

        /// <summary>
        /// 更新技能介面
        /// </summary>
        private void UpdateSkillUI()
        {
            for (int i = 0; i < btnSkills.Length; i++)
            {
                if (i >= dataSkillShuffle.Count)
                {
                    btnSkills[i].gameObject.SetActive(false);
                    continue;
                }

                DataSkill data = dataSkillShuffle[i];
                btnSkills[i].transform.Find(nameTextSkill).GetComponent<TMP_Text>().text = data.skillName;
                btnSkills[i].transform.Find(nameImageSkill).GetComponent<Image>().sprite = data.skillSprite;
                btnSkills[i].transform.Find(nameTextUpgradeDescription).GetComponent<TMP_Text>().text = data.skillUpgrade;
                btnSkills[i].transform.Find(nameTextDescription).GetComponent<TMP_Text>().text = data.skillDescription;
                UpdateSkillLvSprite(i, data.skillLv);

            }
        }

        /// <summary>
        /// 更新技能圖片顏色
        /// </summary>
        /// <param name="btnIndex"></param>
        /// <param name="lv"></param>
        private void UpdateSkillLvSprite(int btnIndex, int lv)
        {
            for (int i = 1; i <= 10; i++)
            {
                btnSkills[btnIndex].transform.Find(nameGroupLv).Find(nameImageLv + i).GetComponent<Image>().color = colorSkillLvHide;
            }

            for (int i = 1; i <= lv; i++)
            {
                btnSkills[btnIndex].transform.Find(nameGroupLv).Find(nameImageLv + i).GetComponent<Image>().color = colorSkillLvShow;
            }
        }

        private void ButtonClickEvent()
        {
            for (int i = 0; i < btnSkills.Length; i++)
            {
                int index = i;

                btnSkills[i].onClick.AddListener(() =>
                {
                    btnPlayerClickIndex = index;
                    print($"<color=#96f>玩家點的按鈕編號 : {btnPlayerClickIndex}</color>");
                    StartCoroutine(UpgradeSkillEffect());
                });
            }
        }

        /// <summary>
        /// 升級技能效果
        /// </summary>
        /// <returns></returns>
        private IEnumerator UpgradeSkillEffect()
        {
            groupUpgrade.interactable = false;
            yield return new WaitForSecondsRealtime(0.5f);
            DataSkill dataPlyerClick = dataSkillShuffle[btnPlayerClickIndex];
            dataPlyerClick.skillLv++;
            UpdateSkillLvSprite(btnPlayerClickIndex, dataPlyerClick.skillLv);
            yield return new WaitForSecondsRealtime(1);
            UpgradeSkillObject(dataPlyerClick);
            yield return StartCoroutine(FadeGroupUpgrade(false)); // 淡出升級畫面
            Time.timeScale = 1;                                   // 時間尺吋恢復1
        }

        /// <summary>
        /// 升級技能物件
        /// </summary>
        private void UpgradeSkillObject(DataSkill dataPlayerClick)
        {
            int skillIndex = dataSkills.ToList().IndexOf(dataPlayerClick);
            print($"<color=#36f>玩家點的技能資料編號 : {skillIndex}</color>");
            goUpgradeSystem[skillIndex].GetComponent<IUpgrade>().Upgrade(dataPlayerClick.skillIncerease);
        }

        /// <summary>
        /// 重設計能等級
        /// </summary>
        private void ResetSkillLv()
        {
            for (int i = 0; i < dataSkills.Length; i++)
            {
                dataSkills[i].skillLv = 1;
            }
        }

        private void InitializePlayerData()
        {

            goUpgradeSystem[0].GetComponent<UpgradePlayer>().InitializePlayerData(dataSkills[0].skillInitilizeValue);
            goUpgradeSystem[1].GetComponent<UpgradePlayer>().InitializePlayerData(dataSkills[1].skillInitilizeValue);
            goUpgradeSystem[2].GetComponent<UpgradePlayer>().InitializePlayerData(dataSkills[2].skillInitilizeValue);
            goUpgradeSystem[3].GetComponent<UpgradePlayer>().InitializePlayerData(dataSkills[3].skillInitilizeValue);
            goUpgradeSystem[6].GetComponent<UpgradePlayer>().InitializePlayerData(dataSkills[6].skillInitilizeValue);
        } 
        #endregion
    }
}

