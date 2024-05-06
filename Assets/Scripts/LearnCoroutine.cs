using UnityEngine;
using System.Collections;

namespace Kevin
{
    /// <summary>
    /// 學習協同程序
    /// </summary>
    public class LearnCoroutine : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(Test());
        }

        private IEnumerator Test()
        {
            print("<color=f69f>第一行程式</color>");
            yield return new WaitForSeconds(1);
            print("<color=f69f>第二行程式,一秒後執行</color>");
            yield return new WaitForSeconds(2.5f); 
            print("<color=f69f>第三行程式,一秒後執行</color>");
        }
    }

}
