using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Object")]
public class AudioObject : ScriptableObject
{
    public string m_AudioName;

    public AudioClip m_Clip;

    [Range(0f, 1f)]
    public float m_Volume = 1;.0f
    [Range(0.1f, 3f)]
    public float m_Pitch = 1.0f;
    [Range(0f, 1f)]
    public float m_HearingBaseOnDist = 1.0f;

    public bool m_Loop = false;
}
