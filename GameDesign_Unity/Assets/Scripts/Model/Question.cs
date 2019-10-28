using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    private string text = "";
    private List<AnswerX> answers = new List<AnswerX>();
    
    // Start is called before the first frame updateb
    void Start()
    {
        
    }

    //-------------------------------------------------------------
    public void AddAnswer(
        string text,
        float startValuePatience,
        float stopValuePatience,
        float startValueEnergy,
        float stopValueEnergy)
    {
        answers.Add(new AnswerX(text, startValuePatience, stopValuePatience, startValueEnergy, stopValueEnergy));
    }
}
