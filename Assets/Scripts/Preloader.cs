﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectOfType<TheGameManager>() == null)
        {
            SceneManager.LoadScene("_preload");
        }
    }
}
