using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI dashes;
    public TextMeshProUGUI jump;

    // Update is called once per frame
    void Update()
    {
        dashes.text = player.numOfDashes.ToString();
        jump.text = player.canJump.ToString();
    }
}
