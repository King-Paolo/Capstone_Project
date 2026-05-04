using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerUpPanel : MonoBehaviour
{
    [SerializeField] private SO_PowerUpData[] _powerUpData;
    [SerializeField] private GameObject _powerUpPanel;
    [SerializeField] private Button[] _powerUpButtons;
    [SerializeField] private TextMeshProUGUI[] _powerUpText;

    public void ShowPowerUps()
    {
        _powerUpPanel.SetActive(true);
        Time.timeScale = 0;

        List<SO_PowerUpData> powerUp = new List<SO_PowerUpData>(_powerUpData);

        for (int i = 0; i < _powerUpButtons.Length; i++)
        {
            if (powerUp.Count == 0)
                break;

            int randomIndex = Random.Range(0, powerUp.Count);

            SO_PowerUpData selectedPowerUp = powerUp[randomIndex];

            _powerUpText[i].text = selectedPowerUp.powerUpName;

            _powerUpButtons[i].onClick.RemoveAllListeners();
            _powerUpButtons[i].onClick.AddListener(() => ApplyPowerUp(selectedPowerUp));

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ApplyPowerUp(SO_PowerUpData powerUpData)
    {
        powerUpData.effect.Invoke();
        _powerUpPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1;
    }
}
