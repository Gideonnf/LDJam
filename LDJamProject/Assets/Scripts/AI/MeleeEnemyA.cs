using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyA : EnemyBase
{
    [SerializeField] float explosionRadius;
    [SerializeField] int explosionDamage;
    #region Animation-related hashes
    int moving_bool;        // bool for isWalking
    int hit_trigger;        // Take damage trigger
    int attack_trigger;     // Shoot trigger
    int attack_animation;   // Attack animation
    int death_trigger;      // Death trigger
    #endregion
    // Start is called before the first frame update
    override public void Awake()
    {
        base.Awake();
        moving_bool = Animator.StringToHash("moving");
        hit_trigger = Animator.StringToHash("hit");
        attack_trigger = Animator.StringToHash("attack");
        attack_animation = Animator.StringToHash("attackAnim");
        death_trigger = Animator.StringToHash("death");
        m_animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
    }

    override protected void OnTargetReached()
    {
        m_animator.SetTrigger(attack_trigger);
        OnDeath();
#if UNITY_EDITOR
        m_triggered = true;
#endif
    }

    override public bool TakeDamage(int dmg)
    {
        if (health > 0)
        {
            if (base.TakeDamage(dmg))
            {
                OnDeath();
            }
            else
                m_animator.SetTrigger(hit_trigger);
        }
        return true;
    }

    /// <summary>
    /// Function called when enemy explodes.
    /// Causes an explosion with a radius and checks if player should take damage
    /// </summary>
    void Explosion()
    {
        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(m_rb.position, explosionRadius);
        foreach(Collider2D col in colliders)
        {
            // Check if it has the player script. If it does, deal damage
            Wagon wagon = null;
            PlayerController player = null;
            wagon = col.gameObject.GetComponent<Wagon>();
            if (wagon != null)
            {
                // Damage wagon
                return;
            }
            player = col.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                // Damage player
                return;
            }
        }
    }

    override protected void OnDeath()
    {
        base.OnDeath();
        m_animator.SetTrigger(death_trigger);
    }
}
