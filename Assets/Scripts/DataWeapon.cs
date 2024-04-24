using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 武器資料
    /// </summary>
    // 腳本化物件 Scriptable Object 將腳本變成物件存放在 Project 方便管理
    [CreateAssetMenu(menuName ="Kevin/Weapon")]
    public class DataWeapon : ScriptableObject
    {
        [Header("武器預製物")]
        public GameObject weaponPrefab;
        [Header("武器等級"), Range(1, 10)]
        public int weaponLv = 1;
        // 陣列 : 資料類型[] - 存放多筆相同資料類型
        [Header("武器素質:每個等級")]
        public WeponValue[] weponValues;
    }

    /// <summary>
    /// 武器數值
    /// </summary>
    /// 針對類別 Class 的序列化
    [System.Serializable]
    public class WeponValue
    {
        [Header("武器生成點")]
        public Vector3[] weaponPoint;
        [Header("武器生成間隔"), Range(0, 5)]
        public float weaponInterva;
        [Header("武器速度"), Range(0, 5000)]
        public float weaponSpeed;
        [Header("武器攻擊"), Range(0, 50000)]
        public float weaponAttack;
    }
}

