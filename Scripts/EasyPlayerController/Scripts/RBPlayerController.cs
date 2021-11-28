using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyPlayerController
{
    [RequireComponent(typeof(Rigidbody))]
    public class RBPlayerController : PlayerController
    {
        Rigidbody Body;

        public override void MoveComponentInit()
        {
            base.MoveComponentInit();
            Body = GetComponent<Rigidbody>();
            Body.constraints = RigidbodyConstraints.FreezeRotation;
        }

        public override void RequestMove(Vector3 MoveVect, Vector3 GravVect)
        {
            base.RequestMove(MoveVect, GravVect);
            Body.velocity = new Vector3(MoveVect.x, GravVect.y, MoveVect.z);
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
            Body.isKinematic = true;
            base.Reposition(Destination);
            Body.isKinematic = false;
        }
    }
}