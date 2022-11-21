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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().numOfDashes++;
            Destroy(gameObject);
        }
        Debug.Log("Projetile Collided");
        //Destroy(gameObject);
    }
}
