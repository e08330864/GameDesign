using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storyboard: MonoBehaviour {

    public GameObject answerOverlay;
    public GameObject ambulanceOverlay;
    public GameObject trainingResultOverlay;
    public LendMoney lendMoneyOverlay;
    public GameOverOverlay gameOver;
    public float displayAnswerDuration;
    public GameObject countdownPrefab;
    public List<Level> levels;
    public Transform levelParent;
    public int currentLevelIndex;
    public int lifes = 2;
    public GameObject nextButton;
    public GameObject arrowTrainingPrefab;
    public List<int> trainingAtLevel;

    [HideInInspector]
    public int currentJitterReduction;
    [HideInInspector]
    public Stress stress;
    [HideInInspector]
    public Money money;
    private GameObject arrowTraining;
    private GameObject currentLevel;

    public void Start()
    {
        SetPanelValues();
        SpawnNextLevel();
    }

    private void SetPanelValues()
    {
        stress = FindObjectOfType<Stress>();
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

            minigame.character = character;
            minigame.answer = answer;
            nextButton.SetActive(true);
            ShowAnswer(answer);
            if (minigame.answer != null)
            {
                FindObjectOfType<TimeLine>().AddDecisionPoint(character, minigame.answer, gameShortText);
            }
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
            GameObject.Destroy(currentLevel);
            currentLevel = null;
        }
    }

    public void GoNextButtonPressed()
    {
        if (lendMoneyOverlay.gameObject.activeInHierarchy)
        {
            lendMoneyOverlay.gameObject.SetActive(false);
            nextButton.SetActive(true);
            SpawnNextLevel();
        }

        if (answerOverlay.activeInHierarchy) //Player in Answer Screen
        {
            answerOverlay.SetActive(false);

            if (stress.Value >= 5)
            {
                lifes--;
                if (lifes <= 0)
                {
                    GameOver("Du hättest auf den Arzt hören sollen...");
                    return;
                }
                ambulanceOverlay.SetActive(true);
                nextButton.SetActive(false);
            }
            else
            {
                SpawnNextLevel();
            }
        }else if (ambulanceOverlay.activeInHierarchy) // Ambulance Screen open
        {
            ambulanceOverlay.SetActive(false);
            nextButton.SetActive(true);
            SpawnNextLevel();
        }else if(arrowTraining != null)
        {
            Destroy(arrowTraining);
            arrowTraining = null;
            trainingResultOverlay.SetActive(true);
        }else if (trainingResultOverlay.activeInHierarchy)
        {
            trainingResultOverlay.SetActive(false);
            SpawnNextLevel();
        }
        else if (currentLevelIndex < levels.Count && levels[currentLevelIndex] is CutSceneLevel)
        {
            currentLevel.GetComponent<TextSceneController>().SkipText();
        }
    }

    private void ShowAnswer(Answer answer)
    {
        answerOverlay.GetComponentInChildren<Text>().text = answer.timeLineText;
        answerOverlay.SetActive(true);
    }

    private void SpawnNextLevel()
    {
        if (trainingAtLevel.Contains(currentLevelIndex)){
            SpawnArrowTraining();
            return;
        }else if (money.Value < 0)
        {
            lendMoneyOverlay.Init();
            nextButton.SetActive(false);
            lendMoneyOverlay.gameObject.SetActive(true);
            return;
        }
        else
        {
            currentLevelIndex++;
        }

        if(currentLevelIndex < levels.Count)
        {
            GameObject levelContainer = null;
            if ((levelContainer = GameObject.FindGameObjectWithTag("LevelContainer")) == null)
            {
                Debug.LogError("levelContainer is NULL in Storyboard");
            }
            var nextLevel = levels[currentLevelIndex].prefab.GetComponent<LevelController>();

            if (shouldSkip(nextLevel))
            {
                SpawnNextLevel();
                return;
            }

            currentLevel = GameObject.Instantiate(levels[currentLevelIndex].prefab, levelParent);
            currentLevel.gameObject.transform.SetParent(levelContainer.transform);
            if (levels[currentLevelIndex] is MinigameLevel)
            {
                nextButton.SetActive(false);
                GameObject.Instantiate(countdownPrefab, levelParent);
            }
        } else
        {
            GameOver("Gratuliere du hast es bis zum Ende geschafft!");
        }
    }

    private void SpawnArrowTraining()
    {
        trainingAtLevel.Remove(currentLevelIndex);
        nextButton.SetActive(false);
        arrowTrainingPrefab.GetComponent<Canvas>().worldCamera = Camera.main;
        arrowTraining = GameObject.Instantiate(arrowTrainingPrefab);
    }

    private bool shouldSkip(LevelController nextLevel)
    {
        bool shouldSkip = false;
        nextLevel.skipOnYes.ForEach((level) =>
        {
            if (level.answer.answer == AnswerValue.YES)
            {
                Debug.Log("Skipping Level: " + nextLevel.name);
                shouldSkip = true;
            }
        });
        nextLevel.skipOnNo.ForEach((level) =>
        {
            if (level.answer.answer == AnswerValue.NO)
            {
                Debug.Log("Skipping Level: " + nextLevel.name);
                shouldSkip = true;
            }
        });
        nextLevel.skipOnIgnore.ForEach((level) =>
        {
            if (level.answer.answer == AnswerValue.IGNORE)
            {
                Debug.Log("Skipping Level: " + nextLevel.name);
                shouldSkip = true;
            }
        });
        return shouldSkip;
    }

    public void GameOver(string endText)
    {
        nextButton.SetActive(false);
        gameOver.GameOver(endText);
    }

}
