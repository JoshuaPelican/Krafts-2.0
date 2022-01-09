using UnityEngine;
using UnityEngine.Events;

public class Manipulate : Tool
{
    public static UnityAction<GameObject, Vector2> OnPickup;
    public static UnityAction<GameObject> OnDrop;

    public static GameObject currentlyHeldObject;
    Vector2 pickupOffset;

    public override void LeftMouseDown()
    {
        //Get interactible
        //If there is one
        if (InputUtility.DidClickObject)
        {
            PickupObject(InputUtility.GetClickedObject());
        }
        //If there is not one
    }

    //Tells what to do when the mouse is released
    public override void LeftMouseUp()
    {
        if(currentlyHeldObject != null)
        {
            DropHeldObject();
        }
    }

    //Sets the currently held object and initializes a click offset
    //Sorts the newly picked up object on top
    public void PickupObject(GameObject gameObject)
    {
        currentlyHeldObject = gameObject;
        pickupOffset = (Vector2)currentlyHeldObject.transform.position - InputUtility.MousePosition;

        OnPickup?.Invoke(currentlyHeldObject, pickupOffset);
    }

    //Removes the currently held object
    public void DropHeldObject()
    {
        Vector3 droppedPosition = new Vector3(InputUtility.MousePosition.x + pickupOffset.x, InputUtility.MousePosition.y + pickupOffset.y, currentlyHeldObject.transform.position.z);
        currentlyHeldObject.transform.position = droppedPosition;

        OnDrop?.Invoke(currentlyHeldObject);

        currentlyHeldObject = null;
    }
}
