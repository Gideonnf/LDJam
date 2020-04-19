using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] protected float movespeed;
    protected Animator m_animator;
    #if UNITY_EDITOR
    public static bool g_debugMode = true;
    static Color32 line_normal = new Color32(0, 0, 255, 255);
    static Color32 line_triggered = new Color32(255, 0, 0, 255);

    protected bool m_triggered = false;
#endif
    protected int health = 10;
    protected Vector3 m_nextPosition;
    protected Rigidbody2D m_rb;
    [SerializeField] float attack_range_sqr;
    public Transform DEBUG_TARGET;
    protected bool m_dead;  // Does not update new path if true
    NavMeshAgent m_agent;
    // Start is called before the first frame update
    virtual public void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponentInChildren<Animator>();
        m_agent.updatePosition = false;
        m_agent.updateRotation = false;
        DEBUG_TARGET = PlayerController.Instance.gameObject.transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Init();
    }

    virtual public void Init()
    {
        m_dead = false;
        SetMoveSpeed(movespeed);
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        spriteRenderer.sortingOrder = (int)(spriteRenderer.transform.position.y * -100);
        if (m_dead)
            return;
        m_agent.SetDestination(DEBUG_TARGET.position);
        m_nextPosition = m_agent.nextPosition;
        m_rb.MovePosition(m_agent.nextPosition);
        transform.position = m_nextPosition;
        if ((m_nextPosition - DEBUG_TARGET.position).sqrMagnitude <= attack_range_sqr)
        {
            OnTargetReached();
        }
        else
            m_triggered = false;
           
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

    virtual protected bool TakeDamage()
    {
        return true;
    }

    virtual protected void OnDeath()
    {
        m_dead = true;
        ClearPath();    // Stop it from moving
    }

    public void Warp(Vector3 position)
    {
        if (m_agent)
        {
            m_agent.Warp(position);
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
