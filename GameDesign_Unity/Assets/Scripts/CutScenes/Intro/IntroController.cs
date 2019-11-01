using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : LevelController
{
    public Text dialogText;
    public string nextScene;

    private string toWrite = "Wenn Sie weiterhin so wenig Motivation an den Tag legen sind Ihre Tage bei ACME Consulting gezählt! \n \n" +
        "                                       \n"+
        "Nach einem langem Arbeitstag kommst Du nach hause zu deiner Frau...";

    void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < toWrite.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            dialogText.text += toWrite[i];
        }
        yield return new WaitForSeconds(2.0f);
        FinishLevel();
    }
}
