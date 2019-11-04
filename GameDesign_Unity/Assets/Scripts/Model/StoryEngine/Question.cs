using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Question class is representing a question that can be asked by an arbitrary identity
/// to an other arbitrary identity.
/// It contains the question text and a list of possible answers
/// </summary>
public class Question
{
    private string questionText = "";
    private List<AnswerX> answers = new List<AnswerX>();

    //--------------------------------------------------------------------------------------------------
    /// <summary>
    /// Question constructor
    /// </summary>
    /// <param name="questionText">question text</param>
    public Question(string questionText)
    {
        this.questionText = questionText;
    }

    //--------------------------------------------------------------------------------------------------
    /// <summary>
    /// Adding a new answer to the question
    /// </summary>
    /// <param name="answerText"></param>
    /// <param name="difficultyRate">difficulty of answer giving: 0=neutral, -1=easy, +1=difficult</param>
    /// <param name="startValuePatience"></param>
    /// <param name="stopValuePatience"></param>
    /// <param name="startValueEnergy"></param>
    /// <param name="stopValueEnergy"></param>
    public AnswerX AddAnswer(
        string answerText,
        int difficultyRate,
        float startValuePatience,
        float stopValuePatience,
        float startValueEnergy,
        float stopValueEnergy)
    {
        AnswerX returnValue;
        answers.Add(returnValue = new AnswerX(answerText, difficultyRate, startValuePatience, stopValuePatience, startValueEnergy, stopValueEnergy));
        return returnValue;
    }

    //--------------------------------------------------------------------------------------------------
    // get-functions
    public string GetQuestionText() { return questionText; }

    /// <summary>
    /// returns the list of possible answers to the question
    /// </summary>
    /// <returns> list of possible answers</returns>
    public List<AnswerX> GetAnswers() { return answers; }
}
