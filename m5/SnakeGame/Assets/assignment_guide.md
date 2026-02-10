# Snake Game Assignment Guide

This guide outlines the steps to complete the assignment mentioned in `tutorial.md`. You will add a spike obstacle that increases in number with each level and kills the snake upon impact. You will also add a generic "Assignment by" text to the UI.

## Part 1: Create the Spike Script and Prefab

1.  **Create the Spike Script:**
    *   In the **Project** window, navigate to `Assets > Scripts`.
    *   Right-click and select `Create > C# Script`.
    *   Name it `Spike`.
    *   Open `Spike.cs` and replace the contents with the following code (it's a simple marker component like `Egg.cs`):
        ```csharp
        using UnityEngine;

        public class Spike : MonoBehaviour
        {
        }
        ```

2.  **Create the Spike Prefab:**
    *   In the **Project** window, navigate to `Assets > Art`.
    *   Find a sprite to use for the spike (or import a new one). If you don't have one, you can use an existing rock or egg sprite temporarily, or create a simple triangle sprite.
    *   Drag the sprite into the **Scene** view to create a new GameObject.
    *   Rename the GameObject to `Spike`.
    *   In the **Inspector**:
        *   Add a **Box Collider 2D** (or Polygon Collider 2D) component. **Important:** Check the `Is Trigger` box.
        *   Add the `Spike` script you just created.
        *   set the z position to -1
    *   Drag the `Spike` GameObject from the **Hierarchy** into the `Assets > Prefabs` folder to create a prefab.
    *   Delete the `Spike` GameObject from the scene.

## Part 2: Update the GameController Script

You need to managing spawning and removing spikes.

1.  Open `Assets/Scripts/GameController.cs`.

2.  **Add Variables:**
    Inside the `GameController` class, add variables to hold the spike prefab and the list of active spikes:
    ```csharp
    public GameObject spikePrefab = null;
    public List<Spike> spikes = new List<Spike>();
    ```

3.  **Create `CreateSpike` Method:**
    Add a new method to spawn a single spike at a random position (similar to `CreateEgg`):
    ```csharp
    void CreateSpike()
    {
        Vector3 position;
        position.x = -width + Random.Range(1f, (width * 2) - 2f);
        position.y = -height + Random.Range(1f, (height * 2) - 2f);
        position.z = -1f;

        Spike spike = Instantiate(spikePrefab, position, Quaternion.identity).GetComponent<Spike>();
        spikes.Add(spike);
    }
    ```

4.  **Create `KillOldSpikes` Method:**
    Add a method to remove all existing spikes:
    ```csharp
    void KillOldSpikes()
    {
        foreach (Spike spike in spikes)
        {
            Destroy(spike.gameObject);
        }
        spikes.Clear();
    }
    ```

5.  **Update `LevelUp` Method:**
    Modify the `LevelUp` method to reset spikes and spawn new ones based on the current level.
    *   Call `KillOldSpikes()` at the start of `LevelUp`.
    *   After calculating functionality for the level, add a loop to create spikes. The tutorial says "Starting at 1 spike for the first level". Level 0 is the start, so `level + 1` works if `level` starts at 0, or just `level` if it starts at 1. Looking at your code, `level` starts at 0 and increments to 1 immediately in `LevelUp`.
    *   Add this logic to `LevelUp`:
        ```csharp
        KillOldSpikes(); // Clear old spikes
        
        // Existing code...
        
        // Spawn spikes for the current level
        // Since level increments at the start of LevelUp (0 -> 1), we can use 'level' directly for 1 spike at level 1.
        for (int i = 0; i < level; i++) 
        {
            CreateSpike();
        }
        ```

6.  **Update `StartGamePlay` Method:**
    Ensure old spikes are cleared when a new game starts.
    *   Add `KillOldSpikes();` inside `StartGamePlay`, near `KillOldEggs();`.

## Part 3: Update the GUI

1.  **Add Text Component:**
    *   In the Unity Editor, go to the **Hierarchy**.
    *   Find your **Canvas** (where `ScoreText`, etc. are located).
    *   Right-click on **Canvas** and select `UI > Text`.
    *   Rename it to `AssignmentText`.
    *   Position it at the bottom of the screen (anchor it to the bottom-center or bottom-left).
    *   Set the **Text** field to "Assignment by [Your Name]".
    *   Adjust font size and color to be visible.

## Part 4: Connect Everything in Editor

1.  **Assign Spike Prefab:**
    *   Select the **GameController** object in the **Hierarchy**.
    *   In the **Inspector**, find the `Spike Prefab` field in the `Game Controller` component.
    *   Drag your `Spike` prefab from the `Assets > Prefabs` folder into this slot.

2.  **Test the Game:**
    *   Press **Play**.
    *   Verify that:
        *   One spike appears when the game starts.
        *   "Assignment by [Your Name]" is visible.
        *   Hitting a spike causes Game Over (this should happen automatically because `SnakeHead.cs` handles non-Egg collisions as death).
        *   When you eat enough eggs to Level Up, the old spike disappears and TWO spikes appear (Level 2).
