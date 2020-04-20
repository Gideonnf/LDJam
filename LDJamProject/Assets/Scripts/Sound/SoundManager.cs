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
    [SerializeField] GameObject SFXObject;
    [SerializeField] GameObject PermanentAudioObj;

    [Tooltip("List of audio objects")]
    public List<AudioObject> ListOfAudioObjects = new List<AudioObject>();

    // Start is called before the first frame update
    void Start()
    {

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
    public GameObject Play(string audioName)
    {
        foreach (AudioObject audio in ListOfAudioObjects)
        {
            if (audio.m_AudioName == audioName)
            {
                if (audio.isSFX)
                {
                    GameObject tempSound = Instantiate(SFXObject);
                    tempSound.GetComponent<AudioSource>().clip = audio.m_Clip;
                    tempSound.GetComponent<AudioSource>().volume = audio.m_Volume;
                    tempSound.GetComponent<AudioSource>().pitch = audio.m_Pitch;
                    tempSound.GetComponent<AudioSource>().loop = audio.m_Loop;
                    tempSound.GetComponent<AudioSource>().Play();
                    return tempSound;
                }
                else
                {
                    GameObject tempSound = Instantiate(PermanentAudioObj);
                    tempSound.GetComponent<AudioSource>().clip = audio.m_Clip;
                    tempSound.GetComponent<AudioSource>().volume = audio.m_Volume;
                    tempSound.GetComponent<AudioSource>().pitch = audio.m_Pitch;
                    tempSound.GetComponent<AudioSource>().loop = audio.m_Loop;
                    tempSound.GetComponent<AudioSource>().Play();
                    return tempSound;
                }
            }
        }

        return null;
    }

    //public bool PauseAudio(string audioName)
    //{
    //    foreach (AudioObject audio in ListOfAudioObjects)
    //    {
    //        if (audio.m_AudioName == audioName)
    //        {
    //            audio.m_audioSource.Pause();
    //            return true;
    //        }
    //    }
    //
    //    return false;
    //}

    public bool PlaySFXWithPitch(string audioName, float range)
    {
        foreach (AudioObject audio in ListOfAudioObjects)
        {
            if (audio.m_AudioName == audioName)
            {
                if (audio.isSFX)
                {
                    GameObject tempSound = Instantiate(SFXObject);
                    tempSound.GetComponent<AudioSource>().clip = audio.m_Clip;
                    tempSound.GetComponent<AudioSource>().volume = audio.m_Volume;
                    tempSound.GetComponent<AudioSource>().loop = audio.m_Loop;
                    tempSound.GetComponent<AudioSource>().pitch = audio.m_Pitch + Random.Range(-range, range);
                    tempSound.GetComponent<AudioSource>().Play();
                    return true;
                }
            }
        }

        return false;
    }

    public AudioObject GetAudioObject(string name)
    {
        foreach (AudioObject audio in ListOfAudioObjects) 
        {
            if (audio.m_AudioName == name)
            {
                return audio;
            }
        }

        return null;
    }
}
