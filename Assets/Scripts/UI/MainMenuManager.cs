using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    public GameObject credBtn, credMenu, mainMenu, helpMenu, optMenu;
    public TextMeshProUGUI topText;

    bool IsBig;


    void Start()
    {
        topText.text = "HIDDEN  KUNAI";
        IsBig = false;
    }

    public void OptBig()
    {
        IsBig = true;
        topText.text = "OPTIONS";
        credMenu.SetActive(false);
        helpMenu.SetActive(false);
        LeanTween.scale(mainMenu, new Vector2(0f,0f),0.1f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.alpha(optMenu.GetComponent<RectTransform>(), 1f, 0.15f);
        LeanTween.scale(optMenu, new Vector2(3.09f,2.07f),0.3f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocal(optMenu, new Vector2(0f, -90f),0.3f).setEase(LeanTweenType.easeOutQuint);
    }

    public void HelpBig()
    {
        IsBig = true;
        topText.text = "HELP";
        credMenu.SetActive(false);
        optMenu.SetActive(false);
        LeanTween.scale(mainMenu, new Vector2(0f,0f),0.1f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.alpha(helpMenu.GetComponent<RectTransform>(), 1f, 0.15f);
        LeanTween.scale(helpMenu, new Vector2(3.09f,2.07f),0.3f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocal(helpMenu, new Vector2(0f, -90f),0.3f).setEase(LeanTweenType.easeOutQuint);
    }
  
    public void CredBig()
    {
        IsBig = true;
        topText.text = "CREDITS";
        optMenu.SetActive(false);
        helpMenu.SetActive(false);
        LeanTween.scale(mainMenu, new Vector2(0f,0f),0.1f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.alpha(credMenu.GetComponent<RectTransform>(), 1f, 0.15f);
        LeanTween.scale(credMenu, new Vector2(3.09f,2.07f),0.3f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocal(credMenu, new Vector2(0f, -90f),0.3f).setEase(LeanTweenType.easeOutQuint);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") && IsBig == true)
        {
            IsBig = false;
            topText.text = "HIDDEN  KUNAI";
            optMenu.SetActive(true);
            helpMenu.SetActive(true);
            credMenu.SetActive(true);

            LeanTween.alpha(optMenu.GetComponent<RectTransform>(), 0.35f, 0.3f);
            LeanTween.scale(optMenu, new Vector2(1f,1f),0.3f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveLocal(optMenu, new Vector2(-507f, -300f),0.3f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.scale(mainMenu, new Vector2(1f,1f),0.3f).setEase(LeanTweenType.easeOutQuint);

            LeanTween.alpha(helpMenu.GetComponent<RectTransform>(), 0.35f, 0.3f);
            LeanTween.scale(helpMenu, new Vector2(1f,1f),0.3f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveLocal(helpMenu, new Vector2(0f, -300f),0.3f).setEase(LeanTweenType.easeOutQuint);
            //LeanTween.scale(mainMenu, new Vector2(1f,1f),0.3f).setEase(LeanTweenType.easeOutQuint);

            LeanTween.alpha(credMenu.GetComponent<RectTransform>(), 0.35f, 0.3f);
            LeanTween.scale(credMenu, new Vector2(1f,1f),0.3f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveLocal(credMenu, new Vector2(507f, -300f),0.3f).setEase(LeanTweenType.easeOutQuint);
            //LeanTween.scale(mainMenu, new Vector2(1f,1f),0.3f).setEase(LeanTweenType.easeOutQuint);
        }
    }


    
}
