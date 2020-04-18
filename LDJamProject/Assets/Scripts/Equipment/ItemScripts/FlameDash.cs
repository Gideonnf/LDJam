using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDash : ItemObjBase
{
    public GameObject flame;
    public float distanceFromPlayer;

    GameObject player;

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

    }

    public override void PassiveEffect()
    {
        base.PassiveEffect();
    }

    public override void OnPickUp()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        base.OnPickUp();
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }

    public override void WhenDashEnds()
    {
        GameObject flameObj = Instantiate(flame);
        flameObj.transform.position = player.transform.position - (player.GetComponent<PlayerMovement>().movementDir * distanceFromPlayer);
        Vector3 dir = player.GetComponent<PlayerMovement>().movementDir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 90;
        flameObj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        base.WhenDashEnds();
    }
}
