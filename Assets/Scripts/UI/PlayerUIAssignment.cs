using UnityEngine;
using UnityEditor;

public class PlayerUIAssignment : MonoBehaviour
{
    [SerializeField]
    private PlayerUI[] playerScoreUI;

    public void DisableScoreUI()
    {
        foreach (var scoreUI in playerScoreUI)
        {
            if (scoreUI.Owner == null && scoreUI.gameObject.activeSelf)
            {
                scoreUI.gameObject.SetActive(false);
            }
        }
    }

    public void EnableScoreUI()
    {
        foreach (var scoreUI in playerScoreUI)
        {
            if (!scoreUI.gameObject.activeSelf && scoreUI.Owner != null)
            { 
                scoreUI.gameObject.SetActive(true);
            }
        }
    }

    //will assign the first index to the first player
    //if the index is not free, it will assign to the second one and so forth
    public void AssignUIToPlayer(GameObject player)
    {
        if (player is null)
        {
            Debug.Log("Player is missing");
        }

        for (int i = 0; i < playerScoreUI.Length; i++)
        {
            if (playerScoreUI[i].Owner is null)
            {
                playerScoreUI[i].SetOwner(player);
                break;
            }
        }
    }
}
