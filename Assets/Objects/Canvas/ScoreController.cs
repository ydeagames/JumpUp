using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    //スコア
    float score;
    //ハイスコア
    float hiScore;
    //スコア用のテキスト
    private Text scoreText;
    //距離計算用
    int distance;

    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Get();
        score = 0.0f;
        hiScore = 0.0f;
        scoreText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.isDying)
        {
            score = manager.player.transform.position.y;

            if(hiScore < score)
            {
                hiScore = score;
            }

            if (scoreText != null)
            {
                scoreText.text = hiScore.ToString("0.0m");
            }
        }
    }
}
