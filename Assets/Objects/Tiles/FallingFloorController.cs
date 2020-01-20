using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloorController : MonoBehaviour
{
    bool checkFalling = false;
    float FallingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        FallingSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkFalling)
        {
            FallingSpeed += 0.001f;
            transform.position = new Vector3(transform.position.x, transform.position.y - FallingSpeed, transform.position.z);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Physics.gravity = new Vector3(0, -3.0f, 0);
        Debug.Log(checkFalling);
    }
}
