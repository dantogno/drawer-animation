using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This UI text displays info about the currently looked at interactive element.
/// The text should be hidden if the player is not currently looking at an interactive element.
/// </summary>
public class LookedAtInteractiveDisplayText : MonoBehaviour
{
    private IInteractive lookedAtObject;
    private Text displayText;

    private void Awake()
    {
        displayText = GetComponent<Text>();
        displayText.text = string.Empty;
    }

    /// <summary>
    /// Update display text based on currently looked at object.
    /// </summary>
    private void UpdateDisplayText()
    {
        if (lookedAtObject != null)
            displayText.text = lookedAtObject.DisplayText;
        else
            displayText.text = string.Empty;
    }

    /// <summary>
    /// Event handler for DetectLookedAtInteractiveObjects.LookedAtInteractiveObjectChanged.
    /// </summary>
    /// <param name="newLookedAtObject">Reference to the new object the player is looking at.</param>
    private void OnLookedAtInteractiveObjectChanged(IInteractive newLookedAtObject)
    {
        lookedAtObject = newLookedAtObject;
        UpdateDisplayText();
    }
    #region Event subscription / unsubscription
    private void OnEnable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged += OnLookedAtInteractiveObjectChanged;
    }
    private void OnDisable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged -= OnLookedAtInteractiveObjectChanged;
    }
    #endregion
}
