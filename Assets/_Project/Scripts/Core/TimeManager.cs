using System;
using UnityEngine;
using UnityEngine.Rendering; // For Volume and PostProcessing
using UnityEngine.Rendering.Universal; // For URP Effects

namespace NenNhangSinhMenh.Core
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }

        [Header("Game Progression")]
        [SerializeField] private Interactables.AltarController altarController;
        [SerializeField] private GameObject roomLights;
        [SerializeField] private int powerOutageThreshold = 3;
        private int _incenseLitCount = 0;

        [Header("Game Over Settings")]
        [SerializeField] private float gracePeriod = 60.0f;
        private float _gracePeriodTimer;
        private bool _isInGracePeriod = false;

        [Header("Grace Period Effects")]
        [SerializeField] private Volume postProcessingVolume;
        [SerializeField] private float vignettePulseSpeed = 5f;
        [SerializeField] [Range(0f, 1f)] private float maxVignetteIntensity = 0.6f;
        [SerializeField] [Range(0f, 1f)] private float maxAberrationIntensity = 0.5f;
        [SerializeField] [Range(-1f, 1f)] private float maxDistortionIntensity = -0.3f;

        private Vignette _vignette;
        private ChromaticAberration _chromaticAberration;
        private LensDistortion _lensDistortion;

        private float _defaultVignetteIntensity;
        private float _defaultAberrationIntensity;
        private float _defaultDistortionIntensity;


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

            if(postProcessingVolume != null)
            {
                postProcessingVolume.profile.TryGet(out _vignette);
                postProcessingVolume.profile.TryGet(out _chromaticAberration);
                postProcessingVolume.profile.TryGet(out _lensDistortion);

                if (_vignette != null)
                    _defaultVignetteIntensity = _vignette.intensity.value;
                if (_chromaticAberration != null)
                    _defaultAberrationIntensity = _chromaticAberration.intensity.value;
                if (_lensDistortion != null)
                    _defaultDistortionIntensity = _lensDistortion.intensity.value;
            }
        }

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
                    StartGracePeriod();
                }
            }

            if (_isInGracePeriod)
            {
                if (_gracePeriodTimer > 0)
                {
                    _gracePeriodTimer -= Time.deltaTime;
                    HandleGracePeriodEffects();
                }
                else
                {
                    _isInGracePeriod = false;
                    TriggerGameOver();
                }
            }
        }

        private void HandleGracePeriodEffects()
        {
            if (_vignette != null)
            {
                float pulse = Mathf.Abs(Mathf.Sin(Time.time * vignettePulseSpeed));
                _vignette.intensity.value = Mathf.Lerp(_defaultVignetteIntensity, maxVignetteIntensity, pulse);
            }
            if (_chromaticAberration != null)
            {
                _chromaticAberration.intensity.value = maxAberrationIntensity;
            }
            if (_lensDistortion != null)
            {
                _lensDistortion.intensity.value = maxDistortionIntensity;
            }
        }

        private void TriggerGameOver()
        {
            Debug.LogError("Game Over! Bạn đã không thắp nhang kịp thời.");
            // last jump scare 
            Time.timeScale = 0;
        }

        private void StartGracePeriod()
        {
            Debug.LogWarning("Thời gian đã hết! Bắt đầu thời gian ân hạn.");
            _isInGracePeriod = true;
            _gracePeriodTimer = gracePeriod;
            // Core.AudioManager.Instance.PlaySFX(gracePeriodSound);
            UI.UIManager.Instance.UpdateTimer("NGUY HiIỂM");

            if (altarController != null)
            {
                altarController.ExtinguishIncense();
            }
            else
            {
                Debug.LogError("Lỗi: Không tìm thấy AltarController để tắt nhang.");
            }
        }

        public void StartTimer()
        {
            if (_isInGracePeriod)
            {
                ResetGracePeriodEffects();
            }
            _isInGracePeriod = false;
            _remainingTime = timePerIncense;
            _isTimerRunning = true;
            _incenseLitCount++;
            Debug.Log($"Đã thắp {_incenseLitCount} nén nhang.");

            if(_incenseLitCount >= powerOutageThreshold && roomLights.activeSelf)
            {
                TriggerPowerOutage();
            }
            Debug.Log("Đồng hồ bắt đầu đếm ngược!");
        }

        private void ResetGracePeriodEffects()
        {
            if (_vignette != null)
            {
                _vignette.intensity.value = _defaultVignetteIntensity;
            }
            if (_chromaticAberration != null)
            {
                _chromaticAberration.intensity.value = _defaultAberrationIntensity;
            }
            if (_lensDistortion != null)
            {
                _lensDistortion.intensity.value = _defaultDistortionIntensity;
            }
        }

        private void TriggerPowerOutage()
        {
            roomLights.SetActive(false);
            Debug.Log("Cúp điện! Đèn trong phòng đã tắt.");
            // Core.AudioManager.Instance.PlaySFX(powerOutageSound);
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