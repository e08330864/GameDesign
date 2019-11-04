using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerX
{
    // answer difficulty levels (can be influenced by multiplication factors and/or addition values of Conditions)
    public const int easy_to_answer = -1;
    public const int neutral_to_answer = 0;
    public const int difficult_to_answer = 1;

    // parameters
    private string answerText = "";
    private int difficultyRate = 0;
    private float startValuePatience = 0f;
    private float stopValuePatience = 0f;
    private float startValueEnergy = 0f;
    private float stopValueEnergy = 0f;
    private List<Dependency> dependencies = new List<Dependency>(); // this answer dependencies on previously given answeres
    private bool isGiven = false;   // answer flag: false=this answer was not given; true=this answer was given

    //--------------------------------------------------------------------------------------------------
    public AnswerX (
        string answerText,
        int difficultyRate,
        float startValuePatience,
        float stopValuePatience,
        float startValueEnergy,
        float stopValueEnergy)
    {
        this.answerText = answerText;
        this.difficultyRate = difficultyRate;
        this.startValuePatience = startValuePatience;
        this.stopValuePatience = stopValuePatience;
        this.startValueEnergy = startValueEnergy;
        this.stopValueEnergy = stopValueEnergy;
    }

    //--------------------------------------------------------------------------------------------------
    public Dependency AddDependency(
        AnswerX conditionAnswer,
        float mulFactorEnergy,
        float mulFactorPatience,
        float addValueEnergy,
        float addValuePatience,
        string questionAdditionalText,
        string questionTextSubstitute,
        string mGIItemText)
    {
        Dependency returnValue;
        dependencies.Add(returnValue = new Dependency(
            conditionAnswer,
            mulFactorEnergy,
            mulFactorPatience,
            addValueEnergy,
            addValuePatience,
            questionAdditionalText,
            questionTextSubstitute,
            mGIItemText));
        return returnValue;
    }

    //--------------------------------------------------------------------------------------------------
    // get-functions
    public string GetAnswerText() { return answerText; }
    public int GetDifficultyRate() { return difficultyRate; }
    public float GetStartValuePatience() { return startValuePatience; }
    public float GetStopValuePatience() { return stopValuePatience; }
    public float GetStartValueEnergy() { return startValueEnergy; }
    public float GetStopValueEnergy() { return stopValueEnergy; }
    public List<Dependency> GetDependencies() { return dependencies; }
    public bool IsGiven() { return isGiven; }
}
