using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANormalGame.Gameplay
{
    public class Obstacle : MonoBehaviour
    {
        [HideInInspector]
        public bool CanHit = true;

        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" && CanHit)
            {
                Managers.GameManager.Instance.KillPlayer(other.GetComponent<EasyPlayerController.PlayerController>());
            }
        }
    }
}