using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreBtn;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI addText;

    public int score;
    public int addScore;

    void Update()
    {
        scoreText.text = "" + score;
        addText.text = "Add " + addScore;
    }

    public void AddToScore() => score += addScore;

}
