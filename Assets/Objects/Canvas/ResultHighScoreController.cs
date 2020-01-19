using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultHighScoreController : MonoBehaviour
{
    private float highScore;
    Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetFloat("highScore", 0.0f);
        highScoreText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        highScoreText.text= highScore.ToString("0.0m");
    }
}
