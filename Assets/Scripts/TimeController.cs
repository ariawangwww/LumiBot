using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Text countdownText;
    private float timeRemaining = 240f;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = $"{minutes:D2}:{seconds:D2}";
    }
}
