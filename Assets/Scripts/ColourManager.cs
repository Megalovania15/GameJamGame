using UnityEngine;

public class ColourManager : MonoBehaviour
{
    public SpriteRenderer body;
    public SpriteRenderer head;

    public void SetColour(Color color)
    {
        body.color = color;
        head.color = color;
    }
}
