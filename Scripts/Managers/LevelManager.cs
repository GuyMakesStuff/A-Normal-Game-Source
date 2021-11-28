using ANormalGame.Gameplay;
using EasyPlayerController;
using UnityEngine;
using ANormalGame.LevelNavigation;
using UnityEngine.SceneManagement;

namespace ANormalGame.Managers
{
    public class LevelManager : Manager<LevelManager>
    {
        public Level[] Levels;
        public int LevelIndex;
        public CCPlayerController Player;
        public Orb orb;

        void Awake()
        {
            Init(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            LoadLevel(LevelIndex);
        }

        // Update is called once per frame
        void Update()
        {
            if (LevelIndex < Levels.Length)
            {
                orb.gameObject.SetActive(ProgressManager.Instance.progress.OrbsCollected[LevelIndex] == false);
                orb.transform.position = Levels[LevelIndex].OrbPoint.position;
            }

            if (LevelIndex > ProgressManager.Instance.progress.LevelAt)
            {
                ProgressManager.Instance.progress.LevelAt = LevelIndex;
            }
        }

        public void LoadLevel(int LevelNum)
        {
            if(LevelNum < 0)
            {
                Debug.LogError("Level Index Must Be Grater Than 0!");
                return;
            }

            if(LevelNum == Levels.Length)
            {
                Debug.Log("Test");
                SceneManager.LoadScene("Ending");
            }

            LevelIndex = LevelNum;
            for (int L = 0; L < Levels.Length; L++)
            {
                Levels[L].gameObject.SetActive(L == LevelNum);
            }
        }
        public async void LoadNextLevel()
        {
            await System.Threading.Tasks.Task.Delay(1000);
            LoadLevel(LevelIndex + 1);
        }
        public void RestartLevel()
        {
            GlobalUIManager.Instance.SetGameOverTextVisible(false);
            ProgressManager.Instance.LoadLevel(LevelIndex);
        }
    }
}