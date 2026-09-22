# What it is

Welcome to **Connect 4 - Gravity Blockers!**

This is a variant of the original Connect 4 built as a CLI application using C#. The additional rules are:
- The board can be rotated left/right
- Each player has 3 moves for their turn
- First 2 moves are normal, but the 3rd is a "BLOCK" piece
- "BLOCKS" can only be placed adjacent to any existing piece
- Normal Connect 4 win conditions apply

Players can either play against each other or the built in Robot. 

# How to run it

To open the application:

- Navigate to **EAS-Project\Gravity-Blockers**
- Open terminal and type "**dotnet run**" (ensure latest version of dotnet-sdk is installed)

To run unit tests:

- Navigate to **EAS-Project\Gravity-Blockers.Tests**
- Open terminal and type "**dotnet test**"

To play the game:

- Open the application
- Enter both players' names on request
- Type in "ROBOT" for one or both names if you wish to play against the built in bot
- Follow the instructions


# How it works

This section has been split into 3 parts for simplicity

**Section 1 - Setup:**
- The games opening point is "Program.cs", where the initial welcome message is given
- The Player Object is then created, prompting the user to enter a valid name
- The Grid Object is created and shown, with preset values for the dimensions
- The Game begins

**Section 2 - Gameplay:**
- As each player attempts to make a move, logic checks if the move is valid
- If valid, the Grid is updated accordingly
- If a win/draw condition is detected, the game finishes
- Messages in the console are given after every update

**Section 3 - Robot:**
- The Robot is accessed by typing "ROBOT" when prompted for the player name
- The user can type "ROBOT" for both names, and it will be bot vs bot
- The Robot prioritises winning - if it can win this round, it goes for it
- The next priority is stopping the opponent from winning
- The third priority is building existing pieces
- The fourth priority is do a random move

# Motivation


My very first independant project was a study app written in TypeScript which used an API to fetch, clean, and output data according to user inputs. I have done something similar during my University Dissertation project, where I retrieved variants of Sudoku puzzles and applied different algorithms to see which was most efficient. This reminded me of the numerous grid-style games I used to create on paper as a child and often play with my friends. One such game is a variant of Connect 4, which I like to call **"Connect 4 - Gravity Blockers"**. I have decided to create this game for this project


# Tools & Technology

To give myself a challenge, I will be using C#. I have not learnt this language in any of my school subjects or University modules, so at this stage I do not know how to even output "Hello World". Since I am familar with Python and JavaScript however, I am hoping I can use the conceptual knowledge to translate into C#, and use online platforms such as **W3Schools** to learn the required syntax

I will be creating a **Command Line Application** to keep things simple. There is possibility of extension for a GUI, although this is highly unlikely for this projects scope


# Improvements

After conducting many tests, the built in Robot **does not work as intended**. It appears te Robot is unable to rotate the Grid, and often causes IndexOutOfBounds error. With more time, this could be fixed.

Another major improvement is adding Unit tests - I have added a simple one to see if I am able to. Since C# is new to me, this was a major step. I can now easily add Unit tests for all remaining functionality with more time, including:

- Check correct win/draw condition
- Check valid piece/block placement
- Check board rotates right/left

Please note that all of these have been checked manually and do work to the best of my knowlegde

Other than these, please refer to the "RESEARCH.md" file for more details on improvements I could make


# Afterthoughts

After having completed this, I realised that C# is not as scary as I thought! It is very similar to Java in terms of layout, so I was able to pick it up quite easily. Initially I was struggling with simple tasks such as printing to terminal, calling a function, installing dependencies etc. As the program kept building (especially the adding Robot logic), it became increasingly more difficult to keep track of which classes/methods interact with what

This programme works perfectly well if 2 players are playing, and it is definitely very fun once you get the hang of it! The biggest challenge now however is getting the robot to work. My motivation for building a Robot was to test how far I can go, and since I was not able to successfully at this stage, I may have reached my limits. Please refer to the bottom section of "AI.md" for more information



