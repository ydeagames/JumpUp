using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// プレイヤークラス
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    // ジャンプ力
    public float jumpPower = 1;
    // 移動力
    public float movePower = 0.5f;
    // 床当たり判定
    public Collider2D footCollider;
    // 床あたり
    public LayerMask footLayerMask;
    // 死ぬレイヤー
    public LayerMask deathLayer;

    private float move;
    private bool jump;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Get();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.isDying)
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (deathLayer.value == (deathLayer.value | (1 << other.gameObject.layer)))
        {
            if (!manager.isDying)
            {
                rigid.drag = 100;
                manager.Dying();
            }
        }
    }
}
