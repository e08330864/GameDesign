using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScreenController : MonoBehaviour
{
    public Text dialogText;
    public string nextScene;

    private string toWrite = "Wenn Sie weiterhin so wenig Motivation an den Tag legen sind Ihre Tage bei ACME Consulting gezählt! \n \n" +
        "                                       \n"+
        "Nach einem langem Arbeitstag kommst Du nach hause zu deiner Frau...";

    // Start is called before the first frame update
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
        SceneManager.LoadScene(nextScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
