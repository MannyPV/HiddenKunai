using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 12f;
    public float projectileLifespan = 5f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= projectileLifespan)
        {
            Debug.Log("Projectile Expired");
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.position += projectileSpeed * Time.deltaTime * transform.forward;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("Kunai hit Enemy");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().numOfDashes++;
            Destroy(gameObject);
        }

        if (other.CompareTag("Floor"))
        {
            Debug.Log("Kunai hit Floor");
            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Debug.Log("Kunai hit Wall");
            Destroy(gameObject);
        }
    }
}
