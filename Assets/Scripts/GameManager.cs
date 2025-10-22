using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameStarted { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Keep game paused until Start is pressed
            IsGameStarted = false;
            Time.timeScale = 0f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        IsGameStarted = true;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        IsGameStarted = false;
        Time.timeScale = 0f;
    }
}