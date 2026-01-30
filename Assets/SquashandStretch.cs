using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    [Header("Squash & Stretch Settings")]
    [Range(0f, 1f)]
    public float intensity = 0.2f; // How exaggerated the effect is

    public float speed = 2f;       // How fast it animates

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float squash = Mathf.Sin(Time.time * speed) * intensity;

        float x = originalScale.x + squash;
        float y = originalScale.y - squash;
        float z = originalScale.z;

        transform.localScale = new Vector3(x, y, z);
    }
}
