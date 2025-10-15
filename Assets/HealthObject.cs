using UnityEngine;

public class HealthObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float respawnTime = 30f; 
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private GameObject visualObject; 
    
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }
    
    void Update()
    {
        if (visualObject == null || visualObject.activeSelf)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthManager healthManager = other.GetComponent<HealthManager>();
        
        if (healthManager != null)
        {
            healthManager.isHealth = true;
            
            HideObject();
            Invoke(nameof(RespawnObject), respawnTime);
        }
    }
    
    private void HideObject()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
        
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
    
    private void RespawnObject()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
        
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }
    }
}