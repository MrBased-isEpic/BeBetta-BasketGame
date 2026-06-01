Play the game in project by
- Opening the MasterScene.
- Pressing play

WebGL build provided in project. Also play in Itch.io here:
https://sri-kumaran.itch.io/bebetta-basketgame?secret=muLufiwSgB3VWbn8AaAa3deMP80

Design Patterns used:
  One major pattern used in the Strategy pattern. The GameManager class employs several states relating to the distinct stages in gameplay.

Improvements to be made:
  - More UI animations could be made. My framework for UI "pages" allows easy insertion for entry and exit animations. Now it's set to the default Fade-In Fade-Out animation.
  - Particle effects. This can be easily implemented with sprite animations. Though it would be challenging since the game is built in the uGUI framework.

AI usage in this project:
  Was only used to generate a boilerplate stateMachine system for the GameManager.
