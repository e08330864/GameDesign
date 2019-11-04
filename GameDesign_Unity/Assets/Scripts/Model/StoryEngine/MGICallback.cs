using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGICallback : MonoBehaviour
{
    private bool waiting = false;
    private Question question = null;

    // minigame values
    private MGIQuestion mGIQuestion = null;
    private float energy = 0f;
    private float patience = 0f;
    private MGIAnswer givenAnswer = null;

    //--------------------------------------------------------------------------------------------------
    /// <summary>
    /// Function is called before minigame is started
    /// </summary>
    public void WaitForMinigame(Question question)
    {
        if (!waiting)
        {
            this.question = question;
            waiting = true;
            CheckMinigameResult();
        }
    }

    IEnumerator CheckMinigameResult()
    {
        yield return new WaitUntil(() => !waiting);
    }

    private void CreateMGIQuestion()
    {
        if (question != null)
        {
            mGIQuestion = new MGIQuestion();
            string questionText = question.GetQuestionText();
            foreach (AnswerX answer in question.GetAnswers())
            {
                string answerText = answer.GetAnswerText();
                foreach (Dependency dependency in answer.GetDependencies())
                {

                }
                //mGIQuestion.answers.Add(new MGIAnswer())
            }


            //
        }
        else { Debug.LogError("question is NULL in MGICallback"); }
    }

    //--------------------------------------------------------------------------------------------------
    /// <summary>
    /// Function must be called from minigame at the end, to return the minigame result and indicate proceeding
    /// </summary>
    /// <param name="energy">The delta value for energy</param>
    /// <param name="patience">The delta value for patience</param>
    /// <param name="givenAnswer">The given answer object; NULL, if no answer was given</param>
    public void MinigameResult(float energy, float patience, MGIAnswer givenAnswer)
    {
        this.energy = energy;
        this.patience = patience;
        this.givenAnswer = givenAnswer;
        waiting = false;
    }
}
