using ANormalGame.Audio;
using UnityEngine.UI;
using UnityEngine;

namespace ANormalGame.Gameplay.PuzzleElements
{
    public class ChargedDoor : Door
    {
        public Slider ChargeMeter;
        public float MaxChargeValue;
        [HideInInspector]
        public bool IsCharging;

        private void Awake()
        {
            ChargeMeter.maxValue = MaxChargeValue;
            ChargeMeter.value = 0f;
        }

        void Update()
        {
            ChargeMeter.value += (IsCharging) ? Time.deltaTime : -Time.deltaTime;

            if(ChargeMeter.value == 0 && IsOpen)
            {
                AudioManager.Instance.InteractWithSFX("Charge Down", SoundEffectBehaviour.Play);
                SetOpen(false);
            }
            else if(ChargeMeter.value == MaxChargeValue && !IsOpen)
            {
                AudioManager.Instance.InteractWithSFX("Charge Up", SoundEffectBehaviour.Play);
                SetOpen(true);
            }
        }

        public void SetCharging(bool Value)
        {
            IsCharging = Value;
        }
    }
}