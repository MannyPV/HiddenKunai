using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            obj1.transform.position = obj2.transform.position;
        }
    }
}
