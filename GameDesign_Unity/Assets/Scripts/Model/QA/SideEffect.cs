using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEffect
{
    private float mulFactorEnergy = 1f;      // factor multiplies the question energy value
    private float mulFactorPatience = 1f;    // factor multiplies the question patience value
    private float addValueEnergy = 0f;       // value added to the question energy value
    private float addValuePatience = 0f;     // value added to the question patience value

    private string questionAlternativeText = null;  // alternative question text for effected question

    public SideEffect(
        float mulFactorEnergy,
        float mulFactorPatience,
        float addValueEnergy,
        float addValuePatience,
        string questionAlternativeText)
    {
        this.mulFactorEnergy = mulFactorEnergy;
        this.mulFactorPatience = mulFactorPatience;
        this.addValueEnergy = addValueEnergy;
        this.addValuePatience = addValuePatience;
        this.questionAlternativeText = questionAlternativeText;
    }

    public float GetMulFacEnergy() { return mulFactorEnergy; }

    public float GetMulFacPatience() { return mulFactorPatience; }

    public float GetAddValueEnergy() { return addValueEnergy; }

    public float GetAddValuePatience() { return addValuePatience; }

    public string GetAlternativeText() { return questionAlternativeText; }
}
