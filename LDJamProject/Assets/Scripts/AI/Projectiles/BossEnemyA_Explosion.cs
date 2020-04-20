using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyA_Explosion : MonoBehaviour
{
    int m_explosion_Trigger; // Explosion trigger for animation hash
    Animator m_animator;
    bool m_damagedPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_explosion_Trigger = Animator.StringToHash("explode");
    }

    public void Init()
    {
        m_animator.SetTrigger(m_explosion_Trigger);
        m_damagedPlayer = false;
    }

    void Remove()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Wagon wagon = null;
        PlayerController player = null;
        wagon = collision.gameObject.GetComponent<Wagon>();
        if (wagon != null)
        {
            // Damage wagon
            return;
        }
        player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            // Damage player
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_damagedPlayer)    // not damage them twice in one hit
            return;
        Wagon wagon = null;
        PlayerController player = null;
        wagon = collision.gameObject.GetComponent<Wagon>();
        if (wagon != null)
        {
            // Damage wagon
            PlayerController.Instance.m_PlayerStats.CaravanTakeDamage(1);
            m_damagedPlayer = true;
            return;
        }
        player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            // Damage player
            PlayerController.Instance.m_PlayerStats.PlayerTakeDamage(1);
            m_damagedPlayer = true;
            return;
        }
    }
}
