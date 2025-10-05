using UnityEngine;
using TMPro;

namespace NenNhangSinhMenh.Core
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        // -----------------------------------------

        [Header("Timer Settings")]
        [SerializeField] private float timePerIncense = 300.0f;

        private float _remainingTime;
        private bool _isTimerRunning = false;

        private void Update()
        {
            if (_isTimerRunning)
            {
                if (_remainingTime > 0)
                {
                    _remainingTime -= Time.deltaTime;
                    UpdateTimerDisplay();
                }
                else
                {
                    _remainingTime = 0;
                    _isTimerRunning = false;

                    Debug.Log("Hết giờ rồi! Trò chơi kết thúc.");
                    UI.UIManager.Instance.UpdateTimer("Hết giờ!");
                }
            }
        }

        public void StartTimer()
        {
            _remainingTime = timePerIncense;
            _isTimerRunning = true;
            Debug.Log("Đồng hồ bắt đầu đếm ngược!");
        }

        private void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(_remainingTime / 60);
            int seconds = Mathf.FloorToInt(_remainingTime % 60);
            string timeString = string.Format("Thời Gian: {0:00}:{1:00}", minutes, seconds);
            UI.UIManager.Instance.UpdateTimer(timeString);
        }
    }
}