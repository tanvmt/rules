using UnityEngine;

namespace NenNhangSinhMenh.Gameplay
{
    /// <summary>
    /// Base class for all items in the game using ScriptableObject.
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "NenNhangSinhMenh/Item")]
    public class ItemData : ScriptableObject
    {
        [Header("Item Information")]
        public string itemName;
        public string description;
        public Sprite icon;
    }
}