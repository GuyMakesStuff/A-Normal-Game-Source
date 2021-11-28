using EasyPlayerController;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using ANormalGame.IO;
using System.Collections.Generic;
using ANormalGame.Audio;

namespace ANormalGame.Managers
{
    public class GlobalUIManager : Manager<GlobalUIManager>
    {
        [Header("Pause")]
        [Space]
        public bool IsPaused;
        [HideInInspector]
        public bool CanPause;
        public GameObject UIPanel;
        public GameObject MenuButton;

        [Header("Settings")]
        public Slider MusicSlider;
        public Slider SFXSlider;
        public TMP_Dropdown QualDropdown;
        public TMP_Dropdown ResDropdown;
        Resolution[] Resolutions;
        public Toggle FSToggle;
        public Toggle PPToggle;
        public Slider FOVSlider;
        public Slider SensSlider;
        public Toggle MouseLockToggle;
        [System.Serializable]
        public class Settings : Saveable
        {
            public float MusicVol;
            public float SFXVol;
            public int QualLevel;
            public int ResIndex;
            public bool FS;
            public bool PP;
            public float FOV;
            public float Sens;
            public bool ML;
        }
        public Settings settings;

        [Header("Other")]
        public Animator GameOverText;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);

            SetGameOverTextVisible(false);

            ResDropdown.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<int>(UpdateRes));
            FSToggle.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<bool>(UpdateFS));

            int CurResIndex = 0;
            InitRes(ref CurResIndex);
            Settings LoadedSettings = Saver.Load(settings) as Settings;
            if(LoadedSettings == null)
            {
                MusicSlider.value = AudioManager.Instance.GetMusicVolume();
                SFXSlider.value = AudioManager.Instance.GetSFXVolume();
                QualDropdown.value = QualitySettings.GetQualityLevel();
                ResDropdown.value = CurResIndex;
                FSToggle.isOn = Screen.fullScreen;
                PPToggle.isOn = PPManager.Instance.Enabled;
                FOVSlider.value = 60f;
                SensSlider.value = 100f;
                MouseLockToggle.isOn = true;
            }
            else
            {
                settings = LoadedSettings;
                MusicSlider.value = settings.MusicVol;
                SFXSlider.value = settings.SFXVol;
                QualDropdown.value = settings.QualLevel;
                ResDropdown.value = settings.ResIndex;
                FSToggle.isOn = settings.FS;
                PPToggle.isOn = settings.PP;
                FOVSlider.value = settings.FOV;
                SensSlider.value = settings.Sens;
                MouseLockToggle.isOn = settings.ML;
            }

            CanPause = true;
        }
        void InitRes(ref int CurResIndexOutput)
        {
            Resolutions = Screen.resolutions;
            ResDropdown.ClearOptions();
            List<string> Res2String = new List<string>();
            for (int R = 0; R < Resolutions.Length; R++)
            {
                Resolution Res = Resolutions[R];
                string String = Res.width + "x" + Res.height;
                Res2String.Add(String);

                Resolution CurRes = Screen.currentResolution;
                if(Res.width == CurRes.width && Res.height == CurRes.height)
                {
                    CurResIndexOutput = R;
                }
            }
            ResDropdown.AddOptions(Res2String);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown("escape") && CanPause)
            {
                if (!IsPaused)
                {
                    AudioManager.Instance.InteractWithSFX("Select", SoundEffectBehaviour.Play);
                    IsPaused = true;
                }
            }
            UIPanel.SetActive(IsPaused);
            Time.timeScale = (IsPaused) ? 0f : 1f;
            AudioManager.Instance.InteractWithAllSFX((IsPaused) ? SoundEffectBehaviour.Pause : SoundEffectBehaviour.Resume);
            AudioManager.Instance.InteractWithMusic((IsPaused) ? SoundEffectBehaviour.Pause : SoundEffectBehaviour.Resume);

            MenuButton.SetActive(!(SceneManager.GetActiveScene().name == "Menu"));

            AudioManager.Instance.SetMusicVolume(MusicSlider.value);
            settings.MusicVol = MusicSlider.value;
            AudioManager.Instance.SetSFXVolume(SFXSlider.value);
            settings.SFXVol = SFXSlider.value;
            QualitySettings.SetQualityLevel(QualDropdown.value);
            settings.QualLevel = QualDropdown.value;
            settings.ResIndex = ResDropdown.value;
            settings.FS = FSToggle.isOn;
            PPManager.Instance.Enabled = PPToggle.isOn;
            settings.PP = PPToggle.isOn;
            Camera.main.fieldOfView = FOVSlider.value;
            settings.FOV = FOVSlider.value;
            FindObjectOfType<PlayerController>().Sens = SensSlider.value;
            settings.Sens = SensSlider.value;
            PlayerController.InputReciever.LockMouse = MouseLockToggle.isOn;
            settings.ML = MouseLockToggle.isOn;

            settings.Save();
        }
        void UpdateRes(int Index)
        {
            UpdateScreen();
        }
        void UpdateFS(bool value)
        {
            UpdateScreen();
        }
        void UpdateScreen()
        {
            Resolution Res = Resolutions[ResDropdown.value];
            Screen.SetResolution(Res.width, Res.height, FSToggle.isOn);
        }

        public void Resume()
        {
            AudioManager.Instance.InteractWithSFX("Select", SoundEffectBehaviour.Play);
            IsPaused = false;
        }
        public void ClearSave()
        {
            ProgressManager.Instance.ResetSave();
            Menu();
        }
        public void Menu()
        {
            Resume();
            SetGameOverTextVisible(false);
            PPManager.Instance.ResetDeadFX();
            SceneManager.LoadSceneAsync("Menu");
        }

        public void SetGameOverTextVisible(bool Value)
        {
            GameOverText.SetBool("IsShowen", Value);
        }
    }
}