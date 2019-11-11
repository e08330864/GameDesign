using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storyboard: MonoBehaviour {

    public GameObject answerOverlay;
    public float displayAnswerDuration;
    public GameObject countdownPrefab;
    public List<Level> levels;
    public Transform levelParent;
    public int currentLevelIndex;

    private GameObject currentLevel;

    public void Start()
    {
        SetPanelValues();
        SpawnNextLevel();
    }

    private void SetPanelValues()
    {
        FindObjectOfType<Energy>().SetValue(3);
        FindObjectOfType<Patience>().SetValue(3);
        FindObjectOfType<Lives>().SetValue(3);
    }

    public Level GetLevelByName(string name)
    {
        return levels.Find(e => e.name == name);
    }

    internal void FinishLevel(Answer? answer, string timelineText)
    {
        if (currentLevel != null)
        {
            GameObject.DestroyImmediate(currentLevel);
            currentLevel = null;
        }
        MinigameLevel minigame = levels[currentLevelIndex] as MinigameLevel;
        if (minigame != null)
        {
            minigame.timelineText = timelineText;
            minigame.answer = answer.Value;
            StartCoroutine(ShowAnswer(timelineText));
        }
        else
        {
            SpawnNextLevel();
        }
    }

    private IEnumerator ShowAnswer(string timelineText)
    {
        answerOverlay.GetComponentInChildren<Text>().text = timelineText;
        answerOverlay.SetActive(true);
        yield return new WaitForSeconds(displayAnswerDuration);
        answerOverlay.SetActive(false);
        SpawnNextLevel();
    }

    private void SpawnNextLevel()
    {
        currentLevelIndex++;
        if(currentLevelIndex < levels.Count)
        {
            currentLevel = GameObject.Instantiate(levels[currentLevelIndex].prefab, levelParent);
            if(levels[currentLevelIndex] is MinigameLevel)
                GameObject.Instantiate(countdownPrefab, levelParent);
        }
    }
}
