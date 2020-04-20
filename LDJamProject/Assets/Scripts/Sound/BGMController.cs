using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{

    [SerializeField] float fadeSpeed;

    bool fadingIn;
    bool fadingOut;
    AudioObject nextBGM;
    AudioSource audioSource;

    public bool fadeDebugTest = false;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource.clip = SoundManager.Instance.GetAudioObject("NonCombatBGM").m_Clip;
        //audioSource.volume = SoundManager.Instance.GetAudioObject("NonCombatBGM").m_Volume;
        //audioSource.pitch = SoundManager.Instance.GetAudioObject("NonCombatBGM").m_Pitch;
        //audioSource.Play();
        fadingIn = false;
        fadingOut = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeDebugTest)
        {
            fadeDebugTest = false;
            ChangeMusic("CombatBGM");
        }

        if(fadingOut)
        {
            audioSource.volume -= fadeSpeed * Time.deltaTime;
            if(audioSource.volume<=0)
            {
                audioSource.volume = 0;
                fadingIn = true;
                fadingOut = false;
                audioSource.clip = nextBGM.m_Clip;
                audioSource.Play();
            }
        }
        else if(fadingIn)
        {
            audioSource.volume += fadeSpeed * Time.deltaTime;
            if (audioSource.volume >= nextBGM.m_Volume)
            {
                audioSource.volume = nextBGM.m_Volume;
                fadingIn = false;
            }
        }
    }

    public void ChangeMusic(string name)
    {
        nextBGM = SoundManager.Instance.GetAudioObject(name);
        fadingOut = true;
    }
}
