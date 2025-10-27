using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ButtonSelection : MonoBehaviour
{
    public void menu()
    {
        // Load MainMenu (build index 0)
        SceneManager.LoadScene(0);
    }
    public void play()
    {
        // Load VastForest (build index 1)
        SceneManager.LoadScene(1);
    }

    public void levels()
    {
        // Load LevelMenu (build index 2)
        SceneManager.LoadScene(2);
    }

    public void level1()
    {
        // Load VastForest (build index 1)
        SceneManager.LoadScene(1);
    }

    public void level2()
    {
        // Load another level (if added later)
        SceneManager.LoadScene(3);
    }

    public void level3()
    {
        // Load another level (if added later)
        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }
}