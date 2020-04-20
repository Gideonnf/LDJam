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
    public override void OnPickUp()
    {
        PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        playerCombat.attack360 = true;
        playerCombat.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("360Attack", true);
        base.OnPickUp();
    }
    public override void OnRemove()
    {
        PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        playerCombat.attack360 = false;
        playerCombat.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("360Attack", false);
        base.OnRemove();
    }
}
