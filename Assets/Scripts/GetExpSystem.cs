using UnityEngine;

namespace Kevin
{
    public class GetExpSystem : MonoBehaviour
    {
        private string expName = "經驗值";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // print($"<color=#63f>碰到的物件{collision.name}</color>");
            // 如果 碰到物件名稱包含經驗值就開啟經驗值物件元件
            if (collision.name.Contains(expName))
                collision.GetComponent<ExpObject>().enabled = true;
        }
    }
}

