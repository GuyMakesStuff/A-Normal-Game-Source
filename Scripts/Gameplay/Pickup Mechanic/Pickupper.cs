using ANormalGame.Audio;
using UnityEngine.UI;
using UnityEngine;

namespace ANormalGame.Gameplay.PickupMechanic
{
    public class Pickupper : MonoBehaviour
    {
        public float MaxDist;
        public float SmoothSpeed;
        public Image Indecator;
        float Dist;
        bool IsHit;
        bool CanPickUp;
        RaycastHit hit;
        Pickuppuble CurPicked;

        // Start is called before the first frame update
        void Start()
        {
            CurPicked = null;
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            IsHit = Physics.Raycast(ray, out hit, MaxDist, -5, QueryTriggerInteraction.Ignore);

            if (Input.GetMouseButtonDown(0))
            {
                TryPickup();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Release();
            }

            if (CurPicked != null)
            {
                Indecator.color = Color.green;
            }
            else if (IsHit && CanPickUp)
            {
                Indecator.color = Color.white;
            }
            else if ((!IsHit && !CanPickUp && CurPicked == null) || (IsHit && !CanPickUp && CurPicked == null))
            {
                Indecator.color = new Color(1, 1, 1, 0.5f);
            }

            if (hit.collider == null)
            {
                CanPickUp = false;
                return;
            }
            Pickuppuble pickuppuble = hit.collider.GetComponent<Pickuppuble>();
            CanPickUp = (pickuppuble != null && hit.collider != null);
        }

        void FixedUpdate()
        {
            if (CurPicked != null)
            {
                Vector3 Pos = (IsHit) ? hit.point : transform.position + (transform.forward * MaxDist);
                Vector3 Gap = Pos - (CurPicked.Body.position);
                CurPicked.Body.AddForce(((Gap * Gap.magnitude) + -CurPicked.Body.velocity) * SmoothSpeed, ForceMode.Acceleration);
            }
        }

        void TryPickup()
        {
            if (CanPickUp)
            {
                Pickuppuble pickuppuble = hit.collider.GetComponent<Pickuppuble>();
                pickuppuble.IsPicked = true;
                Dist = hit.distance;
                CurPicked = pickuppuble;
                AudioManager.Instance.InteractWithSFX("Pick Up", SoundEffectBehaviour.Play);
            }
        }
        void Release()
        {
            if (CurPicked != null)
            {
                CurPicked.IsPicked = false;
                CurPicked = null;
                AudioManager.Instance.InteractWithSFX("Pick Up", SoundEffectBehaviour.Play);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward * MaxDist));
        }
    }
}