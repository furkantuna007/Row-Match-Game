<img width="923" alt="Ekran Resmi 2023-07-31 18 12 00" src="https://github.com/furkantuna007/Row-Match-Game/assets/72814790/111410d8-2ec3-4903-b523-d4c4d9350548"># Row-Match-Game
This is a 3-row match game created using Unity and C#. The game is a classic match-3 where players swap adjacent pieces to form lines of three or more of the same gem type. The game checks for matches both horizontally and vertically. If a match happens, instead of having new gems falling down, a checkmark is coming to the slot. 

Board.cs script controls the overall state of the game board. It manages the board's dimensions and holds references to all the gems on the board. It also controls the spawning and destruction of gems, checks if gems are exploding, and manages the matches and scoring.

BoardLayout.cs script is responsible for the initial setup of the game board. It reads a pre-configured layout and spawns the gems accordingly. If no layout is provided, it generates a random one.

RoundManager.cs controls the round-based game flow. It tracks the remaining moves, updates the UI, checks for game over conditions (based on remaining moves or no more possible matches), stores high scores, and toggles game over screens.

MatchFinder.cs script finds matches of 3 or more gems, both horizontally and vertically, on the board. It also checks if there are any possible matches left, which is useful for determining if the game is over.

Gem.cs controls the behavior of each individual gem. It likely manages gem type, position, matched status, and other gem-specific behaviors.

<img width="923" alt="Ekran Resmi 2023-07-31 18 12 00" src="https://github.com/furkantuna007/Row-Match-Game/assets/72814790/23e0f92e-b3da-4631-9cbc-de158f1a533f">
