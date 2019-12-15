using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesOrNo : MinigameController
{
    public GameObject yesObject;
    public GameObject noObject;



    public void AnswerYes()
    {
      
        var yes = new Answer(AnswerValue.YES, yesTimelineText, yesDeltas);

        FinishLevel(yes);

    }
    public void AnswerNo()
    {
        StopAllCoroutines();
        var no = new Answer(AnswerValue.NO, noTimelineText, noDeltas);

        FinishLevel(no);

    }

}
