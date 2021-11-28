using ANormalGame.Audio;
using TMPro;
using UnityEngine;

namespace ANormalGame.Gameplay.PuzzleElements
{
    public class Spawner : MonoBehaviour
    {
        public int SpawnCount;
        public Transform SpawnPoint;
        public GameObject SpawnParticles;
        public GameObject Prefab;
        public TMP_Text CountText;

        // Update is called once per frame
        void Update()
        {
            CountText.text = SpawnCount.ToString("00");
            CountText.color = (SpawnCount == 0) ? Color.red : Color.green;
        }

        public void Spawn()
        {
            if (SpawnCount > 0)
            {
                Destroy(Instantiate(SpawnParticles, SpawnPoint.position, Quaternion.identity), 5f);
                Instantiate(Prefab, SpawnPoint.position, Quaternion.identity);
                AudioManager.Instance.InteractWithSFX("Spawn", SoundEffectBehaviour.Play);
                SpawnCount--;
            }
            else
            {
                AudioManager.Instance.InteractWithSFX("Charge Down", SoundEffectBehaviour.Play);
            }
        }
    }
}