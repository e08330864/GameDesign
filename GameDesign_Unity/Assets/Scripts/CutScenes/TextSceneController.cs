using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextSceneController : LevelController
{
    public List<Text> dialogTextList;
    private float waitSecondsBefore = 0.5f;
    private float waitSecondsPerChar = 0.002f;
    private float waitSecondsPerNewLine = 0f;
    private DateTime time;
    private string toWrite;
    private Text currentText;
    internal int counter = 0;
    internal bool finished = false;
    internal Image charImage;
    private int charsPerTick = 2;

    void Start()
    {
        charImage = gameObject.transform.Find("Character")?.Find("Image")?.GetComponent<Image>();
        if (dialogTextList.Count > 1)
        {
            if(charImage != null) charImage.enabled = false;
        }
        foreach (Text txt in dialogTextList)
        {
            txt.enabled = false;
        }
        Text dialogText = dialogTextList[0];
        StartCoroutine(TypeText(dialogText, CallNextText));
    }

    internal void CallNextText()  {
        counter += 1;
        if (counter == 1)
        {
            if (charImage != null) charImage.enabled = true;
        }
        if (counter < dialogTextList.Count)
        {
            Text dialogText = dialogTextList[counter];
            StartCoroutine(TypeText(dialogText, CallNextText));
        }
        else
        {
            finished = true;
        }
    } 

    internal IEnumerator TypeText(Text dialogText, System.Action callback)
    {
        toWrite = dialogText.text;
        currentText = dialogText;
        dialogText.text = "";
        dialogText.enabled = true;
        time = DateTime.Now.AddSeconds(waitSecondsBefore);
        while (DateTime.Now < time)
        {
            yield return null;
        }
        for (int i = 0; i < toWrite.Length; i += charsPerTick)
        {
            time = DateTime.Now.AddSeconds(waitSecondsPerChar);
            while (DateTime.Now < time)
            {
                yield return null;
            }
            int startIndex = i - charsPerTick + 1;
            if ( startIndex > 0 )
            {
                for(int j = startIndex; j <= i; j++)
                    dialogText.text += toWrite[j];
            }
            else
            {
                dialogText.text += toWrite[i];
            }
            yield return null;
        }
        dialogText.text = toWrite;
        callback();
    }

    public void SkipText()
    {
        if (finished) {
            FinishLevel();
            return;
        }
        StopAllCoroutines();
        currentText.text = toWrite;
        dialogTextList.ForEach(t => t.enabled = true);
        if (charImage != null) charImage.enabled = true;
        finished = true;
    }
}
