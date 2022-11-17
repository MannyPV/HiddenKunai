using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] string bronzeHex;
    [SerializeField] string silverHex;
    [SerializeField] string goldHex;

    public GameObject scoreBtn, bMedal, sMedal, gMedal;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI addText;

    public int score;
    public int addScore;

    void Start()
    {
        bMedal.SetActive(false);
        sMedal.SetActive(false);
        gMedal.SetActive(false);
    } 

    void Update()
    {
        scoreText.text = "" + score;
        addText.text = "Add " + addScore;

        if (score == 500)
        {
            bMedal.SetActive(true);
        }
        else if (score == 1000)
        {
            bMedal.SetActive(false);
            sMedal.SetActive(true);
        }
        else if (score == 1500)
        {
            sMedal.SetActive(false);
            gMedal.SetActive(true);
        }

    }

    public void AddToScore() => score += addScore;

}
