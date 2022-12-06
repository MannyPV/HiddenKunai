using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] string whiteHex;
    [SerializeField] string blackHex;

    // Audio Clips for Main Menu
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip buttonClickSFX;

    public GameObject credBtn, credMenu, mainMenu, helpMenu, optMenu, quitMenu, quitopt, quitBtn, playBtn, playTran, quitNoBtn, titleHolder, resetHolder,
                      credtextHolder;

    public TextMeshProUGUI topText;

    public ScoreManager scoreManager;

    bool ButtonClicked;
    public float MenuClosed;

    void Start()
    {
        topText.text = "HIDDEN  KUNAI";
        topText.fontSize = 115;
        ButtonClicked = false;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playBtn);

        // Play menu music
        AudioManager.Instance.PlayMusicWithFade(music1, 3);
    }

    public void PlayClicked()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX, 2);

        ButtonClicked = true;
        optMenu.SetActive(false);
        helpMenu.SetActive(false);
        credMenu.SetActive(false);
        quitMenu.SetActive(false);
        topText.text = ("<color=" + blackHex + ">HIDDEN  KUNAI</color>");
        LeanTween.alpha(playTran.GetComponent<RectTransform>(), 1f, 0.4f);
        LeanTween.moveLocal(playTran, new Vector2(0f, 0f),0.2f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.scale(playTran, new Vector2(1.3f,2.73f),0.4f).setEase(LeanTweenType.easeOutQuint).setOnComplete(MoveTopText);
        LeanTween.scale(mainMenu, new Vector2(0f,0f),MenuClosed).setEase(LeanTweenType.easeOutQuint);

        scoreManager.score += scoreManager.addScore;
    }

    void MoveTopText()
    {
        LeanTween.moveLocal(resetHolder, new Vector2(0f, -600f), 0.75f).setDelay(0.25f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocal(titleHolder, new Vector2(0f, 800f), 0.75f).setDelay(0.25f).setEase(LeanTweenType.easeOutQuint).setOnComplete(NextLevel);
    }

    void NextLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OptBig()
    {
        EventSystem.current.SetSelectedGameObject(null);

        AudioManager.Instance.PlaySFX(buttonClickSFX, 2);

        ButtonClicked = true;
        topText.text = "OPTIONS";
        topText.fontSize = 120;
        playTran.SetActive(false);
        credMenu.SetActive(false);
        helpMenu.SetActive(false);
        quitMenu.SetActive(false);
        LeanTween.scale(mainMenu, new Vector2(0f,0f),MenuClosed).setEase(LeanTweenType.easeOutQuint);
        LeanTween.alpha(optMenu.GetComponent<RectTransform>(), 1f, 0.15f);
        LeanTween.scale(optMenu, new Vector2(3.09f,2.07f),0.3f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocal(optMenu, new Vector2(0f, -90f),0.3f).setEase(LeanTweenType.easeOutQuint);
    }

    void OptOpened()
    {

    }

    public void HelpBig()
    {
        EventSystem.current.SetSelectedGameObject(null);

        AudioManager.Instance.PlaySFX(buttonClickSFX, 2);

        ButtonClicked = true;
        topText.text = "HELP";
        topText.fontSize = 120;
        playTran.SetActive(false);
        credMenu.SetActive(false);
        optMenu.SetActive(false);
        quitMenu.SetActive(false);
        LeanTween.scale(mainMenu, new Vector2(0f,0f),MenuClosed).setEase(LeanTweenType.easeOutQuint);
        LeanTween.alpha(helpMenu.GetComponent<RectTransform>(), 1f, 0.15f);
        LeanTween.scale(helpMenu, new Vector2(3.09f,2.07f),0.3f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocal(helpMenu, new Vector2(0f, -90f),0.3f).setEase(LeanTweenType.easeOutQuint);
    }

    void HelpOpened()
    {

    }

    public void CredBig()
    {
        EventSystem.current.SetSelectedGameObject(null);

        AudioManager.Instance.PlaySFX(buttonClickSFX, 2);

        ButtonClicked = true;
        topText.text = "Credits";
        topText.fontSize = 120;
        playTran.SetActive(false);
        optMenu.SetActive(false);
        helpMenu.SetActive(false);
        quitMenu.SetActive(false);
        LeanTween.scale(mainMenu, new Vector2(0f,0f),MenuClosed).setEase(LeanTweenType.easeOutQuint);
        LeanTween.alpha(credMenu.GetComponent<RectTransform>(), 1f, 0.15f);
        LeanTween.scale(credMenu, new Vector2(3.09f,2.07f),0.3f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocal(credMenu, new Vector2(0f, -90f),0.3f).setEase(LeanTweenType.easeOutQuint).setOnComplete(CredOpened);
    }

    void CredOpened()
    {
        LeanTween.scale(credtextHolder, new Vector2(1f, 1f), 0.2f).setEase(LeanTweenType.easeOutQuint);
    }

    public void QuitClicked()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitNoBtn);

        AudioManager.Instance.PlaySFX(buttonClickSFX, 2);

        ButtonClicked = true;
        topText.text = "Quit?";
        topText.fontSize = 120;
        playTran.SetActive(false);
        optMenu.SetActive(false);
        helpMenu.SetActive(false);
        credMenu.SetActive(false);
        quitopt.SetActive(true);
       
        LeanTween.scale(mainMenu, new Vector2(0f,0f),MenuClosed).setEase(LeanTweenType.easeOutQuint);
        LeanTween.alpha(quitMenu.GetComponent<RectTransform>(), 0.3f, 0.15f);
        LeanTween.scale(quitMenu, new Vector2(1.8f,1.7f),0.3f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocal(quitMenu, new Vector2(-860f,-392f),0.01f).setEase(LeanTweenType.easeOutQuint);
    }

    public void QuitYes() => Application.Quit();

    public void QuitNo()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitBtn);

        AudioManager.Instance.PlaySFX(buttonClickSFX, 2);

        ButtonClicked = false;
        topText.text = "HIDDEN  KUNAI";
        topText.fontSize = 115;
            
        playTran.SetActive(true);
        optMenu.SetActive(true);
        helpMenu.SetActive(true);
        credMenu.SetActive(true);
        quitMenu.SetActive(true);
        quitopt.SetActive(false);

        LeanTween.scale(mainMenu, new Vector2(1f,1f),0.25f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.alpha(quitMenu.GetComponent<RectTransform>(), 0f, 0.3f);
        LeanTween.scale(quitMenu, new Vector2(1f,1f),0.2f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocal(quitMenu, new Vector2(-871f, -466f),0.2f).setEase(LeanTweenType.easeOutQuint);
    }
    

    void Update()
    {
        if (Input.GetKeyDown("escape") && ButtonClicked == true)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(playBtn);

            AudioManager.Instance.PlaySFX(buttonClickSFX, 2);

            ButtonClicked = false;
            topText.text = "HIDDEN  KUNAI";
            topText.fontSize = 115;
            
            playTran.SetActive(true);
            optMenu.SetActive(true);
            helpMenu.SetActive(true);
            credMenu.SetActive(true);
            quitMenu.SetActive(true);
            quitopt.SetActive(false);

            LeanTween.scale(mainMenu, new Vector2(1f,1f),0.2f).setEase(LeanTweenType.easeOutQuint);

            LeanTween.alpha(playTran.GetComponent<RectTransform>(), 0.35f, 0.3f);
            LeanTween.moveLocal(playTran, new Vector2(0f, 120f),0.2f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.scale(playTran, new Vector2(1f,1f),0.5f).setEase(LeanTweenType.easeOutQuint);

            LeanTween.alpha(optMenu.GetComponent<RectTransform>(), 0.35f, 0.3f);
            LeanTween.scale(optMenu, new Vector2(1f,1f),0.2f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveLocal(optMenu, new Vector2(-510f, -250f),0.2f).setEase(LeanTweenType.easeOutQuint);

            LeanTween.alpha(helpMenu.GetComponent<RectTransform>(), 0.35f, 0.3f);
            LeanTween.scale(helpMenu, new Vector2(1f,1f),0.2f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveLocal(helpMenu, new Vector2(0f, -250f),0.2f).setEase(LeanTweenType.easeOutQuint);

            LeanTween.alpha(credMenu.GetComponent<RectTransform>(), 0.35f, 0.3f);
            LeanTween.scale(credMenu, new Vector2(1f,1f),0.2f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveLocal(credMenu, new Vector2(510f, -250f),0.2f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.scale(credtextHolder, new Vector2(1f, 0f), 0.2f).setEase(LeanTweenType.easeOutQuint);

            LeanTween.alpha(quitMenu.GetComponent<RectTransform>(), 0f, 0.3f);
            LeanTween.scale(quitMenu, new Vector2(1f,1f),0.2f).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveLocal(quitMenu, new Vector2(-871f, -392f),0.2f).setEase(LeanTweenType.easeOutQuint);
        }

    }


    
}
