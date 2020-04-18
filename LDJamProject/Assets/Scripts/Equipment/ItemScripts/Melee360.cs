using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee360 : ItemObjBase
{
    public GameObject Melee360Hitbox;
    public override void MeleeAttack()
    {
        Instantiate(Melee360Hitbox);
        base.MeleeAttack();
    }
}
