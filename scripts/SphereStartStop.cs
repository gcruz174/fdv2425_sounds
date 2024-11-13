using UnityEngine;

public class SphereStartStop : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    
    public float speed = 10f;
    private bool _isMoving;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _audioSource.Play();
            _isMoving = true;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            _audioSource.Stop();
            _isMoving = false;
        }
        
        if (_isMoving)
        {
            _rigidbody.AddForce(transform.forward * speed);
        }
    }
}
