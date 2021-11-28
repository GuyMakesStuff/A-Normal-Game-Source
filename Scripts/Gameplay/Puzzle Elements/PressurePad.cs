using ANormalGame.Audio;
using UnityEngine.Events;
using UnityEngine;

namespace ANormalGame.Gameplay.PuzzleElements
{
    public class PressurePad : MonoBehaviour
    {
        [HideInInspector]
        public bool IsPressed;
        Material Mat;
        public MeshRenderer ButtonMesh;
        [Space]
        public UnityEvent OnPress;
        public UnityEvent OnPressStay;
        public UnityEvent OnLeave;

        // Start is called before the first frame update
        void Start()
        {
            Mat = new Material(ButtonMesh.sharedMaterial);
            Mat.name = "Pressure Pad Button Mat_" + GetHashCode();
            ButtonMesh.sharedMaterial = Mat;
        }

        // Update is called once per frame
        void Update()
        {
            Mat.color = (IsPressed) ? Color.green : Color.red;
            if (IsPressed)
            {
                OnPressStay.Invoke();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            IsPressed = true;
            AudioManager.Instance.InteractWithSFX("Pressure Pad Press", SoundEffectBehaviour.Play);
            OnPress.Invoke();
        }
        void OnTriggerExit(Collider other)
        {
            IsPressed = false;
            AudioManager.Instance.InteractWithSFX("Pressure Pad Release", SoundEffectBehaviour.Play);
            OnLeave.Invoke();
        }
    }
}