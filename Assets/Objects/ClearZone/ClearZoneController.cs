using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZoneController : MonoBehaviour
{
    private GameManager manager;

    private void Start()
    {
        manager = GameManager.Get();
    }
}
