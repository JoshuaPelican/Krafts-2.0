using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    [Header("Tool Settings")]
    [SerializeField] Texture2D cursorTexture;

    //Do something specific when the tool has been swapped to
    public virtual void SwappedTo()
    {
        //Change the cursor to the tool's cursor
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public virtual void SwappedFrom()
    {

    }

    public virtual void LeftMouseDown()
    {

    }

    public virtual void LeftMouseUp()
    {

    }

    public virtual void RightMouseDown()
    {

    }
}
