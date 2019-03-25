using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. Expand our test level a bit for larger workspace
/// 2. Use probuilder to create door frame geo
/// 3. Create door geo. Half of the door frame depth
/// 4. Vertex colors!
/// 5. Position door in frame
/// 6. Test rotating door. We need to change it's pivot.
/// 7. Door needs to be thin enough to not clip door frame. 
/// 8. Save in a new door scene to avoid conflicts :)
/// Add animator to door
/// Rename game objects. Child door to door frame
/// Click "create." Create new Animation folder. Save in there. "Door_Opening" Naming conventions?
/// First do a keyframe open animation.
/// Explain add property, then record button
/// Explain forward direction (blue arrow)
/// Fix looping
/// Fix play on awake by setting up animation controller.
/// Add Door_Closed animation clip.
/// Make Door_Closed default state. Test.
/// Make Closed > Open transition in the animator.
/// It'll open automatically. Make a shouldOpen bool anim param. Test with game running checking the bool.
/// Control the shouldOpen bool in script. Make a Door.cs
/// Add reference to animator, initialize in start. Require component
/// When do we want to make the transition? Make Door an InteractiveObject
/// Constructor for changing displayText
/// Override InteractWith, setbool
/// Fix timing / delay
/// Add sound effect https://freesound.org/people/mhtaylor67/sounds/126041/ 
/// Our door can only open. Add bool isOpen and only do the InteractWith stuff if it's false
/// String to hash
/// Mention animation events
/// Mention state machine behaviours
/// One frame door animation
/// </summary>
[RequireComponent(typeof(Animator))]
public class Door : InteractiveObject
{
    [Header("Locked door settings")]
    [Tooltip("Setting a key here will lock a door. If a player has the key in their inventory, they can open the locked door.")]
    [SerializeField]
    private InventoryObject key;

    [Tooltip("If this is checked, the associated key will be removed from the player's inventory when the door is unlocked.")]
    [SerializeField]
    private bool consumesKey;

    [Tooltip("The text that displays when the player looks at the door while it's locked.")]
    [SerializeField]
    private string lockedDisplayText = "Locked";


    [Tooltip("Play this audio clip when the player interacts with a locked door without owning the key")]
    [SerializeField]
    private AudioClip lockedAudioClip;

    [SerializeField]
    [Tooltip("Play this audio clip when the player opens the door.")]
    private AudioClip openAudioClip;

    /// <summary>
    /// Display text varies based on whether the door is locked, 
    /// or the door is locked and the player has the key.
    /// If the door is open, the base behavior uses an empty string to effectively hide it.
    /// </summary>
    public override string DisplayText
    {
        get
        {
            string toReturn;
            if (isLocked)
            {
                toReturn = HasKey ? $"Use {key.ObjectName}" : lockedDisplayText;
            }
            else
                toReturn = base.DisplayText;

            return toReturn;
        }
    }
    private bool HasKey => PlayerInventory.InventoryObjects.Contains(key);
    private Animator animator;
    private bool isLocked;
    private bool isOpen;
    private int shouldOpenAnimParam = Animator.StringToHash("shouldOpen");
    /// <summary>
    /// Constructor for initializing the displayText in the editor.
    /// </summary>
    public Door()
    {
        displayText = nameof(Door);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        InitializeIsLocked();
    }

    private void InitializeIsLocked()
    {
        if (key != null)
            isLocked = true;
    }

    public override void InteractWith()
    {
        if (!isOpen)
        {
            if (isLocked && !HasKey)
            {
                audioSource.clip = lockedAudioClip;
            }
            else // if it's not locked, or if it's locked and we have the key...
            {
                audioSource.clip = openAudioClip;
                animator.SetBool(shouldOpenAnimParam, true);
                displayText = string.Empty;
                isOpen = true;
                UnlockDoor();
            }
            base.InteractWith(); // will play the audio source
        }
    }

    private void UnlockDoor()
    {
        isLocked = false;
        if (consumesKey)
            PlayerInventory.InventoryObjects.Remove(key);
    }
}