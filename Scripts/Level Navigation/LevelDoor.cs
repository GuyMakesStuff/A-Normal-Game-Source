using ANormalGame.Gameplay.PuzzleElements;
using ANormalGame.Managers;
using UnityEngine;

namespace ANormalGame.LevelNavigation
{
    public class LevelDoor : MonoBehaviour
    {
        public int LevelIndex;
        public TMPro.TMP_Text LevelText;
        public Door door;

        // Start is called before the first frame update
        void Start()
        {
            door.SetOpen(LevelIndex <= ProgressManager.Instance.progress.LevelAt);
        }

        // Update is called once per frame
        void Update()
        {
            LevelText.text = "Level " + LevelIndex.ToString("00");
            LevelText.color = (door.IsOpen) ? Color.green : Color.red;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                LoadLevel();
            }
        }
        async void LoadLevel()
        {
            door.SetOpen(false);
            await System.Threading.Tasks.Task.Delay(1000);
            ProgressManager.Instance.LoadLevel(LevelIndex);
        }
    }
}