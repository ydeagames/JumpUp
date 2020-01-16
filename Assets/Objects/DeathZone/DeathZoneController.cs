using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneController : MonoBehaviour
{
    public float speed = 1f;
    public float dyingSpeed = .1f;
    private GameManager manager;

    private void Start()
    {
        manager = GameManager.Get();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * (Time.deltaTime * (manager.isDying ? dyingSpeed : speed));
    }
}
