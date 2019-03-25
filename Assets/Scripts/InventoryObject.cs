using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Outline:
/// Add bool isLocked to door
/// Add lockedDisplayText, make DisplayText virtual and override it, ternary operator
/// Add locked audio clip. Make audioSource protected. Use the correct audio clip if it's locked or unlocked
/// Create the InventoryObject script. It inherits InteractiveObject
/// demo ToDo for long description and icon
/// Create the PlayerInventory script with static InventoryObjects readonly property
/// Handle InteractiveObject.InteractWith.  Need refs to collider and renderer
/// Add constructor for initializing display text
/// Add bag sound clip
/// Add the thing to the inventory list. debug.log test
/// Need a separate object name vs display text. Because, I want it to say "take: {objectName}"
/// Clarify my tooltips.
/// How do we know if the player has the key? How do we designate a key? Consume the key?
/// Serialize Field Key on Door. Replace "locked" with it
/// Create InitializeIsLocked method, call in Start
/// Create HasKey readonly private property based on if inventory contains key
/// Change Door InteractWith to open locked door if they have the key
/// Change DisplayText to read differently if the player has the key
/// 
/// </summary>
public class InventoryObject : InteractiveObject
{
    [Tooltip("The name of the object, as it will appear in the inventory menu UI")]
    [SerializeField]
    private string objectName;

    [Tooltip("The text that will display when the player selects this object in the inventory menu.")]
    [TextArea(3, 8)]
    [SerializeField]
    private string description;

    [Tooltip("Icon to display for this item in the inventory menu.")]
    [SerializeField]
    Sprite icon;
    // TODO: Add icon field
    private MeshRenderer meshRenderer;
    // There is an old (deprecated?) variable named "collider," 
    // so we have to use the "new" keyword to hide it.
    private new Collider collider;

    public string ObjectName => objectName;

    public InventoryObject()
    {
        objectName = nameof(InventoryObject);
        displayText = $"Take {objectName}";
    }

    private void Start()
    {        
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    /// <summary>
    /// When the player interacts with an InventoryObject:
    /// Disable renderer and collider to effectively remove object from scene.
    /// Add object to PlayerInventory.InventoryObjects.
    /// </summary>
    public override void InteractWith()
    {
        base.InteractWith();
        if (meshRenderer != null)
            meshRenderer.enabled = false;
        collider.enabled = false;
        PlayerInventory.InventoryObjects.Add(this);
        Debug.Log($"PlayerInventory count: {PlayerInventory.InventoryObjects.Count}");
    }
}