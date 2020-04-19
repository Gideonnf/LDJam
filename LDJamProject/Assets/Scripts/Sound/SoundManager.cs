using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class AudioObject
//{
//    public string name;
//    public AudioClip audioFile;
//    public bool isLooping = false;
//    public bool isRepeatable = false;
//}

public class SoundManager : SingletonBase<SoundManager>
{
    AudioSource audioSource;

    public enum AudioSourceType
    {
        BackgroundSource,
        SFXSource,
        ExtraSource,
        TotalAudioSources
    };

    [Tooltip("List of audio sources to allow multiple music?")]
    public List<AudioSource> ListOfAudioSources = new List<AudioSource>();
    
    [Tooltip("List of audio objects")]
    public List<AudioObject> ListOfAudioObjects = new List<AudioObject>();

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Plays an audio from the audio list 
    /// </summary>
    /// <param name="audioName"> Audio to be played </param>
    /// <param name="audioSource"> Source to play on </param>
    /// <returns></returns>
    public bool Play(string audioName, int audioSource)
    {
        foreach (AudioObject audio in ListOfAudioObjects)
        {
            if (audio.m_AudioName == audioName)
            {
                ListOfAudioSources[audioSource].clip = audio.m_Clip;

                ListOfAudioSources[audioSource].loop = audio.m_Loop;

                ListOfAudioSources[audioSource].volume = audio.m_Volume;

                ListOfAudioSources[audioSource].Play();
            }
        }

        return false;
    }

    public bool PauseAudio (int audioSource)
    {
        if (ListOfAudioSources[audioSource].isPlaying)
        {
            ListOfAudioSources[audioSource].Pause();
            return true;
        }

        return false;
    }

    public bool PlayAudioWithPitch(string audioName, int audioSource)
    {
        foreach (AudioObject audio in ListOfAudioObjects)
        {
            if (audio.m_AudioName == audioName)
            {
                ListOfAudioSources[audioSource].clip = audio.m_Clip;

                ListOfAudioSources[audioSource].loop = audio.m_Loop;

                // Rand a pitch for the audio source between 0.5 to 2.0f
                ListOfAudioSources[audioSource].pitch = Random.Range(0.5f, 2.0f);

                ListOfAudioSources[audioSource].Play();
            }
        }


        return false;
    }
}
