using UnityEngine;

public class BallAttack : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifetime = 5f; // Destroy after 5 seconds if it doesn't hit anything
    
    void Start()
    {
        // Destroy the projectile after a certain time to avoid clutter
        Destroy(gameObject, lifetime);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Check if we hit the bear
        BearHealth bearHealth = collision.gameObject.GetComponent<BearHealth>();
        
        if (bearHealth != null)
        {
            bearHealth.TakeDamage(damage);
        }
        
        // Destroy the projectile on impact
        Destroy(gameObject);
    }
}
