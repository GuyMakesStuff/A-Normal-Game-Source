using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANormalGame.Gameplay.PickupMechanic
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Pickuppuble : MonoBehaviour
    {
        [HideInInspector]
        public Rigidbody Body;
        [HideInInspector]
        public bool IsPicked;
        Vector3 PrevVelocity;

        // Start is called before the first frame update
        void Start()
        {
            Body = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            gameObject.layer = (IsPicked) ? 2 : 3;
            Body.useGravity = !IsPicked;
            // Body.constraints = (IsPicked) ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.None;
        }
    }
}