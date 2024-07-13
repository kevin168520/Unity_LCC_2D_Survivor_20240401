using UnityEngine;

namespace Kevin
{
    /// <summary>
    /// 音效管理器
    /// </summary>
    /// 要求元件(類型(音效來源) - 在首次添加此元件時會執行)
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance
        {
            get
            {
                // 如果 單例模式 為 空值 就尋找場景上的 SoundManager
                if (_instance == null) return FindObjectOfType<SoundManager>();
                // 否則傳回 單例模式
                return _instance;
            }
        }
        // 單例模式欄位(私人)
        private static SoundManager _instance;
        [Header("音效")]
        [SerializeField] private AudioClip soundKnife;
        [SerializeField] private AudioClip soundBnuce;
        [SerializeField] private AudioClip soundEnemyHit;
        [SerializeField] private AudioClip soundEnemyDead;
        [SerializeField] private AudioClip soundPlayerHit;
        [SerializeField] private AudioClip soundPlayerDead;
        [SerializeField] private AudioClip soundWin;
        [SerializeField] private AudioClip soundLose;


        private AudioSource aud;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="soundType">音效類型</param>
        public void PlaySound(SoundType soundType, float min = 0.7f, float max = 1.2f)
        {
            float volume = Random.Range(min, max);
            aud.PlayOneShot(GetSound(soundType),volume);
        }

        private AudioClip GetSound(SoundType soundType)
        {
            return soundType switch
            {
                SoundType.Knife => soundKnife,
                SoundType.Bounce => soundBnuce,
                SoundType.EnemyHit => soundEnemyHit,
                SoundType.EnemyDead => soundEnemyDead,
                SoundType.PlayerHit => soundPlayerHit,
                SoundType.PlayerDead => soundPlayerDead,
                SoundType.Win => soundWin,
                SoundType.Lose => soundLose,
                _ => null
            };
        }

    }

    public enum SoundType
    {
        None, Knife, Bounce, EnemyHit, EnemyDead, PlayerHit, PlayerDead, Win, Lose
    }
}

