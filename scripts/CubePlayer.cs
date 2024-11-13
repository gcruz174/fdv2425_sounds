using UnityEngine;

public class CubePlayer : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private LayerMask sphereLayerMask;
    
    private AudioSource _audioSource;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var velocity = new Vector3(horizontal, 0, vertical) * movementSpeed;
        _rigidBody.linearVelocity = velocity;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        // Check layer mask
        if ((sphereLayerMask.value & 1 << other.gameObject.layer) == 0) return;
        _audioSource.volume = other.relativeVelocity.magnitude / 5;
        _audioSource.Play();
    }
}
