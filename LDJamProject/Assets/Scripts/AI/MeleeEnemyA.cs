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
    #endregion
    // Start is called before the first frame update
    new public void Start()
    {
        base.Start();
        moving_bool = Animator.StringToHash("moving");
        hit_trigger = Animator.StringToHash("hit");
        attack_trigger = Animator.StringToHash("attack");
        attack_animation = Animator.StringToHash("attackAnim");
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

    override protected bool TakeDamage()
    {
        m_animator.SetTrigger(hit_trigger);
        return base.TakeDamage();
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
        }
    }

    new protected void OnDeath()
    {
        m_dead = true;
        //base.OnDeath();
    }
}
