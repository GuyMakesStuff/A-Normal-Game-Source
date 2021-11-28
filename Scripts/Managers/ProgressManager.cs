using System.Collections;
using System;
using UnityEngine;
using ANormalGame.IO;
using UnityEngine.SceneManagement;

namespace ANormalGame.Managers
{
    public class ProgressManager : Manager<ProgressManager>
    {
        [Serializable]
        public class Progress : Saveable
        {
            [NonSerialized]
            public const int LevelCount = 11;
            public int LevelAt;
            public bool[] OrbsCollected;
            public bool GameBeat;
            public bool GameBeatFull;
        }
        public Progress progress;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);

            Progress LoadedData = Saver.Load(progress) as Progress;
            if(LoadedData == null)
            {
                ResetSave();
            }
            else
            {
                progress = LoadedData;
            }

            SceneManager.LoadSceneAsync("TrailerLevel");
        }
        public void ResetSave()
        {
            progress.LevelAt = 0;
            progress.OrbsCollected = new bool[Progress.LevelCount];
            progress.GameBeat = false;
        }

        // Update is called once per frame
        void Update()
        {
            progress.Save();
        }

        public async void LoadLevel(int LevelIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
            while(!operation.isDone)
            {
                await System.Threading.Tasks.Task.Yield();
            }

            LevelManager levelManager = FindObjectOfType<LevelManager>();
            levelManager.LoadLevel(LevelIndex);
        }
    }
}