using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollected;
    
    [SerializeField]
    private AudioClip collectedSound;
    [SerializeField]
    private int damageAmount = 10;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        OnCollected?.Invoke();
        AudioSource.PlayClipAtPoint(collectedSound, transform.position);
        collision.GetComponent<PlayerController>().TakeDamage(damageAmount);
        Destroy(gameObject);
    }
}
