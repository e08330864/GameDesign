using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storyboard: MonoBehaviour {

    public GameObject answerOverlay;
    public GameObject ambulanceOverlay;
    public float displayAnswerDuration;
    public GameObject countdownPrefab;
    public List<Level> levels;
    public Transform levelParent;
    public int currentLevelIndex;

    private GameObject currentLevel;
    private Energy energy;

    public void Start()
    {
        SetPanelValues();
        SpawnNextLevel();
    }

    private void SetPanelValues()
    {
        energy = FindObjectOfType<Energy>();
        energy.SetValue(3);
        FindObjectOfType<Patience>().SetValue(3);
        FindObjectOfType<Lives>().SetValue(3);
    }

    public Level GetLevelByName(string name)
    {
        return levels.Find(e => e.name == name);
    }

    internal void FinishLevel(Answer? answer, string timelineText)
    {
        energy = FindObjectOfType<Energy>();
        if (currentLevel != null)
        {
            GameObject.DestroyImmediate(currentLevel);
            currentLevel = null;
        }

        if(energy.Value <= 0)
        {
            ambulanceOverlay.SetActive(true);
            Debug.Log("GAME OVER!");
            return;
        }

        MinigameLevel minigame = levels[currentLevelIndex] as MinigameLevel;
        if (minigame != null)
        {
            minigame.timelineText = timelineText;
            minigame.answer = answer.Value;

            //TODO: Don't hardcode this...
            if (minigame.answer == Answer.A)
                timelineText += "\n Patience -1 \n Energy +1";
            else if(minigame.answer == Answer.B)
                timelineText += "\n Energy -1";
            else if(minigame.answer == Answer.None)
                timelineText += "\n Energy -2";

            if (energy.Value == 1)
                timelineText += "\n \n Nicht mehr viel Energy übrig, sei vorsichtig....";

            StartCoroutine(ShowAnswer(timelineText));
        }
        else //level was a Cutscene
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
