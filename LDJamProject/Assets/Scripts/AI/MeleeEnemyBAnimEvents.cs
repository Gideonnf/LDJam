using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBAnimEvents : MonoBehaviour
{
    MeleeEnemyB enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<MeleeEnemyB>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        enemy.AttackPlayer(); // Attack his target
    }

    void DeathComplete()
    {
        enemy.Remove();
    }
}
