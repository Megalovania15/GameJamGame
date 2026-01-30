using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FirePitSprite : MonoBehaviour
{
    public void OnFiredUp()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void OnAboutToFireUp()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void OnCooledDown()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
