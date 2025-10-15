using UnityEngine;
using TMPro;
using System.Collections;

public class BearHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] Animator animator;

    private float currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthDisplay();
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        UpdateHealthDisplay();
        
        // Check if bear is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = "Bear : " + currentHealth.ToString("F0");
        }
    }
    
   private void Die()
    {
        animator.SetBool("IsDead", true);
        StartCoroutine(DestroyAfterDelay(2f));

    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Destroy(gameObject);
        animator.SetBool("IsDead", false);


    }

}
