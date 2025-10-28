            using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    private bool isGameOver = false;

    void Awake()
    {
        // Ensure panel starts hidden
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    // Call this to show the Game Over UI
    public void ShowGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }

    // Hook these to UI Buttons:
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) return;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

   
}
