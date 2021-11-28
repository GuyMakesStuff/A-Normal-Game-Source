using ANormalGame.Managers;
using EasyPlayerController;
using UnityEngine;
using ANormalGame.Gameplay.PuzzleElements;

namespace ANormalGame.LevelNavigation
{
    [RequireComponent(typeof(BoxCollider))]
    public class Elevator : MonoBehaviour
    {
        BoxCollider Col;
        public bool CanEnter;
        public Door door;
        public Elevator OtherElevator;
        public Transform SpawnPoint;

        // Start is called before the first frame update
        void Awake()
        {
            Col = GetComponent<BoxCollider>();
            Col.enabled = CanEnter;
        }

        // Update is called once per frame
        void Update()
        {
            Col.enabled = CanEnter;
        }

        public void PutInPlayer(CCPlayerController player)
        {
            player.enabled = false;
            player.transform.position = SpawnPoint.position;
            player.transform.rotation = SpawnPoint.rotation;
            player.enabled = true;
            Camera.main.farClipPlane = 100000f;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                door.SetOpen(false);
                OtherElevator.door.SetOpen(false);
                CanEnter = false;
                LevelManager.Instance.LoadNextLevel();
            }
        }
    }
}