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
    private LevelController controller;

    void Start()
    {
        counter = GetComponentInChildren<Text>();
        controller = FindObjectOfType<LevelController>();
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
        this.controller.StartLevel();
        this.gameObject.SetActive(false);
    }
}
