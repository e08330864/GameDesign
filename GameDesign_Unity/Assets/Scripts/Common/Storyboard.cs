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
    private Money money;

    public void Start()
    {
        SetPanelValues();
        SpawnNextLevel();
    }

    private void SetPanelValues()
    {
        stress = FindObjectOfType<Stress>();
        stress.SetValue(0);
        money = FindObjectOfType<Money>();
    }

    public Level GetLevelByName(string name)
    {
        return levels.Find(e => e.name == name);
    }

    internal void FinishLevel(Answer answer, Character character, string gameShortText)
    {
        DestroyCurrentLevel();

        MinigameLevel minigame = levels[currentLevelIndex] as MinigameLevel;
        if (minigame != null)
        {
            ApplyDeltas(answer, character);
            if (stress.Value >= 5)
            {
                ambulanceOverlay.SetActive(true);
                Debug.Log("GAME OVER!");
                return;
            }
            minigame.character = character;
            minigame.answer = answer;

            StartCoroutine(ShowAnswer(answer));
            FindObjectOfType<TimeLine>().AddDecisionPoint(character, minigame.answer, gameShortText);
        }
        else //level was a Cutscene
        {
            SpawnNextLevel();
        }
    }

    private void ApplyDeltas(Answer answer, Character character)
    {
        stress.ApplyDelta(answer.deltas.stressDelta);
        money.SetValue(money.Value + answer.deltas.moneyDelta);
        character.applySympathyDelta(answer.deltas.sympathyDelta);
        answer.timeLineText = addDeltasText(answer.timeLineText, answer.deltas);
    }

    private string addDeltasText(string timeLineText, ResourceDeltas deltas)
    {
        timeLineText += "\n\n";
        if (deltas.stressDelta > 0) timeLineText += "Du fühlst dich gestresst.\n";
        else if (deltas.stressDelta < 0) timeLineText += "Du fühlst dich entspannter.\n";

        if (deltas.moneyDelta > 0) timeLineText += "Du bekommst "+deltas.moneyDelta+ "€.\n";
        else if (deltas.moneyDelta < 0) timeLineText += "Du verlierst " + deltas.moneyDelta + "€.\n";

        if (deltas.sympathyDelta > 0) timeLineText += "Dein Gegenüber scheint dich besser Leiden zu können.\n";
        else if (deltas.sympathyDelta < 0) timeLineText += "Dein Gegenüber scheint dich weniger zu mögen.\n";

        return timeLineText;
    }

    private void DestroyCurrentLevel()
    {
        if (currentLevel != null)
        {
            GameObject.DestroyImmediate(currentLevel);
            currentLevel = null;
        }
    }

    private IEnumerator ShowAnswer(Answer answer)
    {
        answerOverlay.GetComponentInChildren<Text>().text = answer.timeLineText;
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
