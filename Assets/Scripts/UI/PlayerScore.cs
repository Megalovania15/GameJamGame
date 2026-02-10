using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    public int Kills { get; private set; } = 0;
    public int Deaths { get; private set; } = 0;

    [SerializeField]
    // passes k/d as int kills, int deaths
    private UnityEvent<GameObject, int, int> OnScoreChange = new();

    private TMP_Text playerName;

    void Start()
    {
        playerName = GetComponentInChildren<TMP_Text>();
        playerName.text = gameObject.name;
    }

    //increases this game object's kills
    public void IncreaseKills()
    {
        Kills++;
        OnScoreChange.Invoke(this.gameObject, Kills, Deaths);
        Debug.Log($"{gameObject.name} kills: {Kills}");
    }

    //increase this game object's deaths
    public void IncreaseDeaths()
    {
        Deaths++;
        OnScoreChange.Invoke(this.gameObject, Kills, Deaths);
        Debug.Log($"{gameObject.name} deaths: {Deaths}");
    }

    public void AddOnScoreChangeListener(UnityAction<GameObject, int, int> action)
    {
        OnScoreChange.AddListener(action);
    }
}
