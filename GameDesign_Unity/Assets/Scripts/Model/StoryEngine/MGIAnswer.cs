using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents an answer that is processed in a minigame.
/// Instances of these are list elements of the answer list in MGIQuestion.
/// </summary>
public class MGIAnswer
{
    // parameters
    public string answerText = "";
    public float difficultyRate = 0;        // difficulty of answer to give; 0=neutral; minus values are easier; plus values are more difficult
    public float startValuePatience = 0f;   // value, when minigame starts
    public float stopValuePatience = 0f;    // value, when minigame stops
    public float startValueEnergy = 0f;     // value, when minigame starts
    public float stopValueEnergy = 0f;      // value, when minigame stops
    public List<string> MGItextItems = new List<string>(); // list of text-items as preconditions for this answer
    
    //--------------------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor for possible answers to be processed by minigame
    /// </summary>
    /// <param name="answerText"></param>
    /// <param name="difficultyRate"></param>
    /// <param name="startValuePatience"></param>
    /// <param name="stopValuePatience"></param>
    /// <param name="startValueEnergy"></param>
    /// <param name="stopValueEnergy"></param>
    public MGIAnswer (
        string answerText,
        int difficultyRate,
        float startValuePatience,
        float stopValuePatience,
        float startValueEnergy,
        float stopValueEnergy)
    {
        this.answerText = answerText;
        this.difficultyRate = difficultyRate;
        this.startValuePatience = startValuePatience;
        this.stopValuePatience = stopValuePatience;
        this.startValueEnergy = startValueEnergy;
        this.stopValueEnergy = stopValueEnergy;
    }
}
