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
    public float difficulty;

    private PostIt draggedPostIt;
    private Color spawnColor;
    private int targetCount;
    private WhiteboardGame whiteBoard;

    public void Init()
    {
        whiteBoard = FindObjectOfType<WhiteboardGame>();
        targetCount = difficulty == 1 ? 3 : 1;
        counter.text = ""+targetCount;
        spawnColor = this.GetComponent<Image>().color;
        
        for(int i = 0; i < targets.Length; i++)
        {
            if(i < targetCount)
            {
                targets[i].spawner = this;
                targets[i].gameObject.SetActive(true);
            }
            else
            {
                targets[i].gameObject.SetActive(false);
            }
        }

    }

    private void Update()
    {
        int correctCount = 0;
        foreach(PostItTarget t in targets)
        {
            if (t.gameObject.activeInHierarchy && t.hasPostIt)
            {
                correctCount++;
                counter.text = ""+(targetCount-correctCount);
                if (correctCount == targetCount) whiteBoard.Answered(this);
            }
        }
    }

    public void updateTimeLimit(float timeLeft, float totalTime)
    {
        if (timeLeft < 0) return;
        foreach(PostItTarget t in targets)
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
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        if (!whiteBoard.enabled || !enabled || draggedPostIt == null) return;
        draggedPostIt.Release();
    }
}
