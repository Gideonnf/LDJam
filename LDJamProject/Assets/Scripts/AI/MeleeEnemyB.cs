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
    override public void Awake()
    {
        base.Awake();
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
        //Collider2D[] colliders;
        // Check distance between player and enemy. Deals damage if the player is close enough to the enemy.
        if ((m_rb.position - (Vector2)PlayerController.Instance.transform.position).sqrMagnitude > attackDistance * attackDistance)
            return;
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(m_rb.position, (Vector2)DEBUG_TARGET.position - m_rb.position, attackDistance);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<GameObject>() != null)
                continue;
            WagonCollisionChecker wagon = null;
            PlayerController player = null;
            wagon = hit.collider.gameObject.GetComponent<WagonCollisionChecker>();
            player = hit.collider.gameObject.GetComponent<PlayerController>();
            if (wagon != null)
            {
                // Damage wagon
                PlayerController.Instance.m_PlayerStats.CaravanTakeDamage(1);
                return;
            }
            else if (player != null)
            {
                // Damage player
                PlayerController.Instance.m_PlayerStats.PlayerTakeDamage(1);
                return;
            }
        }
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

    override protected void OnDeath()
    {
        base.OnDeath();
        m_animator.SetTrigger(death_trigger);
    }
}