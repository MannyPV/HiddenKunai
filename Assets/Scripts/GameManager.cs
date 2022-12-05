using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Vector3 Checkpoint= new Vector3(-14.19f,2.29f,0.4f);
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        { 
            Destroy(Instance);
        }

        GameObject.FindGameObjectWithTag("Player").transform.position = Checkpoint;
    }
}
