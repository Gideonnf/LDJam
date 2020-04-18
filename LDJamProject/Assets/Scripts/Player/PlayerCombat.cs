using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [HideInInspector]public Vector3 playerLookDir;
    [SerializeField] float attackSpeed = 0.5f;
    [SerializeField] GameObject attackHitbox;

    float attackSpeed_;

    // Start is called before the first frame update
    void Start()
    {
        attackSpeed_ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackSpeed_ > 0)
            attackSpeed_ -= Time.deltaTime;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0;
        playerLookDir = worldMousePos - GetComponent<Transform>().position;
        playerLookDir.Normalize();
    }

    public void Attack()
    {
        if(attackSpeed_ <= 0)
        {
            Instantiate(attackHitbox);
            attackSpeed_ = attackSpeed;
        }
    }
}
