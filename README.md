# TicTacToe
A configurable version of Tic Tac Toe that uses common patterns like EventDispatcher and configurable scriptable objects.

# Instructions
To Run TicTacToe simply open the TicTacToe scene and run in the Editor.<br>
The scene will start with a simple menu to start the game.
<img width="961" alt="tictactoe1" src="https://user-images.githubusercontent.com/512300/178753734-bbee8712-cf83-4846-bba7-3b619b97b4d2.png">

<br>After the game starts, it will take you to the board to play a game against the AI. After the game is finished, it will take you back to the start menu.
<img width="926" alt="tictactoe2" src="https://user-images.githubusercontent.com/512300/178754070-ea3862e1-7db5-46dc-b73b-ed3a0d171742.png">

# Details and Configuration
TicTacToe is configurable via scriptable objects. <br>
Board data is separate from rendering and any renderer that implements IBoardRenderer interface can display the board details. For the sake of simplicity 2 different renderer are already implemented - debug renderer and 2d game board renderer. <br>
Initial board data has also been made configurable. Since Unity's default int[,] has issue with Editor window display, the board scriptable objects are devided into rows and board accepts an array of rows. Not, the number of rows need to match the size of the board which is configurable too inside BoardData class. 
Note, you can change the scriptable objects at runtime and board should adapt to that change on next game as long as the size of the board has not changed.

# Architecture 
TBA
 
