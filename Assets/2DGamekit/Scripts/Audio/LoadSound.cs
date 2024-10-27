using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadSound : MonoBehaviour
{
    public string fileName = "coin.wav";
    public string folderName = "Audio";
    public string combinedFilePath;
    public AudioSource audioSource;
    public AudioClip clip;

    private void Start()
    {
        combinedFilePath = Path.Combine(Application.streamingAssetsPath, folderName, fileName);
        Debug.Log(combinedFilePath);
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            return;
        }

        LoadSoundFile();

    }

    void LoadSoundFile()
    {
        if(File.Exists(combinedFilePath))
        {
            byte[] audioData = File.ReadAllBytes(combinedFilePath);
            float[] floatArray = new float[audioData.Length / 2];   

            for (int i = 0; i < floatArray.Length; i++)
            {
                short bitValue = System.BitConverter.ToInt16(audioData, i * 2);
                floatArray[i] = bitValue / 32768.0f;
            }

            clip = AudioClip.Create("Clip", floatArray.Length, 1, 44100, false);

            clip.SetData(floatArray, 0);
        }
    }

    void PlaySound()
    {
        if (audioSource == null || clip == null) 
        {
            return;
        }
        audioSource.PlayOneShot(clip);
        audioSource.clip = clip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            PlaySound();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlaySound();
        }
    }
}
