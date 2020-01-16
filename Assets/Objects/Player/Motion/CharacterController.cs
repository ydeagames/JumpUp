using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Animator animator;         // Animator管理用
    public float stopSpeed = 1e-4f;
    private float rotation = 0;
    public float lerp = .1f;
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 移動入力
        float move = Input.GetAxis("Horizontal");

        // characterの移動判定用
        int key = 0;
        if (move > stopSpeed)
            key = 1;
        else if (move < -stopSpeed)
            key = -1;
        else
            key = 0;

        // アニメーション管理
        // bool型のRunをtrueに(走るアニメーションの開始)
        this.animator.SetBool("Run", Mathf.Abs(move) > stopSpeed);
        // ジャンプアニメーションを起動
        this.animator.SetBool("Jump", rigid.velocity.y > stopSpeed);

        //if (key != 0)
        {
            /*
            // プレイヤーの速度
            float speedx = Mathf.Abs(this.rigid2D.velocity.x);

            // スピード制限
            if (speedx < this.maxWalkSpeed)
            {
                this.rigid2D.AddForce(transform.right * key * this.walkForce);
            }

            // transformを取得
            Transform myTransform = this.transform;
            */

            // ワールド座標基準で、現在の回転量へ加算する
            //characterの向きを変える
            var nextRotation = 90.0f * (2 - key);
            rotation = Mathf.LerpAngle(rotation, nextRotation, lerp);
            this.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }
}
