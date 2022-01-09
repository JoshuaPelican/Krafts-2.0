using UnityEngine;

public class ObjectPreview : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    #region Event Subscriptions

    //Assigning events
    private void OnEnable()
    {
        Maker.OnObjectSelected += PreviewObject;
        Maker.OnSelectionCleared += ClearPreview;
    }

    //Removing events
    private void OnDisable()
    {
        Maker.OnObjectSelected -= PreviewObject;
        Maker.OnSelectionCleared -= ClearPreview;
    }

    #endregion

    private void Awake()
    {
        InitializeComponents();
    }

    void InitializeComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //The preview follows the mouse when active
        transform.position = InputUtility.MousePosition;
    }

    //Sets the sprite of the preview to the object's sprite, if it exists
    void PreviewObject(GameObject gameObject)
    {
        if(gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            this.spriteRenderer.sprite = spriteRenderer.sprite;
        }
        else
        {
            Debug.LogError($"{gameObject.name} does not have a Sprite Renderer and cannot be previewed!");
        }
    }

    //Clears the sprite of the preview
    void ClearPreview()
    {
        spriteRenderer.sprite = null;
    }
}
