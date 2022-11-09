using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject credBtn, credMenu, mainMenu, helpMenu, optMenu, playTran;
    public TextMeshProUGUI topText;

    bool IsBig;


    void Start()
    {
        topText.text = "HIDDEN  KUNAI";
        topText.fontSize = 100;
        IsBig = false;
    }

    public void PlayClicked()
    {
        optMenu.SetActive(false);
        helpMenu.SetActive(false);
        credMenu.SetActive(false);
        LeanTween.alpha(playTran.GetComponent<RectTransform>(), 1f, 0.5f);
        LeanTween.moveLocal(playTran, new Vector2(0f, 0f),0.2f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.scale(playTran, new Vector2(1.3f,2.73f),0.5f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.scale(mainMenu, new Vector2(0f,0f),0.1f).setEase(LeanTweenType.easeOutQuint);
    }

    public void OptBig()
    {
        IsBig = true;
        topText.text = "OPTIONS";
        topText.fontSize = 110;
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
        topText.fontSize = 110;
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
        topText.fontSize = 110;
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
            topText.fontSize = 100;
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

    public void QuitGame() => Application.Quit();





    
}
