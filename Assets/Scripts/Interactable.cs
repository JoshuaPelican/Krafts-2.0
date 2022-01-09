using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public static UnityAction OnStuck;

    bool isStuck;

    public bool IsStuck
    {
        get { return isStuck; }
    }

    new Collider2D collider2D;
    static ContactFilter2D filter2D = new();

    public static float currentZDepth;

    #region Event Subscriptions

    //Assigning events
    private void OnEnable()
    {
        Manipulate.OnPickup += Pickup;
        Manipulate.OnDrop += Drop;
        Placer.OnPlaced += Drop;
    }

    //Removing events
    private void OnDisable()
    {
        Manipulate.OnPickup -= Pickup;
        Manipulate.OnDrop -= Drop;
        Placer.OnPlaced -= Drop;
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
    void Pickup(GameObject gameObject, Vector2 pickupOffset)
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
            SortOnTop();

            Debug.Log(transform.position);

            CheckOverlaps();
        }
    }

    //Gives an object a lower Z value to render it on top
    void SortOnTop()
    {
        currentZDepth -= 0.01f;

        Vector3 thisObjectPosition = gameObject.transform.position;
        thisObjectPosition.z = currentZDepth;
        gameObject.transform.position = thisObjectPosition;
    }

    //FIX OVERLAP BUGGGG!!!!! IDK WHAT IS HAPPENING, ITS ALL IN THE RIGHT ORDER!!!!!

    //Checks for overlapping glue collisions, and either sticks the object to it, or un-sticks it if no glue is found
    void CheckOverlaps()
    {
        Debug.Log("Checking Overlaps!");

        List<Collider2D> overlapping = new();
        collider2D.OverlapCollider(filter2D.NoFilter(), overlapping);

        Debug.Log($"Overlap Count: {overlapping.Count}");

        //TODO: Fix this mess, it works but it doesnt look pretty. Glue not being a child of the object to be glued to it is a really good condition!!
        if (overlapping.Count > 0)
        {
            List<Collider2D> sticky = overlapping.FindAll(x => x.gameObject.layer == LayerMask.NameToLayer("Sticky") && !x.transform.IsChildOf(transform));
            Debug.Log($"Sticky Count: {sticky.Count}");

            if (sticky.Count > 0)
            {
                isStuck = true;
                transform.parent = sticky.First().transform;

                //TODO: Fix this too maybe, a little ugly
                Vector3 thisObjectPosition = gameObject.transform.localPosition;
                thisObjectPosition.z = transform.parent.position.z - 0.01f;
                gameObject.transform.localPosition = thisObjectPosition;

                OnStuck?.Invoke();
            }
            else
            {
                isStuck = false;
                transform.parent = null;
            }
        }
        else
        {
            isStuck = false;
            transform.parent = null;
        }
    }
}
