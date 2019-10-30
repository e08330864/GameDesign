using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TypingText : MonoBehaviour
{
    private Text textField;
    private string textToType;
    void Start()
    {
        textField = GetComponent<Text>();
        textToType = textField.text;
        textField.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < textToType.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            textField.text += textToType[i];
        }
    }
}
