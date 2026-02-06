# Endless Runner - Unity 3D

This project is a **3D endless runner game** inspired by the classic **Pepsi Man**.  
The player automatically runs through a **three-lane path**, dodging randomly generated obstacles while trying to achieve the highest possible score based on **distance traveled**.

---

## Controls

| Key                        | Action                          |
| -------------------------- | ------------------------------- |
| **A** or **← Left Arrow**  | Move one lane to the left       |
| **D** or **→ Right Arrow** | Move one lane to the right      |
| _(Automatic)_              | Player moves forward constantly |

---

## Key Features

- **Procedural Generation:**  
  Dynamically spawns path tiles.

- **Safe Start:**  
  The first tile always spawns without obstacles to give players time to prepare.

- **Randomized Obstacles:**  
  Obstacles appear in random lanes with variable spacing for engaging gameplay.
  There are two diffrent kind of obstacle.

- **High Score System:**  
  Uses `PlayerPrefs` to save and display your best distance across sessions.

- **Physics-Based Death:**  
  On collision, the player trips and falls using Unity’s physics before transitioning to the Game Over screen.

---

## Development Reflection

During development, the biggest challenge was balancing **fair procedural spawning** and gameplay flow.

- **Obstacle Spacing:**  
  Used variables like `zInterval` and `rowSpawnChance` to prevent overly tight clustering of obstacles.

- **Data Persistence:**  
  Learned to use `PlayerPrefs` for storing and retrieving the “Best Record” between sessions.

- **Physics:**  
  Switched the player from kinematic to fully physics-simulated upon death for realistic fall behavior.
