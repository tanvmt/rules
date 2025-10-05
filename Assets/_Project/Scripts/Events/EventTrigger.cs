using UnityEngine;

namespace NenNhangSinhMenh.Events
{
    [RequireComponent(typeof(Collider))]
    public class EventTrigger : MonoBehaviour
    {
        [Header("Event Configuration")]
        [Tooltip("The event asset to be executed when triggered.")]
        [SerializeField] private SupernaturalEvent eventToTrigger;

        [Tooltip("If true, this trigger will only activate once and then disable itself.")]
        [SerializeField] private bool triggerOnce = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (eventToTrigger != null)
                {
                    Debug.Log($"Player entered trigger for event: {eventToTrigger.name}");
                    eventToTrigger.Execute();

                    if (triggerOnce)
                    {
                        GetComponent<Collider>().enabled = false;
                    }
                }
                else
                {
                    Debug.LogError("EventTrigger is missing an event to trigger!", this);
                }
            }
        }

        private void OnValidate()
        {
            Collider col = GetComponent<Collider>();
            if (col != null && !col.isTrigger)
            {
                col.isTrigger = true;
            }
        }
    }
}