using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float throwForce = 15f;
    [SerializeField] private float spawnDistance = 2f; 
    [SerializeField] private float spawnHeight = 1.5f; 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowProjectile();
        }
    }
    
    private void ThrowProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is NULL!");
            return;
        }
        
        Vector3 spawnPosition = transform.position + transform.forward * spawnDistance + Vector3.up * spawnHeight;
        
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        
        projectile.transform.localScale = Vector3.one * 0.3f; 
        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            Debug.Log("Ball thrown with force: " + throwForce);
        }
        else
        {
            Debug.LogError("No Rigidbody on projectile!");
        }
    }
}