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

    [Tooltip("Starting Dash Distance")]
    public float m_StartingDashDistance = 1.5f;

    [Tooltip("Starting Health")]
    public int m_StartingHealth = 5;

    [Tooltip("Starting Caravan Health")]
    public int m_StartingCaravanHealth = 5;

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

    [Tooltip("Starting Attack Dash Speed")]
    public float m_StartingAttackDashSpeed = 12f;

    [Tooltip("Starting Attack Dash Distance")]
    public float m_StartingAttackDashDistance = 0.1f;

    [Tooltip("Starting Attack Dash Time")]
    public float m_StartingAttackDashTime = 0.1f;

    [Tooltip("Distance Between Each Pet")]
    public float m_DistanceBetweenEachPet = 1f;

    // Current Stats after bonuses are applied from items

    public float m_CurrentDamage;
    public float m_CurrentSpeed;
    public float m_CurrentMovementSpeed;
    public float m_CurrentDashSpeed;
    public float m_CurrentDashDistance;
    public float m_CurrentHealth;
    public float m_MaxHealth;
    public float m_CurrentCaravanHealth;
    public float m_MaxCaravanHealth;
    public float m_CurrentMeleeAttackSpeed;
    public float m_CurrentRangedAttackSpeed;
    public float m_CurrentTimeToRechargeOneDash;
    public int m_CurrentMaxDash;
    public float m_CurrentAttackDashSpeed;
    public float m_CurrentAttackDashDistance;
    public float m_CurrentAttackDashTime;
    public int m_NumOfPets;


    // Start is called before the first frame update
    void Awake()
    {
        m_CurrentDamage = m_StartingDamage;
        m_CurrentSpeed = m_StartingSpeed;
        m_CurrentMovementSpeed = m_StartingSpeed;
        m_CurrentDashSpeed = m_StartingDashSpeed;
        m_CurrentDashDistance = m_StartingDashDistance;
        m_CurrentHealth = m_StartingHealth;
        m_MaxCaravanHealth = m_StartingCaravanHealth;
        m_CurrentCaravanHealth = m_StartingCaravanHealth;
        m_MaxHealth = m_StartingHealth;
        m_CurrentMeleeAttackSpeed = m_StartingMeleeAttackSpeed;
        m_CurrentRangedAttackSpeed = m_StartingRangedAttackSpeed;
        m_CurrentTimeToRechargeOneDash = m_StartingTimeToRechargeOneDash;
        m_CurrentMaxDash = m_StartingMaxDash;
        m_CurrentAttackDashSpeed = m_StartingAttackDashSpeed;
        m_CurrentAttackDashDistance = m_StartingAttackDashDistance;
        m_CurrentAttackDashTime = m_StartingAttackDashTime;
        m_NumOfPets = 0;
}

    // Update is called once per frame
    void Update()
    {
        if(m_CurrentHealth <= 0 || m_CurrentCaravanHealth <= 0)
        {
            //death screen
        }
    }
}
