using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDash : ItemObjBase
{
    public GameObject flame;

    public override void WhenDashEnds()
    {
        Instantiate(flame);
        base.WhenDashEnds();
    }
}
