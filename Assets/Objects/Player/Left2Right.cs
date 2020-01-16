using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 左端と右端をつなぐ
public class Left2Right : MonoBehaviour
{
    // ワールドの端を表す境界
    public BoxCollider2D bounding;

    // Update is called once per frame
    void Update()
    {
        // 左へ来たら右へ、右へ来たら左へ
        var pos = transform.position;
        if (pos.x < bounding.bounds.min.x)
            pos.x = bounding.bounds.max.x;
        else if (pos.x > bounding.bounds.max.x)
            pos.x = bounding.bounds.min.x;
        transform.position = pos;
    }
}
