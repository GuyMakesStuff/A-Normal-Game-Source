using ANormalGame.Managers;
using ANormalGame.Audio;
using UnityEngine;

namespace EasyPlayerController
{
    public abstract class PlayerController : MonoBehaviour
    {
        public static PlayerInputReciever InputReciever;

        [Header("Camera")]
        public Transform Head;
        public float Sens = 100f;
        public float HeadHeight = 0.5f;
        Transform Cam;
        float XRig;

        [Header("Movement")]
        public float BaseSpeed = 6f;
        public float SprintSpeedMult = 2f;
        float Speed;
        public Transform GroundChecker;
        public LayerMask GroundLayer;
        public float Gravity = -9.81f;
        public float JumpHeight = 10f;
        [HideInInspector]
        public bool IsSprinting;
        [HideInInspector]
        public bool IsGrounded;
        bool Landed;
        Vector3 GravVel;

        void Start()
        {
            PlayerInputReciever playerInputReciever = FindObjectOfType<PlayerInputReciever>();
            if (playerInputReciever == null)
            {
                InputReciever = new GameObject("Player Input Manager").AddComponent<PlayerInputReciever>();
            }
            else
            {
                InputReciever = playerInputReciever;
            }

            Cam = Camera.main.transform;
            if (Head == null)
            {
                Head = new GameObject("Head").transform;
            }
            Head.SetParent(transform);

            MoveComponentInit();
        }
        public virtual void MoveComponentInit()
        {
            Debug.Log("Initalizing Movement Component");
        }

        void Update()
        {
            UpdateCam();
            UpdateMove();
        }
        void UpdateCam()
        {
            Head.position = transform.position + new Vector3(0, HeadHeight, 0);
            Cam.position = Head.position;
            Cam.rotation = Head.rotation;

            XRig -= InputReciever.MouseY * Sens * Time.deltaTime;
            XRig = Mathf.Clamp(XRig, -90f, 90f);

            transform.Rotate(Vector3.up, InputReciever.MouseX * Sens * Time.deltaTime);
            Head.localRotation = Quaternion.Euler(XRig, 0, 0);
        }
        void UpdateMove()
        {
            IsGrounded = Physics.CheckSphere(GroundChecker.position, 0.48f, GroundLayer);
            IsSprinting = InputReciever.SprintKeyHold;
            Speed = (IsSprinting) ? BaseSpeed * SprintSpeedMult : BaseSpeed;
            Vector3 Movement = (InputReciever.Hori * transform.right) + (InputReciever.Vert * transform.forward);
            RequestMove(Movement * Speed, GravVel);

            if (IsGrounded)
            {
                OnGrounded();
            }
            else
            {
                OnNotGrounded();
            }
        }

        public virtual void RequestMove(Vector3 MoveVect, Vector3 GravVect)
        {
            // Debug.Log("Movement Vector-" + MoveVect.ToString() + ".Gravity Vector-" + GravVect.ToString() + ".Total Velocity-" + (MoveVect + GravVect).ToString());
            bool PlaySound = ((IsGrounded) && MoveVect.magnitude > 0.1f) && !GlobalUIManager.Instance.IsPaused;
            if (PlaySound)
            {
                AudioManager.Instance.InteractWithSFXOneShot((IsSprinting) ? "Running" : "Walking", SoundEffectBehaviour.Play);
                AudioManager.Instance.InteractWithSFXOneShot((!IsSprinting) ? "Running" : "Walking", SoundEffectBehaviour.Stop);
            }
            else
            {
                AudioManager.Instance.InteractWithSFXOneShot("Walking", SoundEffectBehaviour.Stop);
                AudioManager.Instance.InteractWithSFXOneShot("Running", SoundEffectBehaviour.Stop);
            }
        }
        public virtual void OnGrounded()
        {
            if (GravVel.y < 0f)
            {
                GravVel.y = -2f;
            }

            if (InputReciever.RequestingJump)
            {
                AudioManager.Instance.InteractWithSFX("Jumping", SoundEffectBehaviour.Play);
                GravVel.y = Mathf.Sqrt(JumpHeight * -2 * Gravity);
            }

            if(!Landed)
            {
                Landed = true;
                AudioManager.Instance.InteractWithSFX("Landing", SoundEffectBehaviour.Play);
            }
        }
        public virtual void OnNotGrounded()
        {
            GravVel.y += Gravity * Time.deltaTime;
            Landed = false;
        }

        public virtual void Reposition(Transform Destination)
        {
            transform.position = Destination.position;
            transform.rotation = Destination.rotation;
            XRig = Destination.rotation.x;
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(GroundChecker.position, 0.48f);
        }
    }
}