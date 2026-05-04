using TMPro;
using UnityEngine;

public class UI_WaveCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveText;

    public void UpdateWaveGraphics(int waveNumber)
    {
        if (_waveText != null)
        {
            _waveText.text = "WAVE: " + waveNumber;
        }
    }
}
