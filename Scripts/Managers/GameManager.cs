using ANormalGame.Audio;
using EasyPlayerController;
using UnityEngine;

namespace ANormalGame.Managers
{
    public class GameManager : Manager<GameManager>
    {
        public GameObject DeathFX;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);
            AudioManager.Instance.SetMusicTrack("Main");
            AudioManager.Instance.InteractWithSFX("Indoors", SoundEffectBehaviour.Play);
            AudioManager.Instance.InteractWithSFX("Outdoors", SoundEffectBehaviour.Stop);
        }

        // Update is called once per frame
        void Update()
        {
            if (PlayerController.InputReciever.RequestingRestart)
            {
                PPManager.Instance.ResetDeadFX();
                AudioManager.Instance.InteractWithSFX("Select", SoundEffectBehaviour.Play);
                LevelManager.Instance.RestartLevel();
            }
        }

        public void KillPlayer(PlayerController player)
        {
            AudioManager.Instance.InteractWithSFX("Die", SoundEffectBehaviour.Play);
            Instantiate(DeathFX, player.transform.position, player.transform.rotation);
            Destroy(player.gameObject);
            GlobalUIManager.Instance.SetGameOverTextVisible(true);
            PPManager.Instance.IsDead = true;
        }
    }
}