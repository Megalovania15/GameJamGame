using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField]
    private float objectLifetime = 5f;

    [SerializeField]
    private Color newColour;

    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        StartCoroutine(FadeObjectOvertime());
    }

    IEnumerator FadeObjectOvertime()
    { 
        float step = Time.deltaTime / objectLifetime;

        while (sprite.color.a > 0f)
        {
            sprite.color = Color.Lerp(sprite.color, newColour, step);
            yield return null;
        }

        Destroy(gameObject);
    }
}
