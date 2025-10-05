using UnityEngine;

namespace NenNhangSinhMenh.Events
{
    [CreateAssetMenu(fileName = "New Debug Log Event", menuName = "NenNhangSinhMenh/Events/Debug Log Event")]
    public class DebugLogEvent : SupernaturalEvent
    {
        [Header("Debug Settings")]
        [SerializeField] private string messageToLog = "Something spooky just happened!";

        public override void Execute()
        {
            Debug.LogWarning($"SUPERNATURAL EVENT TRIGGERED: {eventName} - {messageToLog}");
        }
    }
}