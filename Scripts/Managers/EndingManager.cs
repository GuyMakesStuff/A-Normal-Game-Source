using ANormalGame.Audio;
using ANormalGame.Gameplay.PuzzleElements;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace ANormalGame.Managers
{
    public class EndingManager : Manager<EndingManager>
    {
        [Header("Doors")]
        public Door FacillityExitDoor;
        public Door TrophyStandDoor;
        public Door OrbsEndingDoor;
        
        [Header("Navigation")]
        public Transform Trophy;
        public float TrophyClaimDistance;
        [System.Obsolete("Was Used For Testing. Use ProgressManager.Instance.progress.GameBeat Instead")]
        bool IsClaimed;
        public TMP_Text OrbsCollectedText;
        public Transform Player;
        public Transform OrbsEndingLoadPoint;
        public float OrbsEndingLoadDist;
        bool LoadingOrbsEnding;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);

            AudioManager.Instance.MuteMusic();
            AudioManager.Instance.InteractWithSFX("Indoors", SoundEffectBehaviour.Stop);
            AudioManager.Instance.InteractWithSFX("Outdoors", SoundEffectBehaviour.Play);

            FacillityExitDoor.SetOpen(true);
            TrophyStandDoor.SetOpen(true);
        }

        // Update is called once per frame
        void Update()
        {
            OrbsCollectedText.text = "Orbs Collected:\n<size=250%>" + OrbsCollected().ToString() + "/11";
            OrbsCollectedText.color = (OrbsCollected() == ProgressManager.Progress.LevelCount) ? Color.green : Color.red;

            if(Vector3.Distance(Player.position, Trophy.position) < TrophyClaimDistance && !ProgressManager.Instance.progress.GameBeat)
            {
                AudioManager.Instance.InteractWithSFX("Request Count Reach", SoundEffectBehaviour.Play);
                ProgressManager.Instance.progress.GameBeat = true;
            }

            if(Vector3.Distance(Player.position, OrbsEndingLoadPoint.position) < OrbsEndingLoadDist && !LoadingOrbsEnding)
            {
                LoadOrbsEnding();
            }
        }
        public void OpenDoors()
        {
            TrophyStandDoor.SetOpen(false);
            OrbsEndingDoor.SetOpen(OrbsCollected() == (ProgressManager.Progress.LevelCount));
        }

        public int OrbsCollected()
        {
            int Amount = 0;
            for (int O = 0; O < ProgressManager.Instance.progress.OrbsCollected.Length; O++)
            {
                if(ProgressManager.Instance.progress.OrbsCollected[O])
                {
                    Amount++;
                }
            }
            return Amount;
        }

        async void LoadOrbsEnding()
        {
            LoadingOrbsEnding = true;
            OrbsEndingDoor.SetOpen(false);
            await System.Threading.Tasks.Task.Delay(1000);
            SceneManager.LoadSceneAsync("OrbsEnding");
        }

        public void Exit()
        {
            QuitManager.Instance.QuitGame();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Trophy.position, TrophyClaimDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(OrbsEndingLoadPoint.position, OrbsEndingLoadDist);
        }
    }
}