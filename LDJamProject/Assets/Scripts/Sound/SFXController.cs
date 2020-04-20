using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    AudioSource audioSource;
    float lifetime;
    bool lifetimeSet;

    // Start is called before the first frame update
    void Start()
    {
        lifetimeSet = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!lifetimeSet && !audioSource.loop)
        {
            lifetimeSet = true;
            lifetime = audioSource.clip.length;
        }
        if (!audioSource.loop)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (!audioSource.isPlaying)
                Destroy(gameObject);
        }
    }
}
