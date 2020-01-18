using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    //距離を計算するターゲットオブジェクト
    [SerializeField]
    private GameObject targetObj1;
    //スコア
    float score;
    //ハイスコア
    float hiScore;
    //スコア用のテキスト
    private Text scoreText;
    //距離計算用
    int distance;
    //ゲームマナージャー読み込み用
    private GameManager manager;
    //保存したデータの読み込み用
    float highScore;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Get();
        score = 0.0f;
        hiScore = 0.0f;
        scoreText = GetComponentInChildren<Text>();
        highScore = PlayerPrefs.GetFloat("highScore", 0);
       // Debug.Log(highScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.isDying)
        {
            score = targetObj1.transform.position.y;

            if(hiScore < score)
            {
                hiScore = score;
            }

            if (scoreText != null)
            {
                scoreText.text = hiScore.ToString("0.0m");
            }
        }
        else if(manager.isDying)
        {
            //データの保存
            if (highScore < hiScore)
            {
                PlayerPrefs.SetFloat("highScore", hiScore);
            }

            PlayerPrefs.Save();

            // 保存データの全てを削除する
           //PlayerPrefs.DeleteAll();
        }
    }
}
