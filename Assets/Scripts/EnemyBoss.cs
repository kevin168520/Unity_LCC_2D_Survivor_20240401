﻿using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Kevin
{
    /// <summary>
    /// 敵人系統:BOSS
    /// </summary>
    public class EnemyBoss : EnemySystem
    {
        [SerializeField, Header("光球")]
        private GameObject prefabLightBall;
        [SerializeField, Header("光球生成點")]
        private Vector3[] pointLightBall;
        [SerializeField, Header("光球生成間隔"), Range(0,3)]
        private float intervalLightBall = 0.2f;
        [SerializeField, Header("二階段攻擊時間"), Range(0, 10)]
        private float secondAttackTime = 3;
        [SerializeField, Header("光球攻擊力"), Range(0, 30)]
        private float attackLightBall;
        [SerializeField, Header("二階段攻擊機率"), Range(0, 1)]
        private float secondAttackProbability = 0.7f;

        private bool isSecondState;

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.color = new Color(1, 0.6f, 0.8f, 0.4f);

            for (int i = 0; i < pointLightBall.Length; i++)
            {
                Gizmos.DrawSphere(
                    transform.position + 
                    transform.TransformDirection(pointLightBall[i]), 
                    0.2f);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            HpBoss.instance.onSecondState += SecondState;
        }

        protected override void Attack()
        {
            base.Attack();

            if(isSecondState)
               attackMode = Random.value < secondAttackProbability ? SecondAttack() : StartAttack();

        }

        private void SecondState(object sender, EventArgs e)
        {
            ShakeCamera.instance.Shake(3, 1.5f);
            isSecondState = true;
            StartCoroutine(SecondAttack());
        }

        private IEnumerator SecondAttack()
        {
            isAttacking = true;

            for (int i = 0; i < pointLightBall.Length; i++)
            {
                Vector3 point = transform.position +
                    transform.TransformDirection(pointLightBall[i]);
                Instantiate(prefabLightBall, point, Quaternion.identity);
                yield return new WaitForSeconds(intervalLightBall);
            }

            yield return new WaitForSeconds(secondAttackTime);

            isAttacking = false;
        }
    }
}

