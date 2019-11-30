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
        FindObjectOfType<Stress>().SetValue(3);
        //FindObjectOfType<Money>().SetValue(100);
    }

    public Level GetLevelByName(string name)
    {
        return levels.Find(e => e.name == name);
    }

    internal void FinishLevel(Answer answer, Character character, string gameShortText)
    {
        destroyCurrentLevel();
        applyDeltas(answer);

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
            minigame.answer = answer;

            StartCoroutine(ShowAnswer(answer.timeLineText));
            FindObjectOfType<TimeLine>().AddDecisionPoint(character, minigame.answer, gameShortText);
        }
        else //level was a Cutscene
        {
            SpawnNextLevel();
        }
    }

    private void applyDeltas(Answer answer)
    {
        //Stress Delta Apply
        //Money Delta Apply
        //Sympathy Apply
    }

    private void destroyCurrentLevel()
    {
        if (currentLevel != null)
        {
            GameObject.DestroyImmediate(currentLevel);
            currentLevel = null;
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
