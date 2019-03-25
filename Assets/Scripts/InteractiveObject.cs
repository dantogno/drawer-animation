using System;
using UnityEngine;
/// <summary>
/// A class for testing interactive objects.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class InteractiveObject : MonoBehaviour, IInteractive
{
    [Tooltip("Text that will display in the UI when the player looks at this object in the world.")]
    [SerializeField]
    protected string displayText = nameof(InteractiveObject);

    public virtual string DisplayText => displayText;

    protected AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays a sound effect when the player interacts with it.
    /// </summary>
    public virtual void InteractWith()
    {
        Debug.Log($"{gameObject.name} was just interacted with!");
        audioSource.Play();
    }
}
