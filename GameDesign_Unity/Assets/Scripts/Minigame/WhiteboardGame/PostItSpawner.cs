using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PostItSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject postItPrefab;
    public PostItTarget[] targets;
    public Text counter;

    [HideInInspector]
    public int difficulty;

    private PostIt draggedPostIt;
    private Color spawnColor;
    private int targetCount;
    private WhiteboardGame whiteBoard;
    private int correctCount;

    public void Init()  
    {
        whiteBoard = FindObjectOfType<WhiteboardGame>();
        var maxTargets = targets.Length;
        targetCount = Mathf.Clamp( Mathf.RoundToInt(difficulty / 5.0f * maxTargets) + 1, 1, maxTargets);
        counter.text = ""+targetCount;
        spawnColor = this.GetComponent<Image>().color;
        
        for(int i = 0; i < targets.Length; i++)
        {
            if(i < targetCount)
            {
                targets[i].spawner = this;
                targets[i].gameObject.SetActive(true);
                targets[i].gameObject.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                targets[i].gameObject.SetActive(false);
                targets[i].gameObject.transform.parent.gameObject.SetActive(false);
            }
        }

    }

    internal void updateCorrect()
    {
        correctCount = 0;

        foreach (PostItTarget t in targets)
        {
            if (t == null) continue;
            if (t.gameObject.activeInHierarchy && t.hasPostIt)
            {
                correctCount++;
                if (correctCount == targetCount)
                {
                    if (whiteBoard is WhiteboardYesOnly)
                        (whiteBoard as WhiteboardYesOnly).Answered(this);
                    else
                        whiteBoard.Answered(this);
                }
            }
        }
        counter.text = "" + (targetCount - correctCount);
    }

    public void updateTimeLimit(float timeLeft, float totalTime)
    {
        if (timeLeft < 2) return;
        foreach (PostItTarget t in targets)
        {
            t.transform.localScale = Vector3.one * timeLeft/totalTime;
        }
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        if (!whiteBoard.enabled || !enabled) return;
        draggedPostIt = GameObject.Instantiate(postItPrefab, 
                                        Input.mousePosition, 
                                        Quaternion.identity,
                                        this.transform.parent)
                                            .GetComponent<PostIt>();
        draggedPostIt.spawner = this;
        draggedPostIt.GetComponent<Image>().color = spawnColor;
        counter.text = "" + (targetCount - correctCount - 1);
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        if (!whiteBoard.enabled || !enabled || draggedPostIt == null) return;
        draggedPostIt.Release();
        counter.text = "" + (targetCount - correctCount);
    }
}
