using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T90Controller : MonoBehaviour
{
    float posX = 0;
    bool moveCheck;
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Get();
        moveCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.player.transform.position.y < transform.position.y && manager.player.transform.position.y > transform.position.y - 10)
        {
            moveCheck = true;
        }

        if (moveCheck)
        {
            posX += 0.001f;
            transform.position = new Vector3(transform.position.x - posX, transform.position.y, transform.position.z);
        }
    }
}
