using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLevelMusicControl : MonoBehaviour
{
    //      Audio Variables     //
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(music1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
