using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueBoots : ItemObjBase
{
    public override void Attack()
    {
        base.Attack();

        Debug.Log("VROOM VROOOM I WANT TO DIE VROOM VROOM");
    }

    public override void PassiveEffect()
    {
        base.PassiveEffect();

        Debug.Log("swag swag swag swag");
    }

    public override void OnPickUp()
    {
        base.OnPickUp();

    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
