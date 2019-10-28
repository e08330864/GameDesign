using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    private string text = "";
    private float startValuePatience = 0f;
    private float stopValuePatience = 0f;
    private float startValueEnergy = 0f;
    private float stopValueEnergy = 0f;

    public Answer(
        string text,
        float startValuePatience,
        float stopValuePatience,
        float startValueEnergy,
        float stopValueEnergy)
    {
        this.text = text;
        this.startValuePatience = startValuePatience;
        this.stopValuePatience = stopValuePatience;
        this.startValueEnergy = startValueEnergy;
        this.stopValueEnergy = stopValueEnergy;
    }


}
