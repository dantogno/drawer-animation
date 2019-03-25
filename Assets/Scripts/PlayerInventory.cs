using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for storing the player inventory as a public static list.
/// Inventory objects' InteractWith will add to the list.
/// This isn't a Monobehaviour, so we don't need to (cannot) put it in our scenes.
/// </summary>
public class PlayerInventory
{
    public static List<InventoryObject> InventoryObjects { get; } = new List<InventoryObject>();
}