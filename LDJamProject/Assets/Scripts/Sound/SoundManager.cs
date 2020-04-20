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
        AmbientSource,
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
        foreach (AudioObject audio in ListOfAudioObjects)
        {
            audio.m_audioSource = ListOfAudioSources[(int)audio.AudioSourceType];

            audio.m_audioSource.clip = audio.m_Clip;
            audio.m_audioSource.loop = audio.m_Loop;
            audio.m_audioSource.volume = audio.m_Volume;
        }

        ///Play("Ambient");
       // Play("Combat");
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
    public bool Play(string audioName)
    {
        foreach (AudioObject audio in ListOfAudioObjects)
        {
            if (audio.m_AudioName == audioName)
            {
                audio.m_audioSource.Play();
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

    public bool PlayAudioWithPitch(string audioName)
    {
        foreach (AudioObject audio in ListOfAudioObjects)
        {
            if (audio.m_AudioName == audioName)
            {
                float tempPitch = audio.m_audioSource.pitch;

                audio.m_audioSource.pitch = Random.Range(0.5f, 2.0f);

                audio.m_audioSource.Play();

                // Might need to change it so it stores its starting pitch
                // incase this doesnt work
                audio.m_audioSource.pitch = tempPitch;
               
            }
        }


        return false;
    }
}
