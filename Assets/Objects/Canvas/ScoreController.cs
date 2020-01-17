using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    //距離を計算するターゲットオブジェクト
    [SerializeField]
    private Transform targetObj1;
    //距離を計算するターゲットオブジェクト
    [SerializeField]
    private Transform targetObj2;
    //スコア用のテキスト
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
       scoreText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //　距離を計算
        var distance = Vector3.Distance(transform.position, targetObj2.position);

        if (scoreText != null)
        {
            scoreText.text = distance.ToString("0.00m");
        }
        else
        {
 
        }
    }
}
