# Assignment Guide: Adding the Destruct Card

This guide details the steps to complete the assignment for Merlin's Demon War.

## 1. Code Changes

You need to modify three scripts: `CardData.cs`, `Effect.cs`, and `GameController.cs`.

### A. Modify `CardData.cs`
1.  Open `Assets/Scripts/CardData.cs`.
2.  Inside the `DamageType` enum, add `Destruct` as a new type.
    ```csharp
    public enum DamageType
    {
        Fire,
        Ice,
        Both,
        Destruct // Add this line
    }
    public bool isDestruct = false; // Add this line
    ```

### B. Modify `Effect.cs`
1.  Open `Assets/Scripts/Effect.cs`.
2.  Add a variable for the Boom sound effect at the top with the other AudioSources:
    ```csharp
    public AudioSource destructAudio = null;
    ```
3.  Add a method to play the sound, similar to `PlayIceSound`:
    ```csharp
    internal void PlayDestructSound()
    {
        destructAudio.Play();
    }
    ```

### C. Modify `GameController.cs`
1.  Open `Assets/Scripts/GameController.cs`.
2.  Add a variable for the Destruct Ball image (sprite) where other sprites (like `fireBallImage`) are defined:
    ```csharp
    public Sprite destructBallImage = null;
    ```
3.  In the `CastAttackEffect` method, inside the `switch(card.cardData.damageType)` block, add a case for `Destruct`:
    ```csharp
    case CardData.DamageType.Destruct:
        effect.effectImage.sprite = destructBallImage;
        effect.PlayDestructSound();
    break;
    ```

---

## 2. Unity Editor Steps

After saving your scripts, return to Unity to set up the assets.

### A. Create the Destruct Card Asset
1.  In the Project window, navigate to `Assets/ScriptableObjects` (or wherever you want to save it).
2.  Right-click -> **Create** -> **CardGame** -> **Card**.
3.  Name the new file `Destruct`.
4.  Select the `Destruct` asset and configure specifically:
    *   **Card Title**: `Destruct`
    *   **Description**: `Destroys the enemy made by [Your Name]`
    *   **Cost**: `5`
    *   **Damage**: `9`
    *   **Damage Type**: `Destruct` (This should now be visible in the dropdown)
    *   **Card Image**: Assign the `Destroy` art (from `Assets/Art`).
    *   **Frame Image**: Assign a frame (e.g., `FrameUnique` or similar if available, or standard).
    *   **Number In Deck**: `6` (as per assignment requirements).
    *   **Is Destruct**: Check this box.

### B. Configure GameController
1.  Select the **GameController** object in the Hierarchy.
2.  In the **GameController (Script)** component:
    *   Find the **Destruct Ball Image** field.
    *   Assign the `DestructBall` sprite (from `Assets/Art`).
    *   Find the **Cards** list (likely under "Cards" dropdown).
    *   Click the `+` button to add a new element.
    *   Drag your new `Destruct` CardData asset into the new slot.

### C. Configure Effects
1.  You need to assign the "Boom" sound to the Effect prefabs.
2.  Navigate to `Assets/Prefabs`.
3.  Find the `EffectFromLeft` and `EffectFromRight` prefabs (or similar names).
4.  Open each prefab (double-click).
5.  In the **Effect (Script)** component, you will see the new **Destruct Audio** field.
6.  You may need to add a new **Audio Source** component to the Prefab if there isn't a spare one, or check if the tutorial implies using an existing one. The tutorial mentions "there are now 3 audio sources", so you should:
    *   Add a new **Audio Source** component to the GameObject.
    *   Uncheck "Play On Awake".
    *   Assign the `Boom` clip (from `Assets/Audio`) to this new Audio Source.
    *   Drag this Audio Source component into the **Destruct Audio** slot in the Effect script.
7.  Do this for both `EffectFromLeft` and `EffectFromRight` to ensure it works for both player and enemy.

## 3. Testing
1.  Press **Play**.
2.  Since you added 6 Destruct cards, you should draw one quickly.
3.  Gather 5 Mana.
4.  Use the Destruct card.
5.  Verify:
    *   The "Boom" sound plays.
    *   The "Destruct Ball" graphic (purple fireball) flies.
    *   The enemy takes 9 damage and dies.
