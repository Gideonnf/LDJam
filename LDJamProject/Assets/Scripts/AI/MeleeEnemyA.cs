using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyA : EnemyBase
{
    // Start is called before the first frame update
    new public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
    }

    new protected void OnTargetReached()
    {
#if UNITY_EDITOR
        m_triggered = true;
#endif
    }

    new protected bool TakeDamage()
    {
        return base.TakeDamage();
    }

    new protected void OnDeath()
    {

    }
}
