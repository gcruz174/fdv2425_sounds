using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] 
        private PlayerController playerController;
        [SerializeField] 
        private AudioClip getHealthSound;
        [SerializeField] 
        private AudioClip getHurtSound;
        
        private Slider _slider;
        private AudioSource _audioSource;

        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void OnEnable()
        {
            playerController.OnHealthChanged += OnHealthChanged;
        }
        
        private void OnDisable()
        {
            playerController.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int newHealth)
        {
            var soundToPlay = newHealth >= _slider.value ? getHealthSound : getHurtSound;
            _audioSource.PlayOneShot(soundToPlay);
            _slider.value = newHealth;
        }
    }
}
