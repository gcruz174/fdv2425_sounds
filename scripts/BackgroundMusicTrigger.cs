using UnityEngine;

namespace Audio
{
    public class BackgroundMusicTrigger : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip backgroundMusic;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                audioSource.clip = backgroundMusic;
                audioSource.Play();
            }
        }
    }
}
