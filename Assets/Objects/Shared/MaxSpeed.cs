using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 最大速度を制限
public class MaxSpeed : MonoBehaviour
{
    // 最大速度
    public Vector2 maxSpeed = new Vector2(2, 2);
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 速度を制限
        var vel = rigid.velocity;
        vel.x = Mathf.Clamp(vel.x, -maxSpeed.x, maxSpeed.x);
        vel.y = Mathf.Clamp(vel.y, -maxSpeed.y, maxSpeed.y);
        rigid.velocity = vel;
    }
}
