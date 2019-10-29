using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a question that is processed in a minigame.
/// It contains the question text and a list of possible answers
/// </summary>
public class MGIQuestion
{
    public string questionText = "";
    public List<MGIAnswer> answers = new List<MGIAnswer>();
}
