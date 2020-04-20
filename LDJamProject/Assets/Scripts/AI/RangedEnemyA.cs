using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyA : EnemyBase
{
    ObjectPooler poolerInstance;
    [SerializeField] float attackCooldown; // Time before another attack can be made
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
        m_animator.SetBool(moving_bool, true);
        SetMoveSpeed(movespeed);
        poolerInstance = ObjectPooler.Instance;
    }

    public override void Init()
    {
        base.Init();
    }

    // Update is called once per frame
    new protected void Update()
    {
        if (m_animator.GetBool(m_dead_BoolHash))
            return;
        base.Update();
    }

    /// <summary>
    /// Function called when player is within attack range
    /// </summary>
    override protected void OnTargetReached()
    {
        Attack();
        ClearPath();
#if UNITY_EDITOR
        m_triggered = true;
#endif
    }

    /// <summary>
    /// Plays the attack animation but does not shoot here
    /// </summary>
    private void Attack()
    {
        // Return if already attacking
        if (m_animator.GetCurrentAnimatorStateInfo(0).tagHash == attack_animation || m_animator.GetNextAnimatorStateInfo(0).tagHash == attack_animation)
            return;
        m_animator.SetTrigger(attack_trigger);
    }

    /// <summary>
    /// Function to be called by animation event so do not bother looking for where it's called in code.
    /// </summary>
    public void ShootProjectile()
    {
        // Shoot towards target
        RangedEnemyA_Projectile newProjectile = poolerInstance.FetchGO("ERA_Proj").GetComponent<RangedEnemyA_Projectile>();
        newProjectile.Init(m_rb.position, (m_rb.position - (Vector2)DEBUG_TARGET.position).normalized);
        //Debug.Log("shot towards an enemy");
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
