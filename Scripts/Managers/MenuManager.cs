using ANormalGame.Audio;
using EasyPlayerController;
using UnityEngine;
using ANormalGame.LevelNavigation;

namespace ANormalGame.Managers
{
    public class MenuManager : Manager<MenuManager>
    {
        [Header("Game Quit")]
        public CCPlayerController PlayerController;
        public float QuitHeightLimit;
        [Header("Levels")]
        public GameObject LevelDoorPrefab;
        public GameObject HallwayEndPrefab;
        public Transform StartLevelDoorPos;
        public Transform LevelDoorContainer;
        Vector3 LevelDoorPos;
        
        // Start is called before the first frame update
        void Start()
        {
            Init(this);

            AudioManager.Instance.SetMusicTrack("Menu");
            AudioManager.Instance.InteractWithSFX("Indoors", SoundEffectBehaviour.Stop);
            AudioManager.Instance.InteractWithSFX("Outdoors", SoundEffectBehaviour.Stop);

            InitLevelDoors();
        }
        void InitLevelDoors()
        {
            LevelDoorPos = StartLevelDoorPos.position;

            for (int LD = 0; LD < ProgressManager.Progress.LevelCount; LD++)
            {
                GameObject LDoor = Instantiate(LevelDoorPrefab, LevelDoorPos, Quaternion.identity);
                LDoor.name = "Level Door " + LD.ToString("00");
                LDoor.transform.SetParent(LevelDoorContainer);
                LDoor.GetComponentInChildren<LevelDoor>().LevelIndex = LD;
                LevelDoorPos.x -= 5f;
            }

            Instantiate(HallwayEndPrefab, LevelDoorPos, Quaternion.identity, LevelDoorContainer).name = "Level Door Hallway End";
        }

        // Update is called once per frame
        void Update()
        {
            if(PlayerController.transform.position.y <= -QuitHeightLimit)
            {
                QuitGame();
            }
        }
        void QuitGame()
        {
            QuitManager.Instance.QuitGame();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(0, -QuitHeightLimit, 0), 1f);
        }
    }
}