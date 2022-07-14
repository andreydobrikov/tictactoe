# TicTacToe
A configurable version of Tic Tac Toe that uses common patterns like EventDispatcher and configurable scriptable objects.

# Instructions
To Run TicTacToe simply open the TicTacToe scene and run in the Editor.<p>
The scene will start with a simple menu to start the game.<br>
<img width="961" alt="tictactoe1" src="https://user-images.githubusercontent.com/512300/178753734-bbee8712-cf83-4846-bba7-3b619b97b4d2.png">

<p>After the game starts, it will take you to the board to play a game against the AI. After the game is finished, it will take you back to the start menu.<br>
<img width="926" alt="tictactoe2" src="https://user-images.githubusercontent.com/512300/178754070-ea3862e1-7db5-46dc-b73b-ed3a0d171742.png">

# Details and Configuration
TicTacToe is configurable via scriptable objects. <p>
Board data is separate from rendering and any renderer that implements IBoardRenderer interface can display the board details. For the sake of simplicity 2 different renderer are already implemented - debug renderer and 2d game board renderer. <p>
Initial board data has also been made configurable. Since Unity's default int[,] has issue with Editor window display, the board scriptable objects are devided into rows and board accepts an array of rows. Not, the number of rows need to match the size of the board which is configurable too inside BoardData class. 
Note, you can change the scriptable objects at runtime and board should adapt to that change on next game as long as the size of the board has not changed.<p>
Sound effects are also configurable via SFXMapper scriptable object. Those can be swapped at runtime and the changes are applied without restarting the game.

# Architecture 
At it's core TicTacToe is build around a pattern with having data not know anything about the view. At its core, there is a [GameStateController](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/GameStateController.cs) class which is responsible for resetting the game state and setting the turn state. It holds references to BoardController, UIController and SoundManager classes. It uses a [GlobalEventDispatcher](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Dispatcher/GlobalEventDispatcher.cs) as an [EventDispatcher](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Dispatcher/EventDispatcher.cs) to publish events about turntaker and subscribe to events about tile data. By going through a dispatcher mechanism, the GameStateController does not have to have any direct dependencies on those separate systems such as tile rendering and makes it easier to test.<p>
[BoardController](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/BoardController.cs) class is a single responsibility entity that acts as a mediator between the actual BoardData and BoardRenderer. As a general rule it has no other dependencies and is very limited in its scope.<p>
[UIController](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/UIController.cs) class is a single responsibility entity that sets the UI state of game and passes the load command to the Game State Controller.<p>
[SoundManager](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/SoundManager.cs) class is in charge of playing effects of a specific type. It uses the SFXMapper to map the actual effect to the type of action/state that happens to call it. It also provides fading in/out functionality so that sounds don't override each other in jarring way.<p>
Most of the rendering logic happens either in [GameBoardRenderer](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/GameBoardRenderer.cs) (which implements BoardRenderer) and [TileRenderer](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/TileRenderer.cs). TileRenderer also implements effect transitions when the mouse is over the tile while the actual transition firing happens in [TileButton](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/TileButton.cs) class. TileRenderer also fires a global [TileClickedEvent](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/TileClickedEvent.cs) for anyone to listen to. In particar, GameStateController listen to that event type so that it can update the game state when that happens.<p>
[BoardData](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/BoardData.cs) and [TileData](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/BoardData.cs) are pure data classes with sone utility functionality and have no dependencies. 
The majority of AI tile selection logic happens in [MiniMaxEvaluator](https://github.com/andreydobrikov/tictactoe/blob/main/Assets/Scripts/Game/MiniMaxEvaluator.cs) class which provides static implementation of [MiniMax algorithm](https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-4-alpha-beta-pruning/) with alpha/beta prunning and depth tolerance. The depth tolerance is required for large trees when the board exceeds the size of the regular 3x3 board.
 
 
 
 
 
