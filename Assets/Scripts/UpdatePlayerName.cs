using UnityEngine;

public class UpdatePlayerName : MonoBehaviour
{
    public void NewPlayerName(GameObject player, int playerNumber)
    {
        player.name = $"Player {playerNumber}";
    }
}
