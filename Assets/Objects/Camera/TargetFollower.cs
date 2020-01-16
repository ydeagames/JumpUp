using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カメラを追従
public class TargetFollower : MonoBehaviour
{
    // 追従ターゲット
    public Transform target;
    // 追従スピード (Lerpの％)
    public float percent = .5f;
    // どこの軸を固定するか X=0でX軸は0に固定されて動かない
    public Vector3 constant = Vector3.one;

    private Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        // 移動
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, percent);
        // 固定
        transform.position = Matrix4x4.Scale(constant) * transform.position;
    }

    public void Death()
    {
        constant = Vector3.one;
        percent = .01f;
        offset += Vector3.forward * -(Camera.main.transform.position.z + 4);
    }
}
