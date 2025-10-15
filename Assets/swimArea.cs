using UnityEngine;

public class swimArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        collider.GetComponent<playerScript>().IsSwimming = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        collider.GetComponent<playerScript>().IsSwimming = false;
    }
}
