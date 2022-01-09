using UnityEngine;
using UnityEngine.Events;

public class Maker : Tool
{
    public static UnityAction<GameObject> OnObjectSelected;
    public static UnityAction OnSelectionCleared;
    public static UnityAction<GameObject> OnMakeObject;

    static GameObject selectedObject;

    [SerializeField] GameObject objectPreview;

    public override void SwappedTo()
    {
        base.SwappedTo();

        //When maker is swapped to, the preview is activated
        objectPreview.gameObject.SetActive(true);
    }

    public override void SwappedFrom()
    {
        base.SwappedFrom();

        //When maker is swapped from, the preview is deactivated
        objectPreview.gameObject.SetActive(false);
    }

    public override void LeftMouseDown()
    {
        //If there is a selected object, place it down
        if(selectedObject != null)
        {
            GameObject newObject = Instantiate(selectedObject, InputUtility.MousePosition, selectedObject.transform.rotation);
            OnMakeObject?.Invoke(newObject);
        }
        //Otherwise if a valid object is clicked on then select that object
        else
        {
            if (InputUtility.DidClickObject)
            {
                SelectObject(InputUtility.GetClickedObject());
            }
        }
    }

    //Clears the selected object
    public override void RightMouseDown()
    {
        ClearSelectedObject();   
    }

    //Sets the selected object
    void SelectObject(GameObject gameObject)
    {
        selectedObject = gameObject;
        OnObjectSelected?.Invoke(selectedObject);
    }

    //Clears the selected object
    void ClearSelectedObject()
    {
        selectedObject = null;
        OnSelectionCleared?.Invoke();
    }
}
