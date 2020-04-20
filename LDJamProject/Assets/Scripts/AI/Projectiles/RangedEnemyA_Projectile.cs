using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyA_Projectile : MonoBehaviour
{
    [SerializeField] float lifetime = 5f;
    [SerializeField] float movespeed = 0.1f;

    Rigidbody2D rb;
    Animator m_animator;
    float m_countdown;
    Vector2 m_moveDir;
    int shoot_Trigger;
    // Start is called before the first frame update
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        shoot_Trigger = Animator.StringToHash("shoot");
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position - m_moveDir * movespeed);
        m_countdown -= Time.deltaTime;
        if (m_countdown <= 0f)
            gameObject.SetActive(false);
    }

    /// <summary>
    /// Function to initialize the bullet animation and lifetime
    /// </summary>
    public void Init(Vector2 pos, Vector2 _dir)
    {
        rb.position = pos;
        rb.SetRotation(Mathf.Atan2(_dir.y, _dir.x));
        m_countdown = lifetime;
        m_animator.SetTrigger(shoot_Trigger);
        m_moveDir = _dir;
    }


    private void OnCollisionEnter2D(Collision2D collision)
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
        gameObject.SetActive(false);
    }
}
