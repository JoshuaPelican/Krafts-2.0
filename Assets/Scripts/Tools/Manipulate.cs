using UnityEngine;
using UnityEngine.Events;

public class Manipulate : Tool
{
    public static UnityAction<GameObject> OnDragStart;
    public static UnityAction<GameObject> OnDragEnd;

    public static bool isHoldingObject;
    public static GameObject currentlyHeldObject;
    Vector2 pickupOffset;

    private void FixedUpdate()
    {
        //TODO: Find a way to make this use the preview instead to be consistent!!! Use the events!!!

        //If an object is being held, move it to the mouse position (Only the X and Y; Z is depth and handled in sorting only)
        if (currentlyHeldObject)
        {
            currentlyHeldObject.transform.position = new Vector3(InputUtility.MousePosition.x + pickupOffset.x, InputUtility.MousePosition.y + pickupOffset.y, currentlyHeldObject.transform.position.z);
        }
    }

    public override void LeftMouseDown()
    {
        //Get interactible if any
        GameObject interactable = InputUtility.GetClickedObject();

        //If there is one
        if (interactable != null)
        {
            PickupObject(interactable);
        }
        //If there is not one
    }

    //Tells what to do when the mouse is released
    public override void LeftMouseUp()
    {
        DropHeldObject();
    }

    //Sets the currently held object and initializes a click offset
    //Sorts the newly picked up object on top
    public void PickupObject(GameObject gameObject)
    {
        currentlyHeldObject = gameObject;

        isHoldingObject = true;
        OnDragStart?.Invoke(currentlyHeldObject);

        pickupOffset = (Vector2)currentlyHeldObject.transform.position - InputUtility.MousePosition;
    }

    //Removes the currently held object
    public void DropHeldObject()
    {
        OnDragEnd?.Invoke(currentlyHeldObject);

        currentlyHeldObject = null;
        isHoldingObject = false;
    }
}
