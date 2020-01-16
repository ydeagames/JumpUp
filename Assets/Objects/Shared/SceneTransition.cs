using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Fade fade;

    private void Start()
    {
    }

    public void LoadScene(string scene)
    {
        MyFade.Get().Fadeout(scene);
    }

    public void Reload()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
}
