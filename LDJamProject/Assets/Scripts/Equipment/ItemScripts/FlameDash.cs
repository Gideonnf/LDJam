using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDash : ItemObjBase
{
    public GameObject flame;

    public override void OnDash()
    {
        SoundManager.Instance.Play("LavaDash");
        base.OnDash();
    }
    public override void WhenDashEnds()
    {
        Instantiate(flame);
        base.WhenDashEnds();
    }
}
