using UnityEngine;

public static class InputUtility
{
    public static Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public static float HorizontalInput(bool isRaw = false)
    {
        if (isRaw)
            return Input.GetAxisRaw("Horizontal");
        else
            return Input.GetAxis("Horizontal");

    }

    public static float VerticalInput(bool isRaw = false)
    {
        if (isRaw)
            return Input.GetAxisRaw("Vertical");
        else
            return Input.GetAxis("Vertical");
    }

    public static bool DidClickObject
    {
        get { return Physics2D.OverlapPoint(MousePosition, LayerMask.GetMask("Interactable")); }
    }

    public static GameObject GetClickedObject()
    {
        Collider2D hit = Physics2D.OverlapPoint(MousePosition, LayerMask.GetMask("Interactable"));

        if (DidClickObject)
        {
            Debug.Log("Clicked " + hit.name);
            return hit.gameObject;
        }
        else
        {
            Debug.Log("Clicked Nothing");
            return null;
        }
    }
}

