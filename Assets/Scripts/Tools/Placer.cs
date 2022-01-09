using UnityEngine;
using UnityEngine.Events;

public class Placer : Tool
{
    public static UnityAction<GameObject> OnPlaced;

    [SerializeField] GameObject placeablePrefab;

    public override void LeftMouseDown()
    {
        //Place an object onto something
        if (InputUtility.DidClickObject)
        {
            GameObject newObject;
            Vector3 placedPosition = new Vector3(InputUtility.MousePosition.x, InputUtility.MousePosition.y, 0);

            if(placeablePrefab.layer == LayerMask.NameToLayer("Sticky"))
            {
                newObject = Instantiate(placeablePrefab, placedPosition, Quaternion.identity, InputUtility.GetClickedObject().transform);
            }
            else
            {
                newObject = Instantiate(placeablePrefab, placedPosition, Quaternion.identity);
            }

            newObject.transform.localPosition = new Vector3(newObject.transform.localPosition.x, newObject.transform.localPosition.y, 0);

            OnPlaced?.Invoke(newObject);
        }
    }
}
