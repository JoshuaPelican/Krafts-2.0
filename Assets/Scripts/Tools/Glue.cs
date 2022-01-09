using UnityEngine;
using UnityEngine.Events;

public class Glue : Tool
{
    public static UnityAction OnGluePlaced;

    [SerializeField] GameObject glueBlobPrefab;

    public override void LeftMouseDown()
    {
        //Place a glue blob onto an object or on nothing at the cursor location
        if (InputUtility.DidClickObject)
        {
            Vector3 gluePosition = new Vector3(InputUtility.MousePosition.x, InputUtility.MousePosition.y);

            Instantiate(glueBlobPrefab, gluePosition, Quaternion.identity, InputUtility.GetClickedObject().transform);

            OnGluePlaced?.Invoke();
        }
    }
}
