using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [Header("Player Name Display")]
    public TMP_Text playerNameText;

    private void Start()
    {
        DisplayPlayerName();
    }

    public void DisplayPlayerName()
    {
        if (playerNameText == null)
        {
            Debug.LogError("Player Name Text is not assigned!");
            return;
        }

        if (PlayerDataManager.Instance != null)
        {
            playerNameText.text = "Player: " +
                PlayerDataManager.Instance.PlayerName;
        }
        else
        {
            playerNameText.text = "Player: Player";

            Debug.LogWarning(
                "PlayerDataManager was not found."
            );
        }
    }
}

