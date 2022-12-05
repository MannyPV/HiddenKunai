using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Enemy : MonoBehaviour
{
    PlayerController playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (playerScript.isDashing == true)
            {
                playerScript.numOfDashes++;
                Destroy(gameObject);
            }

            else
            {
                playerScript.alive = false;
            }
        }
        if (other.CompareTag("Kunai"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().numOfDashes++;
            Destroy(gameObject);
        }
    }
}
