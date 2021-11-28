using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANormalGame.Gameplay.PuzzleElements
{
    [RequireComponent(typeof(Rigidbody))]
    public class UnlockableRB : MonoBehaviour
    {
        Rigidbody Body;

        // Start is called before the first frame update
        void Start()
        {
            Body = GetComponent<Rigidbody>();
            Body.isKinematic = true;
        }

        // Update is called once per frame
        public void Unlock()
        {
            Body.isKinematic = false;
        }
    }
}