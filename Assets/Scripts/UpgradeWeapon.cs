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

        public void Upgrade(float increase)
        {
            
        }
    }
}

