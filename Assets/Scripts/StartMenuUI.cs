using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuUI : MonoBehaviour
{
    [Header("Player Name Input")]
    public TMP_InputField playerNameInput;

    [Header("Scene Settings")]
    public string gameSceneName = "Main";

    public void StartGame()
    {
        string enteredName = playerNameInput != null
            ? playerNameInput.text
            : "Player";

        if (PlayerDataManager.Instance != null)
        {
            PlayerDataManager.Instance.SetPlayerName(enteredName);
        }
        else
        {
            Debug.LogWarning("PlayerDataManager is missing!");
        }

        SceneManager.LoadScene(gameSceneName);
    }
}

