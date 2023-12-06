# Guardian
Guardian is the game I made for my third-year project developed in Unity engine.
Here you will be able to view the code for the game. 

Link for  more information on the game 
https://ryry199316.wixsite.com/ryan-hughes/third-year-project

see below is how to view key scripts to the game.
I highlighted different scripts into sections with small descriptions and file locations on how to find them; as well as a link for easier access.

## player script

For the player, there are several different scripts for different functions

Player Script location: Guardains/Player/

## PlayerMovement
has all the player movements functions like walking, running, and rotating

code location: Player/PlayerMovement.cs
code link: https://github.com/RH-Games/Guardains/blob/main/Player/PlayerMovement.cs

## input manager

input manager contacts input mapping from the player and functions to call when the button is pressed. 
- movement input: to move the player and camera
- attack input: used for attack inputs
- block input: used to block incoming attacks
- action input: to bring up the quest goals UI menu
- WeaponHolster: changes what weapons are equipment
- Collision: Collisions for the weapon collision boxes
  - Collision start & Collision end
  - Collision block
- Inputs: all input functions are stored and called from input when that is called
      

code location: BreadcrumbsGuardains/Player/InputManager.cs
code link: BreadcrumbsGuardains/Player/InputManager.cs

## Ai state machine script
The npc ai uses a state machine that changes the behavior of enemies depending on where the player is. 

- Roam: The npc will move around a set area in the game.
- Chase: When the player is within a set distance the Npc will head toward the player's position.
- Attack: When close to the player, the npc will attack them and this will play an animation at random between 1 and 3. 

code location: Guardains/Npc/AiStateMachine.cs

code link: https://github.com/RH-Games/Guardains/blob/main/Npc/AiStateMachine.cs

## Npc script

NPC enemy script is used for the non-playable characters in the game this allows information about the NPC to be stored and accessed.

- damage to the npc including death function when npc Health is zero.
  - Damage -> line 95
  - Npc Death -> line 113
    
- weapon spawn and putting them in the correct slots on the npc.
  - Npc Spawn Weapon -> line 147
    
- Changing weapon position to correct slots when different animation parameters are true.
    - Npc Weapon Holster -> line 166
      
- Npc sensors to detect the player's position.

- Npc colliders for the weapons used to turn on the collider and off during animations.
  - Npc Collision start -> line 225
  - Npc Collision end -> line 243

code location: Guardains/Npc/NpcEnemy.cs

code link: https://github.com/RH-Games/Guardains/blob/main/Npc/NpcEnemy.cs

## User interface programming script

The user interface is made up of these scripts 

- Quest Goal: sets up the quest goal and checks when it has been reached
- Quest Handler: used to show the Quest information and update when an npc has been killed
- Main menu: functions call the right menus and button functions
- Healthbar: is used to set and update the player health which is linked to a UI slider -> code located in Guardains/Player/HealthBar.cs
code location: Guardains/Ui/
code link: https://github.com/RH-Games/Guardains/tree/main/Ui

## Weapon Collider

Weapon Collider script is called when an animation is played to check if it hit an enemy. 

code location Guardains/Items/WeaponCollider.cs

code link: https://github.com/RH-Games/Guardains/blob/main/Items/WeaponCollider.cs

### Npc Weapon Collider

This is the collider for npc weapons for when they hit the player

code location: Guardains/Npc/NpcWeaponCollider.cs

code link: https://github.com/RH-Games/Guardains/blob/main/Npc/NpcWeaponCollider.cs


