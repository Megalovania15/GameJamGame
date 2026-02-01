using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PortalColorChange : MonoBehaviour
{
    [Header("Renderer (Sprite or Mesh)")]
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Portal Colors")]
    [SerializeField] private Color idleColor = Color.cyan;
    [SerializeField] private Color activeColor = Color.magenta;

    [Header("Settings")]
    [SerializeField, Min(0f)] private float lerpSpeed = 8f;
    [SerializeField] private string playerTag = "Player";

    private Material runtimeMaterial;
    private Color targetColor;

    void Awake()
    {
        GetComponent<Collider>().isTrigger = true;

        // Grab whichever renderer exists
        if (meshRenderer != null)
            runtimeMaterial = meshRenderer.material;

        targetColor = idleColor;
        ApplyColorInstant(idleColor);
    }

    void Update()
    {
        // Smooth transition
        if (runtimeMaterial != null)
        {
            runtimeMaterial.color = Color.Lerp(
                runtimeMaterial.color,
                targetColor,
                Time.deltaTime * lerpSpeed
            );
        }
        else if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.Lerp(
                spriteRenderer.color,
                targetColor,
                Time.deltaTime * lerpSpeed
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            targetColor = activeColor;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
            targetColor = idleColor;
    }

    void ApplyColorInstant(Color c)
    {
        if (runtimeMaterial != null)
            runtimeMaterial.color = c;
        else if (spriteRenderer != null)
            spriteRenderer.color = c;
    }
}
