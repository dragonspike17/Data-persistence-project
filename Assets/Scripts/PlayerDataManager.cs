using UnityEngine;
using System.IO;

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

    private string saveFilePath;

    [System.Serializable]
    private class SaveData
    {
        public string BestPlayerName;
        public int BestScore;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveFilePath = Path.Combine(
            Application.persistentDataPath,
            "savefile.json"
        );

        LoadHighScore();
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

            SaveHighScore();
        }
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();

        data.BestPlayerName = BestPlayerName;
        data.BestScore = BestScore;

        string json = JsonUtility.ToJson(data, true);

        try
        {
            File.WriteAllText(saveFilePath, json);

            Debug.Log("High Score Saved!");
            Debug.Log("Save Location: " + saveFilePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Save failed: " + e.Message);
        }
    }

    public void LoadHighScore()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(saveFilePath);

                SaveData data =
                    JsonUtility.FromJson<SaveData>(json);

                if (data != null)
                {
                    BestPlayerName = data.BestPlayerName;
                    BestScore = data.BestScore;

                    Debug.Log("High Score Loaded!");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Load failed: " + e.Message);
            }
        }
        else
        {
            Debug.Log("No previous save file found.");
        }
    }
}

