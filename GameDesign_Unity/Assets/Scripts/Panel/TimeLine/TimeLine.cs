using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour
{
    [SerializeField]
    private DecisionPoint decisionPointPrefab = null;

    //private Storyboard storyboard = null;
    private List<DecisionPoint> decisionPoints = new List<DecisionPoint>();
    private Character character;
    
    // Start is called before the first frame update
    void Start()
    {
        //if ((storyboard = FindObjectOfType<Storyboard>()) == null)
        //{
        //    Debug.LogError("storyBoard is NULL in TimeLine");
        //}
    }

    public void AddDecisionPoint(Character character, string timeLineText)
    {
        DecisionPoint decisionPoint = Instantiate(decisionPointPrefab);
        decisionPoint.gameObject.transform.SetParent(transform, true);
        decisionPoint.SetDecisionText(timeLineText);
        decisionPoint.SetCharacter(character);

        decisionPoints.Add(decisionPoint);
        UpdateTimeLine();
    }

    private void UpdateTimeLine()
    {
        RectTransform rtTimeLine = (RectTransform)gameObject.transform;
        float xPos = 0f;
        float xDelta = rtTimeLine.sizeDelta.x / (decisionPoints.Count + 1);
        foreach (DecisionPoint dp in decisionPoints)
        {
            dp.gameObject.transform.localPosition = new Vector3(xPos, rtTimeLine.rect.height / 2);
            xPos += xDelta;
        }
    }
}
