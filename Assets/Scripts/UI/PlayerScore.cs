using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    private int kills = 0, deaths = 0;

    [SerializeField]
    // passes k/d as int kills, int deaths
    private UnityEvent<int, int> OnScoreChange = new();

    //increases this game object's kills
    public void IncreaseKills()
    {
        kills++;
        OnScoreChange.Invoke(kills, deaths);
        Debug.Log($"{gameObject.name} kills: {kills}");
    }

    //increase this game object's deaths
    public void IncreaseDeaths()
    {
        deaths++;
        OnScoreChange.Invoke(kills, deaths);
        Debug.Log($"{gameObject.name} deaths: {deaths}");
    }

    public void AddOnScoreChangeListener(UnityAction<int, int> action)
    {
        OnScoreChange.AddListener(action);
    }
}
