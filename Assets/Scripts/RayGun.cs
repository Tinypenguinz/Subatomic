using UnityEngine;
using UnityEngine.InputSystem;

public class RayGun : MonoBehaviour
{

    public InputActionReference triggerAction;
    public LayerMask interactableLayer;
    private ShrinkableObject selectedShrinkableObject; // Remember Shrinkable object holds all shrink/grow/neutral logic., this stores the currently selected, hense why private
    
    private enum ActionState // i love enums.
    {
        Shrink,
        Grow,
        Neutral
    }

    private ActionState currentAction = ActionState.Neutral; // reset

    void Update()
    {
        // Check if the trigger button is pressed
        if (triggerAction.action.triggered)
        {
            // Output to the console when the trigger is pressed
            Debug.Log("Trigger pressed!");
            PerformRaycast();

            if (selectedShrinkableObject != null)
            {
                ShrinkGrow();
            }
            else
            {
                Debug.LogError("No ShrinkableObject selected");
            }

        }
    }
    void PerformRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        // If the raycast hits an interactable object with the shrinkable component 
        if (Physics.Raycast(ray, out hit, 10f, interactableLayer))
        {
            ShrinkableObject hitShrinkableObject = hit.collider.GetComponent<ShrinkableObject>();

            if (hitShrinkableObject != null)
            {
                selectedShrinkableObject = hitShrinkableObject;
                Debug.Log("ShrinkableObject selected"+ selectedShrinkableObject.gameObject.name);
            }
            else   
            {
                selectedShrinkableObject = null;
                 Debug.Log("No ShrinkableObject selected.");
            }
        }
    }
// split this incase i need to edit this.
    void ShrinkGrow()
    {   
        if (selectedShrinkableObject == null)
        {
            Debug.LogError("No Shrinkable Object Selected :(");
            return;
        }
        // action based on currentAction state. should current aciton be above switch for ease of reading?
        switch (currentAction)
        {

            case ActionState.Shrink:
                selectedShrinkableObject.Shrink();
                break;
            case ActionState.Grow:
                selectedShrinkableObject.Grow();
                break;
            case ActionState.Neutral:
                selectedShrinkableObject.Neutral();
                break;

        }
        currentAction = (ActionState)(((int)currentAction + 1) % 3);
        Debug.Log("raygun function called");
    }
}
