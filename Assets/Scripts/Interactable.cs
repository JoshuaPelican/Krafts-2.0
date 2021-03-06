using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public static float currentDepth;

    public static UnityAction OnInteractableGlued;

    bool isGlued;

    public bool IsGlued
    {
        get { return isGlued; }
    }

    new Collider2D collider2D;
    static ContactFilter2D filter2D = new();

    #region Event Subscriptions

    //Assigning events
    private void OnEnable()
    {
        Manipulate.OnDragStart += Pickup;
        Manipulate.OnDragEnd += Drop;
        Maker.OnMakeObject += Drop;
    }

    //Removing events
    private void OnDisable()
    {
        Manipulate.OnDragStart -= Pickup;
        Manipulate.OnDragEnd -= Drop;
        Maker.OnMakeObject -= Drop;
    }

    #endregion

    private void Awake()
    {
        InitializeComponents();
    }

    //Initializes component references
    void InitializeComponents()
    {
        collider2D = GetComponent<Collider2D>();
    }

    //Does whatever needs to happen if this is picked up
    void Pickup(GameObject gameObject)
    {
        if(gameObject == this.gameObject)
        {
            Debug.Log($"{name} was picked up!");
        }
    }

    //Does whatever needs to happen if this is dropped, such as check if this object is now glued
    void Drop(GameObject gameObject)
    {
        if(gameObject == this.gameObject)
        {
            Debug.Log($"{name} was put down!");

            SortOnTop();

            CheckOverlaps();
        }
    }

    //Gives an object a lower Z value to render it on top
    void SortOnTop()
    {
        currentDepth -= 0.1f;

        Vector3 thisObjectPosition = gameObject.transform.position;
        thisObjectPosition.z = currentDepth;
        gameObject.transform.position = thisObjectPosition;
    }

    //Checks for overlapping glue collisions, and either sticks the object to it, or un-sticks it if no glue is found
    void CheckOverlaps()
    {
        List<Collider2D> overlapping = new();

        collider2D.OverlapCollider(filter2D, overlapping);

        //TODO: Fix this mess, it works but it doesnt look pretty. Glue not being a child of the object to be glued to it is a really good condition!!
        if (overlapping.Count > 0 && overlapping.Any(x => x.CompareTag("Glue") && !x.transform.IsChildOf(transform)))
        {
            isGlued = true;

            OnInteractableGlued?.Invoke();

            transform.parent = overlapping.First(x => x.CompareTag("Glue")).transform;

            //TODO: Fix this too maybe, a little ugly
            Vector3 thisObjectPosition = gameObject.transform.localPosition;
            thisObjectPosition.z = transform.parent.position.z - 0.01f;
            gameObject.transform.localPosition = thisObjectPosition;
        }
        else
        {
            isGlued = false;
            transform.parent = null;
        }
    }
}
