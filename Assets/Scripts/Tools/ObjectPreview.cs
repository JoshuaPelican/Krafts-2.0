using UnityEngine;

public class ObjectPreview : MonoBehaviour
{
    #region Simple Singleton

    public static GameObject instance;

    private void Awake()
    {
        instance = gameObject;
    }

    #endregion

    [SerializeField] Color previewColor = new Color(0, 0, 0, 0.5f);

    Vector2 previewOffset;

    #region Event Subscriptions

    //Assigning events
    private void OnEnable()
    {
        Manipulate.OnPickup += PreviewObject;
        Manipulate.OnDrop += ClearPreview;
    }

    //Removing events
    private void OnDisable()
    {
        Manipulate.OnPickup -= PreviewObject;
        Manipulate.OnDrop -= ClearPreview;
    }

    #endregion

    private void Update()
    {
        //The preview follows the mouse when active
        transform.position = InputUtility.MousePosition + previewOffset;
    }

    //Sets the sprite of the preview to the object's sprite, if it exists
    void PreviewObject(GameObject gameObject, Vector2 offset)
    {
        /*
        if(gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
        previewOffset = offset;
        this.spriteRenderer.sprite = spriteRenderer.sprite;
        }
        else
        {
        ClearPreview();
        Debug.LogError($"{gameObject.name} does not have a Sprite Renderer and cannot be previewed!");
        }
        */

        Instantiate(gameObject, previewOffset + (Vector2)transform.position, Quaternion.identity, transform);

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.color = previewColor;
        }
    }

    //Clears the sprite of the preview
    void ClearPreview(GameObject gameObject = null)
    {
        Destroy(transform.GetChild(0).gameObject);
    }
}
