using UnityEngine;

namespace Kevin
{
    /// <summary>
    ///  武器系統
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField, Header("武器資料")]
        private DataWeapon dataWeapon;

        /// <summary>
        /// 取得當前等級武器數值
        /// </summary>
        private WeponValue weaponCurrentLv => dataWeapon.weponValues[dataWeapon.weaponLv - 1];

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.5f, 0.2f, 0.3f);

            // 迴圈 : 重複執行程式 for
            // for (初始值 ; 條件 ; 迴圈結束執行) { 迴圈內容 }
            for (int i = 0; i < weaponCurrentLv.weaponPoint.Length; i++)
            {
                Gizmos.DrawSphere(transform.position +
                transform.TransformDirection(weaponCurrentLv.weaponPoint[i]), 0.2f);
            }

           
        }

        private void Awake()
        {
            //SpawnWeapon(); //呼叫一次只會生成一個武器
            // 重複呼叫方法(方法名稱, 延後時間，重複頻率)
            InvokeRepeating("SpawnWeapon", 0, weaponCurrentLv.weaponInterva);
        }

        /// <summary>
        /// 生成武器
        /// </summary>
        private void SpawnWeapon()
        {
            for (int i = 0; i < weaponCurrentLv.weaponPoint.Length; i++)
            {
                // 角度 = 武器的X如果大於 0 就設定 0 角度，否則 180 角度
                float angle = weaponCurrentLv.weaponPoint[i].x > 0 ? 0 : 180;

                // 物件 生成(物件, 座標, 角度)
                // 零度角 Quaternion 代表角度 0,0,0
                GameObject tempWeapon =
                    Instantiate(
                    dataWeapon.weaponPrefab, 
                    transform.position + 
                    transform.TransformDirection(weaponCurrentLv.weaponPoint[i]), 
                    Quaternion.Euler(0,0,angle));

                // 獲得生成暫存武器 剛體 往前產生推力
                // transform.right 指的是此物件區域座標 X 紅色的軸向
                // x 右(紅), Y上(綠), z 前(藍)
                tempWeapon.GetComponent<Rigidbody2D>().AddForce(weaponCurrentLv.weaponSpeed * tempWeapon.transform.right);
            }
            
        }
    }

}
