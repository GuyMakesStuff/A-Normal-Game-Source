using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace ANormalGame.Managers
{
    [RequireComponent(typeof(PostProcessVolume))]
    public class PPManager : Manager<PPManager>
    {
        PostProcessVolume Vol;
        public bool Enabled = true;
        [Header("Profiles")]
        public PostProcessProfile Profile;
        public PostProcessProfile EndProfile;
        ColorGrading colorGrading;
        ChromaticAberration chromatic;
        [HideInInspector]
        public bool IsDead;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);

            Vol = GetComponent<PostProcessVolume>();
            Vol.isGlobal = true;
        }

        // Update is called once per frame
        void Update()
        {
            Vol.enabled = Enabled;

            Vol.profile = (SceneManager.GetActiveScene().name == "Ending") ? EndProfile : Profile;
            colorGrading = Vol.profile.GetSetting<ColorGrading>();
            chromatic = Vol.profile.GetSetting<ChromaticAberration>();

            if (IsDead)
            {
                if(chromatic.intensity.value < 1f)
                {
                    chromatic.intensity.value += (0.9f / 5f) * Time.deltaTime;
                }

                if(colorGrading.saturation.value > -50f)
                {
                    colorGrading.saturation.value -= 10f * Time.deltaTime;
                }
            }
        }

        public void ResetDeadFX()
        {
            IsDead = false;
            chromatic.intensity.value = 0.1f;
            colorGrading.saturation.value = 0f;
        }
    }
}