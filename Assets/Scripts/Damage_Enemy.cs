using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().numOfDashes++;
            Destroy(gameObject);
        }
        if (other.CompareTag("Kunai"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().numOfDashes++;
            Destroy(gameObject);
        }
    }
}
