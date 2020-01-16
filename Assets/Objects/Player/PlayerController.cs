using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤークラス
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    // ジャンプ力
    public float jumpPower = 1;
    // 移動力
    public float movePower = 1;
    // 床当たり判定
    public Collider2D footCollider;
    // 床あたり
    public LayerMask footLayerMask;

    private float move;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        move = Input.GetAxis("Horizontal");

        // ジャンプ
        if (footCollider.IsTouchingLayers(footLayerMask.value))
        {
            if (Input.GetButtonDown("Jump"))
                jump = true;
        }
    }

    void FixedUpdate()
    {
        // 移動
        rigid.AddForce(new Vector2(move * movePower, 0));
        // var vel = rigid.velocity;
        // vel.x = Input.GetAxis("Horizontal") * movePower;
        // rigid.velocity = vel;

        if (footCollider.IsTouchingLayers(footLayerMask.value))
        {
            // ジャンプ
            if (jump)
            {
                rigid.AddForce(Vector2.up * jumpPower);
                jump = false;
            }
        }
    }
}
