using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : LevelController
{
    [TextArea]
    public string gameShortText;

    [TextArea]
    public string question;

    [Space(20)]

    [TextArea]
    public string yesAnswer;
    [TextArea]
    public string yesTimelineText;
    public ResourceDeltas yesDeltas;

    [Space(20)]

    [TextArea]
    public string noAnswer;
    [TextArea]
    public string noTimelineText;
    public ResourceDeltas noDeltas;

    [Header("Silent Answer")]
    [TextArea]
    public string silentTimelineText;
    public ResourceDeltas silentDeltas;

    [Space(20)]
    public Character gameCharacterPrefab;
    private Character character;

    [Header("Adjust Difficulty of Yes Answer")]
    public List<MinigameLevel> yesMoreDifficultYes;
    public List<MinigameLevel> noMoreDifficultYes;
    public List<MinigameLevel> yesEasierYes;
    public List<MinigameLevel> noEasierYes;

    [Header("Adjust Difficulty of No Answer")]
    public List<MinigameLevel> yesMoreDifficultNo;
    public List<MinigameLevel> noMoreDifficultNo;
    public List<MinigameLevel> yesEasierNo;
    public List<MinigameLevel> noEasierNo;

    internal int yesDifficulty = 0;
    internal int noDifficulty = 0;

    internal readonly int minDifficulty = 0;
    internal readonly int maxDifficulty = 5;

    public void Awake()
    {
        Panel panel = FindObjectOfType<Panel>();
        
        // character game initialization - if not already initialized
        GameObject characterGO = null;
        if ((characterGO = GameObject.FindGameObjectWithTag(gameCharacterPrefab.tag)) == null)
        {
            character = Instantiate(gameCharacterPrefab);
            character.transform.parent = GameObject.Find("SympathyCharacters")?.transform;
        }
        else
        {
            character = characterGO.GetComponent<Character>();
        }


        yesDifficulty = calcDifficulty(yesMoreDifficultYes, yesMoreDifficultNo, yesEasierYes, yesEasierNo, FindObjectOfType<Stress>().Value);
        noDifficulty = calcDifficulty(noMoreDifficultYes, noMoreDifficultNo, noEasierYes, noEasierNo, FindObjectOfType<Stress>().Value);
    }

    internal void FinishLevel(Answer answer)
    {
        FindObjectOfType<Storyboard>().FinishLevel(answer, character, gameShortText);
    }

    private int calcDifficulty(List<MinigameLevel> yesMoreDifficult, List<MinigameLevel> noMoreDifficult, List<MinigameLevel> yesEasier, List<MinigameLevel> noEasier, int stress)
    {
        int difficulty = 0;

        yesMoreDifficult.ForEach(lvl =>
        {
            difficulty += minigameAnswerIs(lvl, AnswerValue.YES);
        });

        noMoreDifficult.ForEach(lvl =>
        {
            difficulty += minigameAnswerIs(lvl, AnswerValue.NO);
        });

        yesEasier.ForEach(lvl =>
        {
            difficulty -= minigameAnswerIs(lvl, AnswerValue.YES);
        });

        noEasier.ForEach(lvl =>
        {
            difficulty -= minigameAnswerIs(lvl, AnswerValue.NO);
        });

        return Mathf.Clamp(difficulty+stress, minDifficulty, maxDifficulty);
    }

    private int minigameAnswerIs(MinigameLevel minigame, AnswerValue value)
    {
        return minigame.answer.answer == value ? 1 : 0;
    }
}
