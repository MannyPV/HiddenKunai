using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Enemy : MonoBehaviour
{
    PlayerController playerScript;
    PlayerPosition respawn;

    //  Audio Variables //
    [SerializeField] private AudioClip destroyedSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (playerScript.isDashing == true)
            {
                // play destroyed SFX
                AudioManager.Instance.PlaySFX(destroyedSFX);

                playerScript.numOfDashes++;
                Destroy(gameObject);
            }

            else
            {
                respawn = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPosition>();
                respawn.StartCoroutine("Respawn");
            }
        }

        if (other.CompareTag("Kunai"))
        {
            // play destroyed SFX
            AudioManager.Instance.PlaySFX(destroyedSFX);

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().numOfDashes++;
            Destroy(gameObject);
        }
    }
}
