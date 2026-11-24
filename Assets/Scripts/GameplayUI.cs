using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [Header("Player Name Display")]
    public TMP_Text playerNameText;

    [Header("High Score Display")]
    public TMP_Text highScoreText;

    private void Start()
    {
        DisplayPlayerName();
        DisplayHighScore();
    }

    private void Update()
    {
        DisplayHighScore();
    }

    public void DisplayPlayerName()
    {
        if (playerNameText == null)
        {
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
        }
    }

    public void DisplayHighScore()
    {
        if (highScoreText == null)
        {
            return;
        }

        if (PlayerDataManager.Instance != null)
        {
            highScoreText.text = "Best Score: " +
                PlayerDataManager.Instance.BestPlayerName +
                " - " +
                PlayerDataManager.Instance.BestScore;
        }
        else
        {
            highScoreText.text = "Best Score: None - 0";
        }
    }
}


