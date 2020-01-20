using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingFloorController : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private Vector2 defaultpass;
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        defaultpass = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rigid2D.MovePosition(new Vector2(defaultpass.x + Mathf.PingPong(Time.time, 3), defaultpass.y));
    }
}
