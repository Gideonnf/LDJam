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

    [Tooltip("Seconds of invincibility after getting hit")]
    public float m_IFramesAfterHit = 1f;

    [Tooltip("Rate of blinking when invincibile")]
    public float m_TimeBetweenEachIFrameBlink = 0.05f;

    public bool playerTakeDamageDebug;
    public bool caravanTakeDamageDebug;

    [SerializeField] GameObject caravan;

    // Current Stats after bonuses are applied from items

    public float m_CurrentDamage;
    public float m_CurrentSpeed;
    public float m_CurrentMovementSpeed;
    public float m_CurrentDashSpeed;
    public float m_CurrentDashDistance;
    public int m_CurrentHealth;
    public int m_MaxHealth;
    public int m_CurrentCaravanHealth;
    public int m_MaxCaravanHealth;
    public float m_CurrentMeleeAttackSpeed;
    public float m_CurrentRangedAttackSpeed;
    public float m_CurrentTimeToRechargeOneDash;
    public int m_CurrentMaxDash;
    public float m_CurrentAttackDashSpeed;
    public float m_CurrentAttackDashDistance;
    public float m_CurrentAttackDashTime;
    public int m_NumOfPets;
    public float m_PlayerCurrentIFrames;
    public float m_CaravanCurrentIFrames;
    public float m_PlayerIFramesBlinkTimer;
    public float m_CaravanIFramesBlinkTimer;

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
        m_PlayerCurrentIFrames = 0;
        m_CaravanCurrentIFrames = 0;
        m_PlayerIFramesBlinkTimer = m_TimeBetweenEachIFrameBlink;
        m_CaravanIFramesBlinkTimer = m_TimeBetweenEachIFrameBlink;
        m_NumOfPets = 0;
}

    // Update is called once per frame
    void Update()
    {
        if(m_PlayerCurrentIFrames>=0)
        {
            m_PlayerCurrentIFrames -= Time.deltaTime;
            m_PlayerIFramesBlinkTimer -= Time.deltaTime;
            if(m_PlayerIFramesBlinkTimer <= 0)
            {
                Color temp = GetComponent<SpriteRenderer>().color;
                if (temp.a == 1)
                    temp.a = 0;
                else
                    temp.a = 1;
                GetComponent<SpriteRenderer>().color = temp;
                m_PlayerIFramesBlinkTimer = m_TimeBetweenEachIFrameBlink;
            }
            if(m_PlayerCurrentIFrames <= 0)
            {
                Color temp = GetComponent<SpriteRenderer>().color;
                temp.a = 1;
                GetComponent<SpriteRenderer>().color = temp;
            }
        }

        if (m_CaravanCurrentIFrames >= 0)
        {
            m_CaravanCurrentIFrames -= Time.deltaTime;
            m_CaravanIFramesBlinkTimer -= Time.deltaTime;
            if (m_CaravanIFramesBlinkTimer <= 0)
            {
                Color temp = caravan.GetComponent<SpriteRenderer>().color;
                if (temp.a == 1)
                    temp.a = 0;
                else
                    temp.a = 1;
                caravan.GetComponent<SpriteRenderer>().color = temp;
                m_CaravanIFramesBlinkTimer = m_TimeBetweenEachIFrameBlink;
            }
            if (m_CaravanCurrentIFrames <= 0)
            {
                Color temp = caravan.GetComponent<SpriteRenderer>().color;
                temp.a = 1;
                caravan.GetComponent<SpriteRenderer>().color = temp;
            }
        }

        if(playerTakeDamageDebug)
        {
            playerTakeDamageDebug = false;
            PlayerTakeDamage(1);
        }
        if (caravanTakeDamageDebug)
        {
            caravanTakeDamageDebug = false;
            CaravanTakeDamage(1);
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        if (m_PlayerCurrentIFrames > 0)
            return;
        m_CurrentHealth -= damage;
        if (m_CurrentHealth <= 0)
        {
            //death screen
        }
        m_PlayerCurrentIFrames = m_IFramesAfterHit;
    }

    public void CaravanTakeDamage(int damage)
    {
        if (m_CaravanCurrentIFrames > 0)
            return;
        m_CurrentCaravanHealth -= damage;
        if (m_CurrentCaravanHealth <= 0)
        {
            //death screen
        }
        m_CaravanCurrentIFrames = m_IFramesAfterHit;
    }
}
