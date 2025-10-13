using UnityEngine;
using UnityEngine.UI;

public class CubeSpawnTimer : MonoBehaviour
{
    public Text timerText; // Assign a UI Text element in the Inspector
    public float totalTime = 60f;

    private float timeLeft;

    void Start()
    {
        timeLeft = totalTime;
    }

    void Update()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            timerText.text = "Done!";
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.CeilToInt(timeLeft);
        timerText.text = "Time Left: " + seconds + "s";
    }
}