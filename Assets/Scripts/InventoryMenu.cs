using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 1. Take care of our todos for inventory object data structure
/// TextAreaAttribute
/// Note icon. sprites folder, sprite import settings
/// Look at our mockup. Build the menu in unity
/// Create new Inventory Canvas. Rename and hide the HUD Canvas.
/// Setup the canvas scaler
/// Setup anchors for perecents
/// Delete the background
/// Take a screenshot of the mockup and put it in PS to get colors
/// Create left and right column empty game objects, add horiz layout group to panel
///layout group controls size
/// Add the main items: left scroll view, exit button, right scroll view
/// Size the buttons and scroll views, add vertical layout groups
/// Rename stuff
/// Setup Inventory List scroll view
///     Add four buttons
///     Add a grid layout group to content area
///     adjust spacing, cell size
///     Add a bunch more buttons to test scrolling. Disable fps controller
///     Delete horiz bar, disable horiz scroll
///     Disable image
/// Setup button
///    Replace with toggle
///    Delete label, background image
///    Set up normal/highlight/pressed colors
///    Figure out "selected" image border width by adjusting width of checkmark
///    Create 32x32 transparent image in PS, stroke inside 8 px white
///    9-slice "full rect" sprite editor. Demo before and after
///    Replace checkmark with new box outline
///    Create prefab!
/// Test. What's wrong? Create toggle group. Rename to Content Toggle Group
///     Uh oh. Can't apply this change to prefab. Why? Doesn't matter, we have to spawn all this dynamically anyway.
///     Add image. apply to prefab
/// Fix our button
///     Delete image. Change color. 
///     Change font (google fonts, fonts folder) bold size 28
/// Fix text box (right column)
///     change scroll view color to full opacity black
///     Remove horizontal scroll
///     Add UI text as component of content area. Add Lorem ipsum. Add content size fitter.
/// Fix scroll view art
///     Delete images. Use colors from scheme.
/// Add "the item" label. This took finicking with the vertical layout group. Save early, save often. Make commits!
/// </summary>
public class InventoryMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
