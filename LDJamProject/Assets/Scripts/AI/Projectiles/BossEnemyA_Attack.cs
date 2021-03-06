﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossEnemyA_Attack : MonoBehaviour
{
    [SerializeField] float explosionRadius = .25f;
    [Tooltip("Determines the time taken before creating another explosion")]
    [SerializeField] float sleepTimer;
    [Tooltip("Determines the max number of explosions before expiring")]
    [SerializeField] int maxIterations;

    ObjectPooler poolerInstance;
    private float m_countdown;  // Countdown for sleep timer
    private int m_iterations;   // Count for iterations 
    private Vector2 m_dir;
    private Rigidbody2D m_rb;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().radius = explosionRadius;
        explosionRadius *= 2f;   // Actually gonna use it as a diameter instead on runtime :P
        m_rb = GetComponent<Rigidbody2D>();
        poolerInstance = ObjectPooler.Instance;
    }

    public void Init(Vector2 _dir)
    {
        m_dir = _dir;
        m_countdown = 0f;
        m_iterations = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_countdown -= Time.deltaTime;
        if (m_countdown <= 0f)
        {
            UpdateAttack();
            m_countdown = sleepTimer;
        }
    }

    void UpdateAttack()
    {
        // Spawns a new attack
        BossEnemyA_Explosion explosionGO = poolerInstance.FetchGO("BA_E").GetComponent<BossEnemyA_Explosion>();
        explosionGO.Init();
        explosionGO.transform.position = transform.position;
        // Move on to next position
        m_rb.MovePosition(m_rb.position + m_dir * explosionRadius);
        if (++m_iterations == maxIterations)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Debug.LogWarning("The boss projectile is not a trigger");
    }
}
