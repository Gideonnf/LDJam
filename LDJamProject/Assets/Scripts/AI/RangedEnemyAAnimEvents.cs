using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAAnimEvents : MonoBehaviour
{
    RangedEnemyA enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<RangedEnemyA>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Animation event called when the enemy shoots
    /// </summary>
    public void Shoot()
    {
        enemy.ShootProjectile();
    }

    /// <summary>
    /// Animation event called when the enemy shoots
    /// </summary>
    void DeathComplete()
    {
        enemy.Remove();
    }
}
