# GameDesign
Game Design 2019W  
Unity 2019.2.10f1  
Git LFS: https://git-lfs.github.com/

## Documentation

### Structure

The "main" scene is the only scene in our game, the SCRIPT GameObject contains the Storyboard Script which holds our Levels as a List of ScriptableObjects.The Storyboard also handles spawning a new Level and finishing a Level with the FinishLevel(Answer? answer) function, a Minigame is supposed to call this with the given answer when its game is finished, a CutScene calls it without a parameter. Each Level Prefab has a root GameObject with a Script which inherits from the LevelController class, this LevelController should look up its dependent Levels via Storyboard.GetLevelByName() to check for previous Answers and adjust its difficulty accordingly.

### Scriptable Objects

 These Level Objects can be created in the Project View (folder "Levels") -> Right Click -> Level -> CutScene or Minigame. Both types of Levels hold a Prefab which is spawned on the Canvas in the main scene. 