using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuUI : MonoBehaviour
{
    [Header("Player Name")]
    public TMP_InputField playerNameInput;

    [Header("Scene Settings")]
    public string gameSceneName = "main";

    public void StartGame()
    {
        // Check that the data manager exists.
        if (PlayerDataManager.Instance == null)
        {
            Debug.LogError("PlayerDataManager is missing!");
            return;
        }

        // Check that the input field is assigned.
        if (playerNameInput == null)
        {
            Debug.LogError("Player Name Input is not assigned!");
            return;
        }

        // Save the player's name.
        PlayerDataManager.Instance.SetPlayerName(
            playerNameInput.text
        );

        // Load the gameplay scene.
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
