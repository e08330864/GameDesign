using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int countdownFrom;
    public int delayBy;

    private Text counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = GetComponentInChildren<Text>();
        StartCoroutine(Count());
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(delayBy);
        counter.text = "" + countdownFrom;
        for (int i = countdownFrom-1; i > 0; i--)
        {
            counter.text = ""+i;
            yield return new WaitForSeconds(1.0f);
        }
        this.gameObject.SetActive(false);
        FindObjectOfType<Minigame>().BeginGame();
    }
}
