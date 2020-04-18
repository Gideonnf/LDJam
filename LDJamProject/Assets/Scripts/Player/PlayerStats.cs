using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Starting Stats")]
    [Tooltip("Starting Damage")]
    public float m_StartingDamage = 1f;

    [Tooltip("Starting Speed")]
    public float m_StartingSpeed = 5f;

    [Tooltip("Starting Dash Speed")]
    public float m_StartingDashSpeed = 12f;

    [Tooltip("Starting Dash Dsitance")]
    public float m_StartingDashDistance = 1.5f;

    [Tooltip("Starting Health")]
    public int m_StartingHealth = 5;

    [Tooltip("Starting Melee Attack Speed")]
    public float m_StartingMeleeAttackSpeed = 0.5f;

    [Tooltip("Capped Melee Attack Speed")]
    public float m_FastestMeleeAttackSpeed = 0.05f;

    [Tooltip("Starting Ranged Attack Speed")]
    public float m_StartingRangedAttackSpeed = 10f;

    [Tooltip("Capped Ranged Attack Speed")]
    public float m_FastestRangedAttackSpeed = 0.5f;

    [Tooltip("Starting Time To Reacharge One Dash")]
    public float m_StartingTimeToRechargeOneDash = 2f;

    [Tooltip("Starting Max Amount Of Dashes")]
    public int m_StartingMaxDash = 4;

    // Current Stats after bonuses are applied from items
    
    public float m_CurrentDamage;
    public float m_CurrentSpeed;
    public float m_CurrentMovementSpeed;
    public float m_CurrentDashSpeed;
    public float m_CurrentDashDistance;
    public float m_CurrentHealth;
    public float m_MaxHealth;
    public float m_CurrentMeleeAttackSpeed;
    public float m_CurrentRangedAttackSpeed;
    public float m_CurrentTimeToRechargeOneDash;
    public int m_CurrentMaxDash;


    // Start is called before the first frame update
    void Awake()
    {
        m_CurrentDamage = m_StartingDamage;
        m_CurrentSpeed = m_StartingSpeed;
        m_CurrentMovementSpeed = m_StartingSpeed;
        m_CurrentDashSpeed = m_StartingDashSpeed;
        m_CurrentDashDistance = m_StartingDashDistance;
        m_CurrentHealth = m_StartingHealth;
        m_MaxHealth = m_StartingHealth;
        m_CurrentMeleeAttackSpeed = m_StartingMeleeAttackSpeed;
        m_CurrentRangedAttackSpeed = m_StartingRangedAttackSpeed;
        m_CurrentTimeToRechargeOneDash = m_StartingTimeToRechargeOneDash;
        m_CurrentMaxDash = m_StartingMaxDash;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
