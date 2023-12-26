using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTheText();
    }

    public void UpdateTheText()
    {
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerPrefs.GetInt("Score", 0).ToString();
    }

    public void BlueTeamScores()//Call this function when blue team scores
    {
        int x = PlayerPrefs.GetInt("BlueScore", 0);//Calling blueScore and assign to x
        x++;//increase the x
        PlayerPrefs.SetInt("BlueScore", x);//update the score
        scoreText.text = "Blue Team " + PlayerPrefs.GetInt("BlueScore", 0).ToString();        
    }
}
