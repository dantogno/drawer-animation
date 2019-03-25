using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detect IInteractives the player looks at.
/// Tutorial walkthrough:
/// -1. Take a peak at the GDD!
/// 0. Setup the test scene with floor and cube as mock interactive object
/// 1. Look at https://docs.unity3d.com/ScriptReference/Physics.Raycast.html and create variables for parameters.
/// 2. Debug.DrawRay
/// 3. Put on player and test (game view vs scene view; maybe use orthographic view)
/// 4. setup real raycast, explain out variable, print what we're looking at. Test!
/// 5. I'm not going to deal with the layermask unless it becomes an issue. Not sure why it's not an issue.
/// 6. Add reticle for easier testing. UI Text with outline component. Centered. Overflow.
/// 7. Start creating IInteractive interface.
///     - suggest some videos. Tell them to optionally pause here and review: inheritance, polymorphism, interfaces.
///     - Discuss difference between interface and inheritance, explain why interface (crate example)
/// 8. Create IInteractive.cs in Interfaces folder. It has void InteractWith()
/// 9. Create InteractiveObject.cs as a test interactive object, implement interface with printing name
/// 10. Detect when player is actually looking at an IInteractive and call InteractWith
/// 11. We don't want to always activate the looked at object. We just want this script to DETECT them. Component pattern
/// 12. Create a class InteractWithLookedAt. It'll handle calling interact with, but let's just focus on detecting first.
/// 13. Create a variable for lookedAtInteractive
/// 14. Refactor code in fixed update to update the looked at interactive object variable
/// 15. Add  reference of DetectLookedAtInteractive to InteractWithLookedAt. Discuss making lookedAtInteractive
/// public. Introduce Properties.
/// 16. Add Interact input.
/// 17. Add input / interactWith logic to InteractWithLookedAt
/// 18. Add InteractWithLookedAT component to player, plug in reference to DetectLookedAt in editor. Test.
/// 19. Start the text! Just create the UI Text object in the editor first. 
/// 20. Create a script for the text. LookedAtInteractiveDisplayText
/// 21. Discuss coupling. Introduce observer pattern.
/// 22. Add event to LookedAtInteractive
/// 23. Change LookedAtInteractive property to invoke event (if it changes)
/// 24. Subscribe to event in InteractWithLookedAtObjects, remove reference there to DetectLookedAtInteractive. Test in game.
/// 25. Add event subscription in DisplayText script.
/// 26. Add reference to text component, initialize in Awake
/// 27. Start implementing update text function, but need to add display text property to our interface.
/// 28. Be sure to call UpdateDisplayText in event handler, and add script to gameobject in editor.
///     Also, we must initialize the text (because it doesn't change until we look at something for the first time) Then test!
/// 29. Add display text serialize field.
/// 30. Add sound effects when player triggers interactive object.
/// 31. Add audiosource, initialize in awake--no playonawake (not fast enough in code), require component
/// 32. DL sfx (use the one from the gdd) https://freesound.org/people/tbrook/sounds/348224/
/// 33. Add a new folder for it, put it in project, assign it
/// 34. Make InteractiveObject a prefab
/// 35. Make a script for turning stuff on or off: ToggleSetActive
/// 36. serialize field object to toggle
/// 37. virtual / override, base.InteractWith
/// 38. bool isResusable
/// 39. if not reusable, remove display text (requires making it protected)
/// 30. Uh oh, the display text won't update unless we look away and back.
///     It's because we're only raising the event when the actual looked at item is new.
///     We need to also raise it when we're looking at the same object, but it's now got different text.
/// 31. Add lastDisplayText variable. MAAYBE explain reference vs value types...
/// 32. If the text changes, or the IInteractive itself changes, raise the event.
/// </summary>
public class DetectLookedAtInteractive : MonoBehaviour
{
    [SerializeField]
    private Transform raycastOrigin;

    [Tooltip("How far from the raycastOrigin we will search for interactive elements.")]
    [SerializeField]
    private float maxRange = 5.0f;

    /// <summary>
    /// Event raised when the player looks at a different IInteractive.
    /// </summary>
    public static event Action<IInteractive> LookedAtInteractiveChanged;

    /// <summary>
    /// The currently looked at IInteractive. This property raises the LookedAtInteractiveChanged event
    /// when the player looks at something different, or if the display text of the currently looked at interactive
    /// changes dynamically.
    /// </summary>
    private IInteractive LookedAtInteractive
    {
        get => lookedAtInteractive;
        set
        {
            bool isInteractiveChanged = value != lookedAtInteractive;
            bool isTextChanged = lastDisplayText != value?.DisplayText;

            if (isInteractiveChanged)
            {
                lookedAtInteractive = value;
                lastDisplayText = lookedAtInteractive?.DisplayText;
            }

            if (isInteractiveChanged || isTextChanged)
                LookedAtInteractiveChanged?.Invoke(lookedAtInteractive);
        }
    }
    private IInteractive lookedAtInteractive;
    private string lastDisplayText;

    private void FixedUpdate()
    {
        LookedAtInteractive = GetLookedAtInteractive();
        // Debug.Log($"Looking at: {LookedAtInteractive}");
    }

    /// <summary>
    /// Raycast forward from the camera to look for IInteractives.
    /// </summary>
    /// <returns>The first IInteractive detected, or null if none are found.</returns>
    private IInteractive GetLookedAtInteractive()
    {
        IInteractive interactive = null;
        Vector3 direction = raycastOrigin.forward;
        RaycastHit raycastHit;
        // Debug.DrawRay(raycastOrigin.position, direction * maxRange, Color.red);
        if (Physics.Raycast(raycastOrigin.position, direction, out raycastHit, maxRange))
        {
            interactive = raycastHit.collider.GetComponent<IInteractive>();
        }
        return interactive;
    }
}