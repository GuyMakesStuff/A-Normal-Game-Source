using System.Collections;
using ANormalGame.Audio;
using UnityEngine;

namespace ANormalGame.Gameplay.PuzzleElements
{
    public class Door : MonoBehaviour
    {
        Vector3 StartPos;
        Vector3 OpenPos;
        [HideInInspector]
        public bool IsOpen;
        Vector3 Velocity;

        // Start is called before the first frame update
        void Start()
        {
            StartPos = transform.localPosition;
            OpenPos = transform.localPosition + new Vector3(0, transform.localScale.y, 0);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 TargetPos = (IsOpen) ? OpenPos : StartPos;
            Vector3 Pos = Vector3.SmoothDamp(transform.localPosition, TargetPos, ref Velocity, 0.25f);
            transform.localPosition = Pos;
        }

        public void SetOpen(bool Value)
        {
            AudioManager.Instance.InteractWithSFX("Door Open", SoundEffectBehaviour.Play);
            IsOpen = Value;
        }
    }
}