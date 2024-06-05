using Cinemachine;
using System.Collections;
using UnityEngine;


namespace Kevin
{
    /// <summary>
    /// 晃動攝影機
    /// </summary>
    public class ShakeCamera : MonoBehaviour
    {
        public static ShakeCamera instance;

        private CinemachineVirtualCamera virtualCamera;
        private CinemachineBasicMultiChannelPerlin perlin;

        private void Awake()
        {
            instance = this;
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        /// <summary>
        /// 晃動攝影機
        /// </summary>
        /// <param name="intensite"></param>
        /// <param name="time"></param>
        public void Shake(float intensity, float time)
        {
            perlin.m_AmplitudeGain = intensity;
            StartCoroutine(StartShake(time));
        }

        private IEnumerator StartShake(float time)
        {
            yield return new WaitForSeconds(time);
            perlin.m_AmplitudeGain = 0;
        }
    }
}

