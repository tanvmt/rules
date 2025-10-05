using UnityEngine;

namespace NenNhangSinhMenh.Events
{
    public abstract class SupernaturalEvent : ScriptableObject
    {
        [Header("Event Description")]
        [SerializeField] protected string eventName;
        [TextArea]
        [SerializeField] protected string description;

        public abstract void Execute();
    }
}