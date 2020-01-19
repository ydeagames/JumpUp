using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScoreController : MonoBehaviour
{
    private Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ScoreController.hiScore.ToString("0.0m");
    }
}
