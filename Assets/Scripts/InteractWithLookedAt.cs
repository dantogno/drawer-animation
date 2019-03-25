using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects when the player presses the Interact button while looking at an IInteractive,
/// and then calls that IInteractive's InteractWith method.
/// </summary>
public class InteractWithLookedAt : MonoBehaviour
{
    private IInteractive lookedAtInteractive;
    private void Update()
    {
        //if (Input.GetButtonDown("Interact"))
        //    Debug.Log("Pressed Interact");

        if (Input.GetButtonDown("Interact") && lookedAtInteractive != null)
        {
            lookedAtInteractive.InteractWith();
        }
    }

    /// <summary>
    /// Event handler for DetectLookedAtInteractive.LookedAtInteractiveChanged.
    /// </summary>
    /// <param name="newLookedAtInteractive">Reference to the new IInteractive the player is looking at.</param>
    private void OnLookedAtInteractiveChanged(IInteractive newLookedAtInteractive)
    {
        lookedAtInteractive = newLookedAtInteractive;
    }


    #region Event subscription / unsubscription
    private void OnEnable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged += OnLookedAtInteractiveChanged;
    }
    private void OnDisable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged -= OnLookedAtInteractiveChanged;
    }
    #endregion
}
