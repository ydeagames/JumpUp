using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 簡易プレイヤークラス (Unityちゃんに差し替え予定)
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    // ジャンプ力
    public float jumpPower = 1;
    // 移動力
    public float movePower = 1;
    // 床当たり判定
    public BoxCollider2D footCollider;
    // 床あたり
    public LayerMask footLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        rigid.AddForce(new Vector2(Input.GetAxis("Horizontal") * movePower, 0));
        // var vel = rigid.velocity;
        // vel.x = Input.GetAxis("Horizontal") * movePower;
        // rigid.velocity = vel;

        if (footCollider.IsTouchingLayers(footLayerMask.value))
        {
            // ジャンプ
            if (Input.GetButtonDown("Jump"))
                rigid.AddForce(Vector2.up * jumpPower);
        }
    }
}
