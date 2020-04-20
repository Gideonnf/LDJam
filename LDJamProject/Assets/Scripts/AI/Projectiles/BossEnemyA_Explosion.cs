using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyA_Explosion : MonoBehaviour
{
    int m_explosion_Trigger; // Explosion trigger for animation hash
    Animator m_animator;
    // Start is called before the first frame update
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_explosion_Trigger = Animator.StringToHash("explode");
    }

    public void Init()
    {
        m_animator.SetTrigger(m_explosion_Trigger);
    }

    void Remove()
    {
        gameObject.SetActive(false);
    }
}
