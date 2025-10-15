using UnityEngine;
using TMPro;
public class HealthManager : MonoBehaviour
{
    [SerializeField] private float health = 200f;        
    [SerializeField] private float damageRate = 0.5f; 
    [SerializeField] private float Takeheal = 60f;  
    [SerializeField] private TMP_Text healthText; 
    internal bool isAttack=false;
    internal bool isHealth = false;

    private float HealthIncremente = 0f;

    void Start()
    {
        healthText.text = "Health: 200" ; 
        
    }

    void Update()
    {
        if(isAttack){
            TakeDamage();
        }
        if(isHealth){
            TakeHeal();
            isHealth=false;
        }
    }



   public void TakeDamage()
{
    health -= damageRate;
    health = Mathf.Clamp(health, 0f, 200f); 
    UpdateHealthText();
}

public void TakeHeal()
{
    health += Takeheal;
    health = Mathf.Clamp(health, 0f, 200f); 
    UpdateHealthText();
}

    private void UpdateHealthText()
    {
        if (healthText != null)
            healthText.text = "Health: " + health.ToString("F0"); 
    }

}
