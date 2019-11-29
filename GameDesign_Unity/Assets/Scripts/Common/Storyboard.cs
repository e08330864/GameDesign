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
    private Stress stress;

    public void Start()
    {
        SetPanelValues();
        SpawnNextLevel();
    }

    private void SetPanelValues()
    {
        stress = FindObjectOfType<Stress>();
        stress.SetValue(3);
        FindObjectOfType<Stress>().SetValue(3);
    }

    public Level GetLevelByName(string name)
    {
        return levels.Find(e => e.name == name);
    }

    internal void FinishLevel(AnswerValue answerValue, string timeLineText, Character character, string gameShortText)
    {
        stress = FindObjectOfType<Stress>();
        if (currentLevel != null)
        {
            GameObject.DestroyImmediate(currentLevel);
            currentLevel = null;
        }

        if(stress.Value <= 0)
        {
            ambulanceOverlay.SetActive(true);
            Debug.Log("GAME OVER!");
            return;
        }

        MinigameLevel minigame = levels[currentLevelIndex] as MinigameLevel;
        if (minigame != null)
        {
            minigame.character = character;
            minigame.answer.answer = answerValue;
            minigame.answer.timeLineText = timeLineText;
            
            //TODO: Don't hardcode this...
            if (minigame.answer.answer == AnswerValue.A)
                minigame.answer.timeLineText += "\n Patience -1 \n Energy +1";
            else if(minigame.answer.answer == AnswerValue.B)
                minigame.answer.timeLineText += "\n Energy -1";
            else if(minigame.answer.answer == AnswerValue.None)
                minigame.answer.timeLineText += "\n Energy -2";

            if (stress.Value == 1)
                minigame.answer.timeLineText += "\n \n Nicht mehr viel Energy übrig, sei vorsichtig....";

            StartCoroutine(ShowAnswer(timeLineText));
            FindObjectOfType<TimeLine>().AddDecisionPoint(character, minigame.answer, gameShortText);
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
            GameObject levelContainer = null;
            if ((levelContainer = GameObject.FindGameObjectWithTag("LevelContainer")) == null)
            {
                Debug.LogError("levelContainer is NULL in Storyboard");
            }
            currentLevel = GameObject.Instantiate(levels[currentLevelIndex].prefab, levelParent);
            currentLevel.gameObject.transform.SetParent(levelContainer.transform);
            if (levels[currentLevelIndex] is MinigameLevel)
                GameObject.Instantiate(countdownPrefab, levelParent);
        }
    }
}
