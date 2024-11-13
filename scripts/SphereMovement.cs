using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    public float speed = 10f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            _rigidbody.AddForce(Vector3.forward * speed);
        }
    }
}
