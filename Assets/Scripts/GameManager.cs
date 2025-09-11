using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    [Header("Game Time")]
    public TextMeshProUGUI clockText;
    public float secondsPerGameMinute = 1f;
    public int startHour = 23;
    public int startMinute = 45;

    private int currentHour;
    private int currentMinute;
    private float timer;

    [Header("Clue System")]
    private List<string> collectedClues = new List<string>();

    [Header("Strike System")]
    public TextMeshProUGUI strikeText;
    public int maxStrikes = 3;
    public int currentStrikes = 0;

    [Header("Rule References")]
    public InteractiveDoor mainDoor;

    private bool rule1_EventTriggered = false;
    void Start()
    {
        currentHour = startHour;
        currentMinute = startMinute;
        timer = 0f;
        UpdateClockDisplay();

        currentStrikes = 0;
        UpdateStrikeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= secondsPerGameMinute)
        {
            timer -= secondsPerGameMinute;
            currentMinute++;

            if (currentMinute >= 60)
            {
                currentMinute = 0;
                currentHour++;
            }

            if (currentHour >= 24)
            {
                currentHour = 0;
            }
        }

        UpdateClockDisplay();

        // Test game over condition
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            RecordStrike();
        }

        if (!rule1_EventTriggered && currentHour == 0 && currentMinute == 15)
        {
            rule1_EventTriggered = true;
            Debug.Log("SỰ KIỆN: (00:15) Tiếng chuông gió treo ngoài cửa đột nhiên rung lên dữ dội!");
            if (mainDoor.IsOpen)
            {
                Debug.Log("HẬU QUẢ: Cửa đang mở! Khí xấu đã lọt vào.");
                RecordStrike();
            }
            else
            {
                Debug.Log("AN TOÀN: Cửa đã được đóng kín.");
            }
        }
    }

    void UpdateClockDisplay()
    {
        clockText.text = string.Format("{0:00}:{1:00}", currentHour, currentMinute);
    }

    public void AddClue(string clueDescription)
    {
        if (!collectedClues.Contains(clueDescription))
        {
            collectedClues.Add(clueDescription);
            Debug.Log("Clue added: " + clueDescription);
        }
    }

    public List<string> GetCollectedClues()
    {
        return collectedClues;
    }

    void UpdateStrikeDisplay()
    {
        strikeText.text = "Lỗi: " + currentStrikes + " / " + maxStrikes;
    }

    public void RecordStrike()
    {
        currentStrikes++;
        UpdateStrikeDisplay();

        if (currentStrikes >= maxStrikes)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over! You have reached the maximum number of strikes.");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
