using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dependency
{
    private AnswerX conditionAnswer;         // the answer that is checked for given, so that this dependency will be applied on this answer
    private float mulFactorEnergy = 1f;      // factor multiplies this answer energy value
    private float mulFactorPatience = 1f;    // factor multiplies this answer patience value
    private float addValueEnergy = 0f;       // value added to this answer energy value
    private float addValuePatience = 0f;     // value added to this answer patience value

    private string answerAdditionalText = null;  // additional answer text for this answer, in case the conditionAnswer is given
    private string answerTextSubstitute = null;  // substitute of answer text (if null, it will not be considered); in case that more then one conditionAnswers fire for the answer, the last substitute text in the condition list will be the new answer text
    private string mGIitemText = null;           // item text for minigame

    //--------------------------------------------------------------------------------------------------
    public Dependency(
        AnswerX conditionAnswer,
        float mulFactorEnergy,
        float mulFactorPatience,
        float addValueEnergy,
        float addValuePatience,
        string answerAdditionalText,
        string answerTextSubstitute,
        string mGIitemText)
    {
        this.conditionAnswer = conditionAnswer;
        this.mulFactorEnergy = mulFactorEnergy;
        this.mulFactorPatience = mulFactorPatience;
        this.addValueEnergy = addValueEnergy;
        this.addValuePatience = addValuePatience;
        this.answerAdditionalText = answerAdditionalText;
        this.answerTextSubstitute = answerTextSubstitute;
        this.mGIitemText = mGIitemText;
    }

    //--------------------------------------------------------------------------------------------------
    // get-functions
    public AnswerX GetConditionAnswer() { return conditionAnswer; }
    public float GetMulFacEnergy() { return mulFactorEnergy; }
    public float GetMulFacPatience() { return mulFactorPatience; }
    public float GetAddValueEnergy() { return addValueEnergy; }
    public float GetAddValuePatience() { return addValuePatience; }
    public string GetAdditionalText() { return answerAdditionalText; }
    public string GetSubstituteText() { return answerTextSubstitute; }
    public string GetMGIitemText() { return mGIitemText; }
}
