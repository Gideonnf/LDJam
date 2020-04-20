using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyA : EnemyBase
{
    ObjectPooler poolerInstance;
    [SerializeField] float attackCooldown; // Time before another attack can be made
    [SerializeField] GameObject attackPrefab;
    [SerializeField] Transform shootPosition;
    float m_countdown;
    #region Animation-related hashes
    int moving_bool;        // bool for isWalking
    int hit_trigger;        // Take damage trigger
    int attack_trigger;     // Shoot trigger
    int attack_animation;   // Attack animation
    int death_trigger;      // Death trigger
    #endregion
    // Start is called before the first frame update
    new public void Awake()
    {
        base.Awake();
        moving_bool = Animator.StringToHash("moving");
        hit_trigger = Animator.StringToHash("hit");
        attack_trigger = Animator.StringToHash("attack");
        attack_animation = Animator.StringToHash("attackAnim");
        death_trigger = Animator.StringToHash("death");
        m_animator.SetBool(moving_bool, true);
        SetMoveSpeed(movespeed);
        poolerInstance = ObjectPooler.Instance;
#if UNITY_EDITOR
        m_triggered = true;
#endif
    }

    public override void Init()
    {
        base.Init();
        m_countdown = attackCooldown;
        m_animator.SetBool(m_dead_BoolHash, false);
    }

    // Update is called once per frame
    new protected void Update()
    {
        if (m_animator.GetBool(m_dead_BoolHash))
            return;
        m_countdown -= Time.deltaTime;
        if (m_countdown <= 0f)
        {
            Attack();
            m_countdown = attackCooldown;
        }
        //base.Update();
    }

    /// <summary>
    /// Plays the attack animation but does not shoot here
    /// </summary>
    private void Attack()
    {
        // Attack the player with spell
        BossEnemyA_Attack bullet = Instantiate(attackPrefab).GetComponent<BossEnemyA_Attack>();
        bullet.transform.position = shootPosition.position;
        bullet.Init((Vector2)DEBUG_TARGET.position - m_rb.position);
       // m_animator.SetTrigger(attack_trigger);
    }

    /// <summary>
    /// Function to call when this enemy takes damage
    /// </summary>
    /// <returns></returns>
    override public bool TakeDamage(int dmg)
    {
        if (health > 0)
        {
            m_animator.SetTrigger(hit_trigger);
            if (base.TakeDamage(dmg))
            {
                OnDeath();
            }
        }
        return true;
    }

    override protected void OnDeath()
    {
        base.OnDeath();
        m_animator.SetTrigger(death_trigger);
    }
}