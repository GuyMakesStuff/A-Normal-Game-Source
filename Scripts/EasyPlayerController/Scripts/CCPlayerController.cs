using System.Collections;
using UnityEngine;

namespace EasyPlayerController
{
    [RequireComponent(typeof(CharacterController))]
    public class CCPlayerController : PlayerController
    {
        CharacterController CC;

        public override void MoveComponentInit()
        {
            base.MoveComponentInit();
            CC = GetComponent<CharacterController>();
        }

        public override void RequestMove(Vector3 MoveVect, Vector3 GravVect)
        {
            base.RequestMove(MoveVect, GravVect);
            CC.Move(MoveVect * Time.deltaTime);
            CC.Move(GravVect * Time.deltaTime);
        }

        public override void OnGrounded()
        {
            base.OnGrounded();
        }
        public override void OnNotGrounded()
        {
            base.OnNotGrounded();
        }

        public override void Reposition(Transform Destination)
        {
            CC.enabled = false;
            base.Reposition(Destination);
            CC.enabled = true;
        }
    }
}