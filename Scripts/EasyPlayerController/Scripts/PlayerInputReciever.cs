using System.Collections;
using ANormalGame.Managers;
using UnityEngine;

namespace EasyPlayerController
{
    public class PlayerInputReciever : MonoBehaviour
    {
        [Header("Mouse")]
        public bool LockMouse;
        public string MouseXAxis = "Mouse X";
        [HideInInspector]
        public float MouseX;
        public string MouseYAxis = "Mouse Y";
        [HideInInspector]
        public float MouseY;

        [Header("Movement")]
        public bool Smooth;
        public string HoriAxis = "Horizontal";
        [HideInInspector]
        public float Hori;
        public string VertAxis = "Vertical";
        [HideInInspector]
        public float Vert;

        [Header("Keybinds")]
        public KeyCode JumpKey = KeyCode.Space;
        [HideInInspector]
        public bool RequestingJump;
        public KeyCode SprintKey = KeyCode.LeftShift;
        [HideInInspector]
        public bool SprintKeyHold;
        public KeyCode RestartKey = KeyCode.R;
        [HideInInspector]
        public bool RequestingRestart;

        // Update is called once per frame
        void Update()
        {
            if (GlobalUIManager.IsInstanced)
            {
                if (!GlobalUIManager.Instance.IsPaused)
                {
                    Cursor.lockState = (LockMouse) ? CursorLockMode.Locked : CursorLockMode.None;
                    Cursor.visible = !LockMouse;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
            MouseX = Input.GetAxis(MouseXAxis);
            MouseY = Input.GetAxis(MouseYAxis);

            Hori = (!Smooth) ? Input.GetAxisRaw(HoriAxis) : Input.GetAxis(HoriAxis);
            Vert = (!Smooth) ? Input.GetAxisRaw(VertAxis) : Input.GetAxis(VertAxis);

            RequestingJump = Input.GetKeyDown(JumpKey);
            SprintKeyHold = Input.GetKey(SprintKey);
            RequestingRestart = Input.GetKeyDown(RestartKey);
        }
    }
}