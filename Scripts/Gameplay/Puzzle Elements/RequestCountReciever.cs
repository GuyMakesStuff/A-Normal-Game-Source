using ANormalGame.Audio;
using UnityEngine.Events;
using UnityEngine;

namespace ANormalGame.Gameplay.PuzzleElements
{
    public class RequestCountReciever : MonoBehaviour
    {
        [HideInInspector]
        public int Count;
        public int MaxCount;
        public UnityEvent OnMaxCountReached;
        public UnityEvent OnMaxCountStay;
        public UnityEvent OnRelease;
        [HideInInspector]
        public bool CountReached;

        // Update is called once per frame
        void Update()
        {
            if(CountReached)
            {
                OnMaxCountStay.Invoke();
            }

            if (Count >= MaxCount && !CountReached)
            {
                CountReached = true;
                AudioManager.Instance.InteractWithSFX("Request Count Reach", SoundEffectBehaviour.Play);
                OnMaxCountReached.Invoke();
            }
            else if(Count < MaxCount && CountReached)
            {
                CountReached = false;
                OnRelease.Invoke();
            }
        }

        public void AddReqest()
        {
            Count++;
        }
        public void RemoveRequest()
        {
            Count--;
        }
    }
}