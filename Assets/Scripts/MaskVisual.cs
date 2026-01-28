using UnityEngine;

public class MaskVisual : MonoBehaviour
{
    [SerializeField] SpriteRenderer headRenderer;

    void Reset()
    {
        headRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetMask(Sprite maskSprite)
    {
        headRenderer.sprite = maskSprite;
        headRenderer.enabled = maskSprite != null;
    }

}
