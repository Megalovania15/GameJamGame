using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject endGameScreen;
    public TMP_Text playerWinsText;

    private List<PlayerScore> activePlayers = new();

    void Start()
    {
        endGameScreen.SetActive(false);

    }

    public void AddActivePlayers(GameObject player, int playerNumber)
    {
        activePlayers.Add(player.GetComponent<PlayerScore>());
    }

    public void ActivateEndScreen()
    {
        var winningPlayer = DetermineWinningPlayer();
        endGameScreen.SetActive(true);
        UpdateWinText(winningPlayer);
    }

    private void UpdateWinText(GameObject player)
    {
        var playerScore = player.GetComponent<PlayerScore>();
        
        playerWinsText.text = $"{player.name} wins! With: " +
            $"\n{playerScore.Kills} kills! " +
            $"\nAnd {playerScore.Deaths} deaths!";
    }

    private GameObject DetermineWinningPlayer()
    {
        var winningPlayer = activePlayers[0];

        for (int i = 0; i < activePlayers.Count; i++)
        {
            if (ReferenceEquals(winningPlayer, activePlayers[i]))
            {
                continue;
            }

            if (activePlayers[i].Kills > winningPlayer.Kills)
            {
                winningPlayer = activePlayers[i];
            }
            else if (activePlayers[i].Kills == winningPlayer.Kills)
            {
                if (activePlayers[i].Deaths < winningPlayer.Deaths)
                {
                    winningPlayer = activePlayers[i];
                }
                else if (activePlayers[i].Deaths == winningPlayer.Deaths)
                {
                    continue;
                }
            }
        }

        return winningPlayer.gameObject;
    }
}
