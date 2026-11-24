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

    [field: SerializeField]
    public string BestPlayerName
    {
        get;
        private set;
    } = "None";

    [field: SerializeField]
    public int BestScore
    {
        get;
        private set;
    } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName(string newName)
    {
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

    public void UpdateHighScore(int currentScore)
    {
        if (currentScore > BestScore)
        {
            BestScore = currentScore;
            BestPlayerName = PlayerName;

            Debug.Log("New High Score: " + BestScore);
            Debug.Log("Best Player: " + BestPlayerName);
        }
    }
}
