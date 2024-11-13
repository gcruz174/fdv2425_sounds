using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action<int> OnHealthChanged;
    
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jumpForce = 25.0f;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private bool _jumpPressed;
    private int _health = 100;
    
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        _animator.SetFloat(Horizontal, horizontal);
        _animator.SetBool(IsWalking, horizontal != 0);
        _spriteRenderer.flipX = horizontal > 0 && Mathf.Abs(horizontal) > 0.1f;
        if (Input.GetKeyDown(KeyCode.Space)) _jumpPressed = true;
    }
    
    private void FixedUpdate()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var velocity = new Vector2(x * speed, _rb.linearVelocity.y);
        _rb.linearVelocity = velocity;
        
        CheckGrounded();

        if (!_jumpPressed || !_isGrounded) return;
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _audioSource.PlayOneShot(jumpSound);
        _isGrounded = false;
        _jumpPressed = false;
    }
    
    private void CheckGrounded()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f);
        var newIsGrounded = hit.collider != null && hit.collider.CompareTag("Ground");
        if (!_isGrounded && newIsGrounded) _audioSource.PlayOneShot(landSound);
        _isGrounded = newIsGrounded;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform")) 
            transform.SetParent(other.transform);
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            transform.SetParent(null);
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health);
    }
}
