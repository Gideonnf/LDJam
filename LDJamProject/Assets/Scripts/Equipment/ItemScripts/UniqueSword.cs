using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueSword : ItemObjBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        base.Attack();

        Debug.Log("It does an attack lol pew pew pew pew ");

    }

    public override void PassiveEffect()
    {
        base.PassiveEffect();

        Debug.Log("Anti-Coronavirus");
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
