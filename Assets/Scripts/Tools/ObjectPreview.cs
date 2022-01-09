using UnityEngine;

public class ObjectPreview : MonoBehaviour
{
    public static GameObject instance;

    SpriteRenderer spriteRenderer;
    Vector2 previewOffset;

    #region Event Subscriptions

    //Assigning events
    private void OnEnable()
    {
        //Maker.OnObjectSelected += PreviewObject;
        //Maker.OnSelectionCleared += ClearPreview;
        Manipulate.OnPickup += PreviewObject;
        Manipulate.OnDrop += ClearPreview;
    }

    //Removing events
    private void OnDisable()
    {
        //Maker.OnObjectSelected -= PreviewObject;
        //Maker.OnSelectionCleared -= ClearPreview;
        Manipulate.OnPickup -= PreviewObject;
        Manipulate.OnDrop -= ClearPreview;
    }

    #endregion


    private void Awake()
    {
        instance = gameObject;

        InitializeComponents();
    }

    void InitializeComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //The preview follows the mouse when active
        transform.position = InputUtility.MousePosition + previewOffset;
    }

    //Sets the sprite of the preview to the object's sprite, if it exists
    void PreviewObject(GameObject gameObject, Vector2 offset)
    {
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
    }

    //Clears the sprite of the preview
    void ClearPreview(GameObject gameObject = null)
    {
        spriteRenderer.sprite = null;
    }
}
