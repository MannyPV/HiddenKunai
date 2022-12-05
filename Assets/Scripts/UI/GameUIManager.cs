using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    public GameObject fadePanel;
    void Start()
    {
        LeanTween.alpha(fadePanel.GetComponent<RectTransform>(), 0f, 1.5f);




    }

    
}
