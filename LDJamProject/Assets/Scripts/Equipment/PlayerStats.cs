using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Starting Stats")]
    [Tooltip("Starting Damage")]
    public float m_StartingDamage;

    [Tooltip("Starting Speed")]
    public float m_StartingSpeed;

    [Tooltip("Starting Health")]
    public int StartingHealth;

    // Current Stats after bonuses are applied from items
    float m_CurrentDamage;
    float m_CurrentSpeed;
    float m_CurrentHealth;

    // Keep track max health
    float m_MaxHealth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
