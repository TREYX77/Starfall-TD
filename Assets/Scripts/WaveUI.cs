using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    private int currentWave = 1;

    public void UpdateWave(int wave)
    {
        currentWave = wave;
        waveText.text = "Wave: " + currentWave;
    }
}
