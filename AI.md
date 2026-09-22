**Initial use**

As the programme expanded, my C# knowledge and the complexity of the logic also expanded. Initially, I was using AI for basic commands such as printing to console, getting user input, creating methods etc. I knew how to do these conceptually, but did not know the correct C# syntax

**As program developed**

Some functions such as "rotate board" I had never done before in any langauge, so AI helped me generate the correct code. I manually tested by dropping a few pieces in, then rotating the board and checking if the dimensions and pieces are still correct. This method was followed for many functions. Some functions I knew the logic of what to do, and just asked AI to do this specifically to speed up the process. Others I knew conceptually what I wanted so simply described that and tested out the end result

Often times the AI would assume certain things, for example I asked it to create a function to drop the piece, and specified exactly what the parameters are and what the logic should be. I made it clear that the piece must be dropped, then the board must be rotated, and the piece dropped again. The AI, presumably using knowledge from classic Connect4, did not rotate the board. I double checked the produced code and realised the mistake before implementing it into the final solution

**Final use**

The final implemention of my project was to build the Robot. Conceptually, I understand exactly what I am trying to do. However, because of the structure of the project, I was not able to manually test out each new component of the Robot after creating it. As a result, I fell into the loop of asking AI to create a function, and mentally keeping track of it, then creating a bunch more which rely on it, with many parameters passing through each. I eventually reached a point where I was unable to logically determine if the generated code was correct as it required too many back and forths with other functions

As a direct result, the Robot now has errors and I have no idea where to begin looking or why its not working as expected