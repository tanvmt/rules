using UnityEngine;

namespace NenNhangSinhMenh.Events
{
    [CreateAssetMenu(fileName = "New Sound Event", menuName = "NenNhangSinhMenh/Events/Sound Event")]
    public class SoundEvent : SupernaturalEvent
    {
        [Header("Sound Settings")]
        [SerializeField] private AudioClip soundToPlay;

        public override void Execute()
        {
            if (soundToPlay != null)
            {
                Core.AudioManager.Instance.PlaySFX(soundToPlay);
            }
        }
    }
}
