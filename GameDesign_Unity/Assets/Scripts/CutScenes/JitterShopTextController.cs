using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JitterShopTextController : TextSceneController
{
    public GameObject drugs;

    void Update()  {
        if (finished || (!drugs.activeInHierarchy && counter >= 1))
        {
            drugs.SetActive(true);
        }
    } 
}
