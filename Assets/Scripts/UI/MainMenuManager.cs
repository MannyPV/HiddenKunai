using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public GameObject button;

    bool IsBig;


    void Start()
    {
        IsBig = false;
    }
  
    public void ButtonBig()
    {
        IsBig = true;
        LeanTween.scale(button, new Vector2(2f,2f),1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveLocal(button, new Vector2(0f, 0f),1f).setEase(LeanTweenType.easeOutBounce);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") && IsBig == true)
        {
            LeanTween.scale(button, new Vector2(1f,1f),1f).setEase(LeanTweenType.easeOutBounce);
            LeanTween.moveLocal(button, new Vector2(450f, -200f),1f).setEase(LeanTweenType.easeOutBounce);
            
        }
    }


    
}
