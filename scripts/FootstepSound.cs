using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    [SerializeField] 
    private float distancePerStep = 1f;
    [SerializeField]
    private AudioClip[] footstepSounds;
    
    private AudioSource _source;
    private Vector3 _lastPosition;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _lastPosition = transform.position;
    }

    private void Update()
    {
        var distance = Vector3.Distance(_lastPosition, transform.position);
        if (distance < distancePerStep) return;
        
        _source.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
        _source.Play();
        _lastPosition = transform.position;
    }
}
