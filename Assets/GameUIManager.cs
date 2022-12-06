using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameUIManager : MonoBehaviour
{
    public GameObject fadePanel, winStateUi, winTitle, winPanel, replayBtn, menuBtn, quitBtn;
    
    void Start()
    {
        
        LeanTween.alpha(fadePanel.GetComponent<RectTransform>(), 0f, 1f).setDelay(0.2f);
        //winStateUi.SetActive(false);
    }

    //When you win the game
    //winStateUi.SetActive(true)
    //Time.timeScale = 0f
    //EventSystem.current.SetSelectedGameObject(null);
    //EventSystem.current.SetSelectedGameObject(replayBtn);

    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() => Application.Quit();
}
