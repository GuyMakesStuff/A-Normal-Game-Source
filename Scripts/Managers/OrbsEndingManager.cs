using ANormalGame.Audio;
using ANormalGame.Gameplay.PuzzleElements;
using UnityEngine;

namespace ANormalGame.Managers
{
    public class OrbsEndingManager : Manager<OrbsEndingManager>
    {
        [Header("Navigation")]
        public Door EntranceDoor;

        [Header("Light")]
        public Light RoomLight;
        public Gradient Cols;
        public float ColChangeSpeed;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);

            EntranceDoor.SetOpen(true);

            AudioManager.Instance.SetMusicTrack("Orbs Ending");
            AudioManager.Instance.InteractWithSFX("Indoors", SoundEffectBehaviour.Stop);
            AudioManager.Instance.InteractWithSFX("Outdoors", SoundEffectBehaviour.Stop);

            ProgressManager.Instance.progress.GameBeatFull = true;
        }

        // Update is called once per frame
        void Update()
        {
            float T = Mathf.PingPong(Time.time * ColChangeSpeed, 1f);
            RoomLight.color = Cols.Evaluate(T);
        }
    }
}