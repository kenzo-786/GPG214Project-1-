using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

namespace Gamekit2D
{
    [RequireComponent(typeof(Slider))]
    public class SFXVolumeMixerSliderLink : MonoBehaviour
    {
        public AudioMixer mixer;
        public string mixerParameter;

        public float maxAttenuation = 0.0f;
        public float minAttenuation = -80.0f;

        protected Slider m_Slider;
        public float value;
        public string filePath;
        public string folderPath;

        void Awake()
        {
            filePath = Path.Combine(Application.dataPath, "SFXVolumeData.jsonMasterVolume");
            folderPath = Path.Combine(Application.persistentDataPath, "AudioData");
            m_Slider = GetComponent<Slider>();
            LoadMasterVolume();

            mixer.GetFloat(mixerParameter, out value);

            m_Slider.value = (value - minAttenuation) / (maxAttenuation - minAttenuation);
            m_Slider.onValueChanged.AddListener(SliderValueChange);
        }

        void SliderValueChange(float value)
        {
            mixer.SetFloat(mixerParameter, minAttenuation + value * (maxAttenuation - minAttenuation));
            SaveMasterVolume();
        }

        public void SaveMasterVolume()
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            SFX_Data data = new SFX_Data();
            data.sfxVolumeLevel = m_Slider.value;

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);
            Debug.Log(json);
        }

        public void LoadMasterVolume()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Debug.Log(json);
                SFX_Data data = JsonUtility.FromJson<SFX_Data>(json);

                m_Slider.value = data.sfxVolumeLevel;
                float mixerValue = minAttenuation + data.sfxVolumeLevel * (maxAttenuation - minAttenuation);
                mixer.SetFloat(mixerParameter, mixerValue);
            }
        }
    }
}
