using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public const int neutral_to_answer = 0;
    public const int easy_to_answer = -1;
    public const int difficult_to_answer = 1;

    private string questionText = "";
    private Dictionary<AnswerX, int> answers = new Dictionary<AnswerX, int>();

    /// <summary>
    /// Adding a new answer to the question
    /// </summary>
    /// <param name="rating">difficulty of answer giving: 0=neutral, -1=easy, +1=difficult</param>
    /// <param name="answerText"></param>
    /// <param name="startValuePatience"></param>
    /// <param name="stopValuePatience"></param>
    /// <param name="startValueEnergy"></param>
    /// <param name="stopValueEnergy"></param>
    public void AddAnswer(
        int rating,            
        string answerText,
        float startValuePatience,
        float stopValuePatience,
        float startValueEnergy,
        float stopValueEnergy)
    {
        answers.Add(new AnswerX(answerText, startValuePatience, stopValuePatience, startValueEnergy, stopValueEnergy), rating);
    }

    public Dictionary<AnswerX, int> GetAnswers()
    {
        return answers;
    }
}
