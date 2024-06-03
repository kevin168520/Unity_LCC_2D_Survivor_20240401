using UnityEngine;

namespace Kevin
{
    // C# 為單一繼承，但可以多重實作介面
    /// <summary>
    /// 武器升級
    /// </summary>
    public class UpgradeWeapon : MonoBehaviour, IUpgrade
    {
        [SerializeField, Header("武器資料")]
        private DataWeapon dataWeapon;
        [SerializeField, Header("武器系統")]
        private WeaponSystem weaponSystem;

        private void Awake()
        {
            dataWeapon.weaponLv = 1;
        }

        public void Upgrade(float increase)
        {
            print($"<color=#f63>升級武器 : {dataWeapon.name}</color>");
            dataWeapon.weaponLv++;
            weaponSystem.RestartSpawnWeapon();
        }
    }
}

