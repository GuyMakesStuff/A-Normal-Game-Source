using System.Collections;
using ANormalGame.Managers;
using UnityEngine;

namespace ANormalGame.LevelNavigation
{
    public class Level : MonoBehaviour
    {
        [System.Serializable]
        public class ElevatorPoint
        {
            public Transform Point;
            public Elevator elevator;

            public void UpdateElevator()
            {
                elevator.transform.position = Point.position;
                elevator.transform.rotation = Point.rotation;
            }
        }
        public Transform OrbPoint;
        public ElevatorPoint Entrance;
        public ElevatorPoint Exit;
        public bool IsExitOpen;

        // Start is called before the first frame update
        void OnEnable()
        {
            Load();
        }

        // Update is called once per frame
        void Update()
        {
            Entrance.UpdateElevator();
            Exit.UpdateElevator();
        }

        public void Load()
        {
            Entrance.elevator.door.SetOpen(true);
            Entrance.elevator.CanEnter = false;
            Entrance.UpdateElevator();
            Exit.elevator.door.SetOpen(IsExitOpen);
            Exit.elevator.CanEnter = true;
            Exit.UpdateElevator();
            SendPlayerToStart();
        }
        void SendPlayerToStart()
        {
            Entrance.elevator.PutInPlayer(LevelManager.Instance.Player);
        }
    }
}