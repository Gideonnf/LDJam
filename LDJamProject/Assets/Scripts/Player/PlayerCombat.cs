using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [HideInInspector]public Vector3 playerLookDir;
    [SerializeField] float meleeAttackSpeed = 0.5f;
    [SerializeField] float rangedAttackSpeed = 2f;
    [SerializeField] GameObject meleeAttackHitbox;
    [SerializeField] GameObject rangedAttackHitbox;
    [SerializeField] Slider meleeBar;
    [SerializeField] Slider rangedBar;

    float meleeAttackSpeed_;
    float rangedAttackSpeed_;

    // Start is called before the first frame update
    void Start()
    {
        meleeAttackSpeed_ = meleeAttackSpeed;
        rangedAttackSpeed_ = rangedAttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (meleeAttackSpeed_ < meleeAttackSpeed)
            meleeAttackSpeed_ += Time.deltaTime;
        if (rangedAttackSpeed_ < rangedAttackSpeed)
            rangedAttackSpeed_ += Time.deltaTime;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0;
        playerLookDir = worldMousePos - GetComponent<Transform>().position;
        playerLookDir.Normalize();

        meleeBar.value = meleeAttackSpeed_ / meleeAttackSpeed;
        rangedBar.value = rangedAttackSpeed_ / rangedAttackSpeed;
    }

    public void Melee()
    {
        if(meleeAttackSpeed_ >= meleeAttackSpeed)
        {
            Instantiate(meleeAttackHitbox);
            meleeAttackSpeed_ = 0;
        }
    }

    public void Ranged()
    {
        if (rangedAttackSpeed_ >= rangedAttackSpeed)
        {
            Instantiate(rangedAttackHitbox);
            rangedAttackSpeed_ = 0;
        }
    }
}
