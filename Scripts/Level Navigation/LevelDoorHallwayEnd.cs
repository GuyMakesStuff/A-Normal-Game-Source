using System.Collections;
using ANormalGame.Managers;
using UnityEngine;

namespace ANormalGame.LevelNavigation
{
    public class LevelDoorHallwayEnd : MonoBehaviour
    {
        public GameObject NormalEndTrophy;
        public GameObject FullEndCake;

        // Update is called once per frame
        void Update()
        {
            NormalEndTrophy.SetActive(ProgressManager.Instance.progress.GameBeat);
            FullEndCake.SetActive(ProgressManager.Instance.progress.GameBeatFull);
        }
    }
}