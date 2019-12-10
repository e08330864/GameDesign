using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextSceneController : LevelController
{
    public List<Text> dialogTextList;
    private float waitSecondsBefore = 2.0f;
    private float waitSecondsAfter = 3.0f;
    private float waitSecondsPerChar = 0.02f;
    private float waitSecondsPerNewLine = 1f;
    private DateTime time;
    private int counter = 0;

    void Start()
    {
        Button btn = gameObject.AddComponent<Button>();
        btn.onClick.AddListener(() => FinishLevel());
        if (dialogTextList.Count > 1)
        {
            gameObject.transform.Find("Character").Find("Image").GetComponent<Image>().enabled = false;
        }
        foreach (Text txt in dialogTextList)
        {
            txt.enabled = false;
        }
        Text dialogText = dialogTextList[0];
        StartCoroutine(TypeText(dialogText, CallNextText));
    }

    void CallNextText()  {
        counter += 1;
        if (counter == 1)
        {
            gameObject.transform.Find("Character").Find("Image").GetComponent<Image>().enabled = true;
        }
        if (counter < dialogTextList.Count)
        {
            Text dialogText = dialogTextList[counter];
            StartCoroutine(TypeText(dialogText, CallNextText));
        }
        else
        {
            FinishLevel();
        }
    } 

    IEnumerator TypeText(Text dialogText, System.Action callback)
    {
        string toWrite = dialogText.text;
        dialogText.text = "";
        dialogText.enabled = true;
        time = DateTime.Now.AddSeconds(waitSecondsBefore);
        while (DateTime.Now < time)
        {
            yield return null;
        }
        for (int i = 0; i < toWrite.Length; i++)
        {
            if (toWrite[i] == '\n')
            {
                time = DateTime.Now.AddSeconds(waitSecondsPerNewLine);
                while (DateTime.Now < time)
                {
                    yield return null;
                }
            }
            else
            {
                time = DateTime.Now.AddSeconds(waitSecondsPerChar);
                while (DateTime.Now < time)
                {
                    yield return null;
                }
            }
            dialogText.text += toWrite[i];
            yield return null;
        }
        time = DateTime.Now.AddSeconds(waitSecondsAfter);
        while (DateTime.Now < time)
        {
            yield return null;
        }
        callback();
    }
}
