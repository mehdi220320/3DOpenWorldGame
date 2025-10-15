using UnityEngine;

public class BearAttack : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        HealthManager healthManager = collision.collider.GetComponent<HealthManager>();
        
        if (healthManager != null)
        {
            healthManager.isAttack = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        HealthManager healthManager = collision.collider.GetComponent<HealthManager>();
        
        if (healthManager != null)
        {
            healthManager.isAttack = false;
            Debug.Log("Bear stopped attacking player!");
        }
    }
}