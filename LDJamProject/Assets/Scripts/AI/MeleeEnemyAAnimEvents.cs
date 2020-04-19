using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAAnimEvents : MonoBehaviour
{
    MeleeEnemyA enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<MeleeEnemyA>();
    }

    void DeathComplete()
    {
        enemy.Remove();
    }
}
