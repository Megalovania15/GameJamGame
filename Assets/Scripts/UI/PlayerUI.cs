using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject Owner { get; private set; }

    private TMP_Text scoreText;

    void Awake()
    {
        scoreText = GetComponentInChildren<TMP_Text>();
        UpdateText(this.gameObject, 0, 0);
    }

    public void SetOwner(GameObject newOwner)
    {
        Owner = newOwner;
        newOwner.GetComponent<PlayerScore>().AddOnScoreChangeListener(UpdateText);
    }

    public void UpdateText(GameObject player, int kills, int deaths)
    {
        scoreText.text = $"Kills: {kills} / Deaths: {deaths}";
    }

}
