using UnityEngine;
using UnityEngine.Events;

public class Manipulate : Tool
{
    public static UnityAction<GameObject, Vector2> OnPickup;
    public static UnityAction<GameObject> OnDrop;

    public static GameObject currentlyHeldObject;
    Vector2 pickupOffset;

    public static float currentDepth;

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

        SortOnTop(currentlyHeldObject);

        OnPickup?.Invoke(currentlyHeldObject, pickupOffset);
    }

    //Removes the currently held object
    public void DropHeldObject()
    {
        currentlyHeldObject.transform.position = new Vector3(InputUtility.MousePosition.x + pickupOffset.x, InputUtility.MousePosition.y + pickupOffset.y, currentlyHeldObject.transform.position.z);


        OnDrop?.Invoke(currentlyHeldObject);

        currentlyHeldObject = null;
    }

    //Gives an object a lower Z value to render it on top
    void SortOnTop(GameObject gameObject)
    {
        currentDepth -= 0.01f;

        Vector3 thisObjectPosition = gameObject.transform.position;
        thisObjectPosition.z = currentDepth;
        gameObject.transform.position = thisObjectPosition;
    }
}
