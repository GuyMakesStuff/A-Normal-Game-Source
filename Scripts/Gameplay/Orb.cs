using ANormalGame.Audio;
using ANormalGame.Managers;
using UnityEngine;

namespace ANormalGame.Gameplay
{
    public class Orb : MonoBehaviour
    {
        public float RotSpeed;
        public GameObject CollectFX;

        [Header("Light")]
        public Light L;
        public float LightIntensity;

        [Header("Color")]
        public Gradient Colors;
        public float ColorChangeSpeed;
        public Material Mat;
        Material NewMat;

        // Start is called before the first frame update
        void Start()
        {
            NewMat = new Material(Shader.Find("Standard"));
            NewMat.name = "Orb Mat_" + GetHashCode();
            NewMat.CopyPropertiesFromMaterial(Mat);
            GetComponent<MeshRenderer>().sharedMaterial = NewMat;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.one, RotSpeed * Time.deltaTime);

            L.intensity = LightIntensity;

            float T = Mathf.PingPong(Time.time * ColorChangeSpeed, 1f);
            Color Col = Colors.Evaluate(T);
            NewMat.color = Col;
            NewMat.SetColor("_EmissionColor", Col);
            L.color = Col;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                AudioManager.Instance.InteractWithSFX("Orb Collect", SoundEffectBehaviour.Play);
                ProgressManager.Instance.progress.OrbsCollected[LevelManager.Instance.LevelIndex] = true;
                Destroy(Instantiate(CollectFX, transform.position, Quaternion.identity), 5f);
            }
        }
    }
}