using UnityEngine;
public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            Destroy(other.gameObject);
        }
    }
}
