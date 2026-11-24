using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance
    {
        get;
        private set;
    }

    [field: SerializeField]
    public string PlayerName
    {
        get;
        private set;
    } = "Player";

    private void Awake()
    {
        // Prevent duplicate data managers.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Keep player data when changing scenes.
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName(string newName)
    {
        // Use a default name if nothing is entered.
        if (string.IsNullOrWhiteSpace(newName))
        {
            PlayerName = "Player";
        }
        else
        {
            PlayerName = newName.Trim();
        }

        Debug.Log("Player Name Saved: " + PlayerName);
    }
}
