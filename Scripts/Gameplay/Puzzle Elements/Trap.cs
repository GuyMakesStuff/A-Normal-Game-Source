using ANormalGame.Audio;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

namespace ANormalGame.Gameplay.PuzzleElements
{
    public class Trap : MonoBehaviour
    {
        public GameObject Spikes;
        Obstacle obstacle;
        Animator Anim;
        BoxCollider Col;
        bool IsPlaying;
        public float ChargeTime;
        public float HoldTime;
        public float RechargeTime;

        // Start is called before the first frame update
        void Start()
        {
            Col = GetComponent<BoxCollider>();
            obstacle = Spikes.GetComponent<Obstacle>();
            Anim = Spikes.GetComponent<Animator>();
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(TryAttack());
            }
        }

        IEnumerator TryAttack()
        {
            IsPlaying = true;
            ResumeCycle();
            AudioManager.Instance.InteractWithSFX("Spikes Charge", SoundEffectBehaviour.Play);
            yield return new WaitForSeconds(ChargeTime);
            obstacle.CanHit = true;
            ResumeCycle();
            AudioManager.Instance.InteractWithSFX("Spikes Attack", SoundEffectBehaviour.Play);
            yield return new WaitForSeconds(HoldTime);
            obstacle.CanHit = false;
            ResumeCycle();
            AudioManager.Instance.InteractWithSFX("Spikes Recharge", SoundEffectBehaviour.Play);
            yield return new WaitForSeconds(RechargeTime);
            ResetCycle();
            IsPlaying = false;
            AudioManager.Instance.InteractWithSFX("Spikes Recharge", SoundEffectBehaviour.Play);
        }
        void ResumeCycle()
        {
            int CurCycle = Anim.GetInteger("CycleCount");
            Anim.SetInteger("CycleCount", CurCycle + 1);
        }
        void ResetCycle()
        {
            Anim.SetInteger("CycleCount", 0);
        }

        // Update is called once per frame
        void Update()
        {
            Col.enabled = !IsPlaying;
        }
    }
}