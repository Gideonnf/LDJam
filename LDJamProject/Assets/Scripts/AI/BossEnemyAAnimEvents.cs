using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAAnimEvents : MonoBehaviour
{
    BossEnemyA enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<BossEnemyA>();
    }

    void Remove()
    {
        enemy.Remove();
    }
}
