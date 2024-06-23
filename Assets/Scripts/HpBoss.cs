using System;
using UnityEngine;


namespace Kevin
{
    /// <summary>
    /// 血量系統 ：　敵人BOSS
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class HpBoss : HpEnemy
    {
        public static HpBoss instance;
        public event EventHandler onSecondState;
        public event EventHandler onDead;


        [SerializeField, Header("血量剩餘多少進入二階段"), Range(0, 1)]
        private float secondState = 0.5f;

        private float hpMax;
        private float secondHp => hpMax * secondState;

        /// <summary>
        /// 是否進入二階段
        /// </summary>
        private bool inSecondState;

        protected override void Awake()
        {
            instance = this;
            base.Awake();
            hpMax = hp;
        }

        public override void Damage(float damage)
        {
            base.Damage(damage);
            SecondState();
        }

        /// <summary>
        /// 二階段
        /// </summary>
        protected void SecondState()
        {
            if (inSecondState) return;
            if (hp <= secondHp)
            {
                inSecondState = true;   
                onSecondState?.Invoke(this, null);
                print("<color=#df3>進入二階段</color>");
            }
        }

        protected override void Dead()
        {
            base.Dead();
            onDead.Invoke(this, null);
            GameManager.instance.ShowWinCanvas();
        }
    }

}
