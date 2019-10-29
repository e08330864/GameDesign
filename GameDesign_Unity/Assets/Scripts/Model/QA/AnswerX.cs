using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerX
{
    // parameters
    private string text = "";
    private float startValuePatience = 0f;
    private float stopValuePatience = 0f;
    private float startValueEnergy = 0f;
    private float stopValueEnergy = 0f;
    // sied effects of given answere
    private AnswerXdictSideEffect sideEffects = new AnswerXdictSideEffect();

    public AnswerX (
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

    public void AddSideEffect(AnswerX effectedAnswer, float effectMulFac)
    {
        sideEffects.Add(effectedAnswer, effectMulFac);
    }

    public AnswerXdictSideEffect GetSideEffects()
    {
        return sideEffects;
    }
}
