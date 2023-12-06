# Guardian
Guardian is the game I made for my third-year project developed in Unity engine.
Here you will be able to view the code for the game. 

Link for  more information on the game 
https://ryry199316.wixsite.com/ryan-hughes/third-year-project

see below is how to view different scripts
## player

## input manager

## Ai state machine
The npc ai uses a state machine that changes the behavior of enemies depending on where the player is. 

- Roam: The npc will move around a set area in the game.
- Chase: When the player is within a set distance the Npc will head toward the player's position.
- Attack: When close to the player, the npc will attack them and this will play an animation at random between 1 and 3. 

code location: Guardains/Npc/AiStateMachine.cs

code link: https://github.com/RH-Games/Guardains/blob/main/Npc/AiStateMachine.cs

## npc
