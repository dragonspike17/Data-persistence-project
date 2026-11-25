using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuUI : MonoBehaviour
{
    [Header("Player Name")]
    public TMP_InputField playerNameInput;

    [Header("High Score Display")]
    public TMP_Text menuHighScoreText;

    [Header("Scene Settings")]
    public string gameSceneName = "main";

    private void Start()
    {
        DisplayHighScore();
    }

    public void DisplayHighScore()
    {
        if (menuHighScoreText == null)
        {
            return;
        }

        if (PlayerDataManager.Instance != null)
        {
            menuHighScoreText.text =
                "Best Score: " +
                PlayerDataManager.Instance.BestPlayerName +
                " - " +
                PlayerDataManager.Instance.BestScore;
        }
        else
        {
            menuHighScoreText.text =
                "Best Score: None - 0";
        }
    }

    public void StartGame()
    {
        if (PlayerDataManager.Instance == null)
        {
            Debug.LogError(
                "PlayerDataManager is missing!"
            );
            return;
        }

        if (playerNameInput == null)
        {
            Debug.LogError(
                "Player Name Input is not assigned!"
            );
            return;
        }

        PlayerDataManager.Instance.SetPlayerName(
            playerNameInput.text
        );

        SceneManager.LoadScene(gameSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

