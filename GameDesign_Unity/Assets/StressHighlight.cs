using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressHighlight : MonoBehaviour
{
    public Color stressedColor;
    public GameObject warning;

    Color defaultColor;
    Image bg;
    Stress playerStress;

    // Start is called before the first frame update
    void Start()
    {
        playerStress = FindObjectOfType<Stress>();
        bg = GetComponent<Image>();    
        defaultColor = bg.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStress.Value >= 4)
        {
            bg.color = Color.Lerp(defaultColor, stressedColor, Mathf.PingPong(Time.time, 1));
            warning.SetActive(true);
        }
        else
        {
            bg.color = defaultColor;
            warning.SetActive(false);
        }
    }
}
