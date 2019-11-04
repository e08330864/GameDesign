using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextSceneController : LevelController
{
    public Text dialogText;
    [TextArea]
    public string toWrite;

    void Start()
    {
        Button btn = gameObject.AddComponent<Button>();
        btn.onClick.AddListener(() => FinishLevel());
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
