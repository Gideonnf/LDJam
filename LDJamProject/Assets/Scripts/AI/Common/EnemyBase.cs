using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    public Color32 hitColor;
    public Color32 normalColor;
    public float hitFlashTimer;
    SpriteRenderer spriteRenderer;
    [SerializeField] protected float movespeed;
    protected Animator m_animator;
    #if UNITY_EDITOR
    public static bool g_debugMode = true;
    static Color32 line_normal = new Color32(0, 0, 255, 255);
    static Color32 line_triggered = new Color32(255, 0, 0, 255);

#endif
    protected bool m_triggered = false;

    // Values for knockback on damage
    [SerializeField] bool canBeKnocked;
    [SerializeField] float knockbackDuration = 0.5f;
    float m_knockbackCountdown;
    bool stunned;

    // The hash for the animator's dead bool
    protected int m_dead_BoolHash;

    protected int maxHealth = 10;
    protected int health;
    protected Vector3 m_nextPosition;
    protected Rigidbody2D m_rb;
    [SerializeField] float attack_range_sqr;
    public Transform DEBUG_TARGET;
    //protected bool m_dead;  // Does not update new path if true
    NavMeshAgent m_agent;
    // Start is called before the first frame update

    float hitFlashTimer_;

    virtual public void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponentInChildren<Animator>();
        m_agent.updatePosition = false;
        m_agent.updateRotation = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_dead_BoolHash = Animator.StringToHash("dead");
        health = maxHealth;
        Init();
        hitFlashTimer_ = 0;
        stunned = false;
        m_knockbackCountdown = 0f;
    }

    virtual public void Init()
    {
        m_rb.isKinematic = false;
        m_animator.SetBool(m_dead_BoolHash, false);
        health = maxHealth;
        SetMoveSpeed(movespeed);
        m_agent.acceleration = 9f;
        float distToPlayer = ((Vector2)PlayerController.Instance.transform.position - m_rb.position).sqrMagnitude;
        float distToYourMom = ((Vector2)Wagon.instance.transform.position - m_rb.position).sqrMagnitude;
        if (distToYourMom < distToPlayer)
            DEBUG_TARGET = Wagon.instance.transform;
        else
            DEBUG_TARGET = PlayerController.Instance.gameObject.transform;

    }

    // Update is called once per frame
    virtual protected void Update()
    {
        spriteRenderer.sortingOrder = (int)(spriteRenderer.transform.position.y * -100);
        m_nextPosition = m_agent.nextPosition;
        m_rb.MovePosition(m_agent.nextPosition);
        if (stunned)
        {
            m_knockbackCountdown -= Time.deltaTime;
            if (m_knockbackCountdown <= 0f)
            {
                stunned = false;
            }
            else
                return;
        }
        if (m_animator.GetBool(m_dead_BoolHash))
            return;
        m_agent.SetDestination(DEBUG_TARGET.position);
        transform.position = m_nextPosition;
        if ((m_nextPosition - DEBUG_TARGET.position).sqrMagnitude <= attack_range_sqr)
        {
            OnTargetReached();
        }
        else
            m_triggered = false;
           
        if(transform.GetChild(0).GetComponent<SpriteRenderer>().color != normalColor)
        {
            hitFlashTimer_ += Time.deltaTime;
            Debug.Log(hitFlashTimer_);
            if(hitFlashTimer_ >= hitFlashTimer)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = normalColor;
            }
        }
#if UNITY_EDITOR
        Debug.DrawLine(transform.position, DEBUG_TARGET.position, m_triggered ? line_triggered : line_normal);
#endif
    }

    public void ClearPath(){ m_agent.SetDestination(transform.position); }

    public void SetTarget(Transform _target)
    {
        DEBUG_TARGET = _target;
    }

    /// <summary>
    /// Function called when player is within attack range of enemy
    /// </summary>
    virtual protected void OnTargetReached()
    {
    }

    public void SetMoveSpeed(float _speed)
    {
        m_agent.speed = _speed;
    }

    virtual public bool TakeDamage(int dmg)
    {
        health -= dmg;
        hitFlashTimer_ = 0;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = hitColor;
        if (health <= 0) 
        {
            return true;
        }
        // If can be stunned, enable stun logic
        if (canBeKnocked)
        {
            stunned = true;
            m_knockbackCountdown = knockbackDuration;
            Vector3 newDestination = transform.position + (transform.position - DEBUG_TARGET.position);
            m_agent.SetDestination(newDestination);
            m_agent.velocity = Vector3.zero;
        }
        return false;
    }

    virtual protected void OnDeath()
    {
        EquipmentManager.Instance.NormalItemDrop(gameObject.transform.position);
        m_animator.SetBool(m_dead_BoolHash, true);
        m_agent.acceleration = 0f;
        m_agent.velocity = Vector3.zero;
        m_rb.isKinematic = true;
        ClearPath();    // Stop it from moving
    }

    public void Warp(Vector3 position)
    {
        if (m_agent)
        {
            m_agent.updatePosition = true;
            m_agent.Warp(position);
            m_agent.updatePosition = false;
        }
    }

    /// <summary>
    /// Sets the gameobject inactive
    /// </summary>
    virtual public void Remove()
    {
        gameObject.SetActive(false);
    }
}
