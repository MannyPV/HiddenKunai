using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerPosition : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    public Animator animator;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            StartCoroutine(Respawn());
        }
        else if(other.CompareTag("Checkpoint"))
        {
            GameManager.Checkpoint = transform.position;
        }
    }

    IEnumerator Respawn()
    {
        Destroy(gameObject, 0.2f);
        // animator.SetTrigger("Standing React Death Backward");
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
