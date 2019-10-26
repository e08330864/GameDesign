# GameDesign
Game Design 2019W  
Unity 2019.2.10f1  
Git LFS: https://git-lfs.github.com/

## Documentation

### Whiteboard Minigame

Testscene: Scenes/Minigames/Whiteboard.unity

TestMiniGameSpawner on Canvas implements the IGameManager Interface and spawns the WhiteboardMinigame.prefab, sets the required MinigameInput and activates its root Gameobject.

Neutral Difficulty:
Player has to assign 3 PostIts in 10 seconds.

Hard Difficulty:
Player has to assign 5 PostIts in 7 seconds.

Easy Difficulty:
Player has to assign 1 PostIt in 13 seconds.
