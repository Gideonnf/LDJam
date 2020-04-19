using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [HideInInspector]public Vector3 playerLookDir;
    [SerializeField] GameObject meleeAttackHitbox;
    [SerializeField] GameObject rangedAttackHitbox;
    [SerializeField] Slider meleeBar;
    [SerializeField] Slider rangedBar;

    PlayerStats playerStats;
    PlayerMovement playerMovement;
    float meleeAttackSpeed_;
    float rangedAttackSpeed_;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playerMovement = GetComponent<PlayerMovement>();
        meleeAttackSpeed_ = playerStats.m_CurrentMeleeAttackSpeed;
        rangedAttackSpeed_ = playerStats.m_CurrentRangedAttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (meleeAttackSpeed_ < playerStats.m_CurrentMeleeAttackSpeed)
            meleeAttackSpeed_ += Time.deltaTime;
        if (rangedAttackSpeed_ < playerStats.m_CurrentRangedAttackSpeed)
            rangedAttackSpeed_ += Time.deltaTime;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0;
        playerLookDir = worldMousePos - GetComponent<Transform>().position;
        playerLookDir.Normalize();

        meleeBar.value = meleeAttackSpeed_ / playerStats.m_CurrentMeleeAttackSpeed;
        rangedBar.value = rangedAttackSpeed_ / playerStats.m_CurrentRangedAttackSpeed;
    }

    public void Melee()
    {
        if(meleeAttackSpeed_ >= playerStats.m_CurrentMeleeAttackSpeed && !playerMovement.isDashing && !playerMovement.isAttackDashing)
        {
            playerMovement.MeleeAttackDash();
            for (int i = 0; i < GetComponent<PlayerInventory>().UniqueItems.Count; ++i)
            {
                GetComponent<PlayerInventory>().UniqueItems[i].MeleeAttack();
            }
            Instantiate(meleeAttackHitbox);
            meleeAttackSpeed_ = 0;
        }
    }

    public void Ranged()
    {
        if (rangedAttackSpeed_ >= playerStats.m_CurrentRangedAttackSpeed && !playerMovement.isDashing && !playerMovement.isAttackDashing)
        {
            Instantiate(rangedAttackHitbox);
            rangedAttackSpeed_ = 0;
        }
    }
}
