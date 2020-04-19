using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyB : EnemyBase
{
    #region Animation-related hashes
        int moving_bool;        // bool for isWalking
    int hit_trigger;        // Take damage trigger
    int attack_trigger;     // Shoot trigger
    int death_trigger;      // Death trigger
    int attack_animation;   // Attack animation
    #endregion
    [SerializeField] float attackDistance;
    // Start is called before the first frame update
    new public void Start()
    {
        base.Start();
        moving_bool = Animator.StringToHash("moving");
        hit_trigger = Animator.StringToHash("hit");
        attack_trigger = Animator.StringToHash("attack");
        attack_animation = Animator.StringToHash("attackAnim");
        death_trigger = Animator.StringToHash("death");
        m_animator = GetComponentInChildren<Animator>();
        m_animator.SetBool(moving_bool, true);
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
    }

    override protected void OnTargetReached()
    {
        ClearPath();
        Attack();
    #if UNITY_EDITOR
        m_triggered = true;
    #endif
    }

    void Attack()
    {
        if (m_animator.GetCurrentAnimatorStateInfo(0).tagHash == attack_animation || m_animator.GetNextAnimatorStateInfo(0).tagHash == attack_animation)
            return;
        m_animator.SetTrigger(attack_trigger);
    }

    public void AttackPlayer()
    {
        // Check distance between player and enemy. Deals damage if the player is close enough to the enemy.
        Collider2D[] colliders;
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(m_rb.position, (Vector2)DEBUG_TARGET.position - m_rb.position, attackDistance);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<EnemyBase>() != null)
                continue;
            else if (hit.collider.gameObject.CompareTag("Player"));
        }
    }

    override protected bool TakeDamage()
    {
        m_animator.SetTrigger(hit_trigger);
        return base.TakeDamage();
    }

    new protected void OnDeath()
    {
        base.OnDeath();
        m_animator.SetTrigger(death_trigger);
    }
}