using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 血量系統:玩家
    /// </summary>
    public class HpPlayer : HpSystem
    {
        [SerializeField, Header("玩家資料")]
        private DataPlayer dataPlayer;

        private Animator ani;
        private string parDead = "觸發死亡";

        private void Awake()
        {
            hp = dataPlayer.hp;
            ani = GetComponent<Animator>();
        }

        protected override void Dead()
        {
            base.Dead();
            ani.SetTrigger(parDead);
        }
    }

}
