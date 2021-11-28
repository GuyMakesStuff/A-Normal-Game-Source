using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using ANormalGame.Audio;

namespace ANormalGame.Gameplay
{
    public class TV : MonoBehaviour
    {
        [HideInInspector]
        public bool IsPlayed;
        public float DetectionRadius;
        public float InitialDelay;
        [System.Serializable]
        public class Sentence
        {
            [TextArea(3, 10)]
            public string Text;
            public float NextDelay;
        }
        public Sentence[] Sentences;
        public float CharDelay;
        public TMP_Text TextDisplay;
        Transform Player;
        public MeshRenderer screen;
        public UnityEvent OnVidEnd;
        Material Mat;
        bool CanSkip;

        // Start is called before the first frame update
        void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            Mat = new Material(screen.material);
            Mat.name = "Screen Mat_" + GetHashCode();
            screen.material = Mat;
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, Player.position) < DetectionRadius && !IsPlayed)
            {
                Play();
            }

            if (Input.GetKeyDown(KeyCode.Tab) && CanSkip)
            {
                CanSkip = false;
                AudioManager.Instance.InteractWithSFX("Dialog Start", SoundEffectBehaviour.Stop);
                AudioManager.Instance.InteractWithSFX("Skip Dialog", SoundEffectBehaviour.Play);
                StopAllCoroutines();
                TextDisplay.text = Sentences[Sentences.Length - 1].Text;
                OnVidEnd.Invoke();
            }
        }

        void Play()
        {
            IsPlayed = true;
            Mat.color = Color.white;
            Mat.SetColor("_EmissionColor", Color.white);
            StartCoroutine(play());
        }
        IEnumerator play()
        {
            CanSkip = true;
            TextDisplay.text = "";
            AudioManager.Instance.InteractWithSFX("Dialog Start", SoundEffectBehaviour.Play);
            yield return new WaitForSeconds(InitialDelay);

            for (int S = 0; S < Sentences.Length; S++)
            {
                TextDisplay.text = "";
                char[] Chars = Sentences[S].Text.ToCharArray();
                foreach (char C in Chars)
                {
                    TextDisplay.text += C;
                    AudioManager.Instance.InteractWithSFX("Dialog Talk", SoundEffectBehaviour.Play);
                    yield return new WaitForSeconds(CharDelay);
                }

                yield return new WaitForSeconds(Sentences[S].NextDelay);
            }

            OnVidEnd.Invoke();
            CanSkip = false;
        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, DetectionRadius);
        }
    }
}