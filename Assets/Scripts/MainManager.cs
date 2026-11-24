using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    private InputAction m_LaunchAction;

    void Awake()
    {
        m_LaunchAction = new InputAction(
            "Launch",
            InputActionType.Button,
            "<Keyboard>/space"
        );
    }

    void OnEnable()
    {
        m_LaunchAction.Enable();
    }

    void OnDisable()
    {
        m_LaunchAction.Disable();
    }

    void OnDestroy()
    {
        m_LaunchAction.Dispose();
    }

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] 
        {
            1, 1, 2, 2, 5, 5
        };

        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(
                    -1.5f + step * x,
                    2.5f + i * 0.3f,
                    0
                );

                var brick = Instantiate(
                    BrickPrefab,
                    position,
                    Quaternion.identity
                );

                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        // Reset the current score.
        m_Points = 0;
        ScoreText.text = "Score : 0";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (m_LaunchAction.WasPressedThisFrame())
            {
                m_Started = true;

                float randomDirection =
                    Random.Range(-1.0f, 1.0f);

                Vector3 forceDir = new Vector3(
                    randomDirection, 1, 0
                );

                forceDir.Normalize();

                Ball.transform.SetParent(null);

                Ball.AddForce(
                    forceDir * 2.0f,
                    ForceMode.VelocityChange
                );
            }
        }
        else if (m_GameOver)
        {
            if (m_LaunchAction.WasPressedThisFrame())
            {
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().buildIndex
                );
            }
        }
    }

    void AddPoint(int point)
    {
        // Increase the current score.
        m_Points += point;

        // Update the score display.
        ScoreText.text = $"Score : {m_Points}";

        // Check for a new high score.
        if (PlayerDataManager.Instance != null)
        {
            PlayerDataManager.Instance.UpdateHighScore(
                m_Points
            );
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // Check the final score.
        if (PlayerDataManager.Instance != null)
        {
            PlayerDataManager.Instance.UpdateHighScore(
                m_Points
            );
        }
    }
}
