using UnityEngine;

public class SplitScreenCamera : MonoBehaviour
{
    [Header("Player Settings")]
    [Tooltip("Index of this player (0-based)")]
    public int index;

    [Tooltip("Total number of active players")]
    public int totalPlayers = 1;

    [Header("Aspect Ratio")]
    [Tooltip("Target camera aspect ratio (16:9 by default)")]
    [SerializeField] private float targetAspect = 16f / 9f;

    public Camera cam;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        SetupCamera();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
            cam = GetComponentInChildren<Camera>();

        SetupCamera();
    }

    public void SetupCamera()
    {
        Rect baseRect;

        switch (totalPlayers)
        {
            case 1:
                baseRect = new Rect(0f, 0f, 1f, 1f);
                break;

            case 2:
                baseRect = new Rect(
                    index == 0 ? 0f : 0.5f,
                    index == 0 ? 0.1f : 0.4f,
                    0.5f,
                    0.5f
                );
                break;

            case 3:
                baseRect = GetThreePlayerRect();
                break;

            default:
                baseRect = new Rect(
                    (index % 2) * 0.5f,
                    (index < 2) ? 0.5f : 0f,
                    0.5f,
                    0.5f
                );
                break;
        }

        cam.rect = GetAspectCorrectedRect(baseRect);
    }

    private Rect GetThreePlayerRect()
    {
        switch (index)
        {
            case 0: // Top-left
                return new Rect(0f, 0.5f, 0.5f, 0.5f);

            case 1: // Top-right
                return new Rect(0.5f, 0.5f, 0.5f, 0.5f);

            default: // Bottom-center
                return new Rect(0.25f, 0f, 0.5f, 0.5f);
        }
    }

    private Rect GetAspectCorrectedRect(Rect baseRect)
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float scaleHeight = screenAspect / targetAspect;

        if (scaleHeight < 1f)
        {
            // Letterbox (top & bottom)
            float height = scaleHeight;
            float yOffset = (1f - height) / 2f;

            return new Rect(
                baseRect.x,
                baseRect.y + baseRect.height * yOffset,
                baseRect.width,
                baseRect.height * height
            );
        }
        else
        {
            // Pillarbox (left & right)
            float width = 1f / scaleHeight;
            float xOffset = (1f - width) / 2f;

            return new Rect(
                baseRect.x + baseRect.width * xOffset,
                baseRect.y,
                baseRect.width * width,
                baseRect.height
            );
        }
    }
}
