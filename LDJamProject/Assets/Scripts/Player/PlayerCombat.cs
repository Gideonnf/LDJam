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
    [SerializeField] GameObject meleeWeapon;
    [SerializeField] Animator meleeWeaponAnimator;
    [SerializeField] GameObject meleeAttackParticles;

    PlayerStats playerStats;
    PlayerMovement playerMovement;
    float meleeAttackSpeed_;
    float rangedAttackSpeed_;
    bool attack1Animation;
    public bool attack360;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playerMovement = GetComponent<PlayerMovement>();
        meleeAttackSpeed_ = playerStats.m_CurrentMeleeAttackSpeed;
        rangedAttackSpeed_ = playerStats.m_CurrentRangedAttackSpeed;
        attack1Animation = false;
        attack360 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.m_CurrentHealth <= 0 || playerStats.m_CurrentCaravanHealth <= 0)
            return;
        if (meleeAttackSpeed_ < playerStats.m_CurrentMeleeAttackSpeed)
            meleeAttackSpeed_ += Time.deltaTime;
        if (rangedAttackSpeed_ < playerStats.m_CurrentRangedAttackSpeed)
            rangedAttackSpeed_ += Time.deltaTime;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0;
        playerLookDir = worldMousePos - GetComponent<Transform>().position;
        playerLookDir.Normalize();
        meleeWeapon.transform.rotation = Quaternion.identity;
        meleeWeapon.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(playerLookDir.y, playerLookDir.x)));

        meleeBar.value = meleeAttackSpeed_ / playerStats.m_CurrentMeleeAttackSpeed;
        rangedBar.value = rangedAttackSpeed_ / playerStats.m_CurrentRangedAttackSpeed;
    }

    public void Melee()
    {
        if(meleeAttackSpeed_ >= playerStats.m_CurrentMeleeAttackSpeed && !playerMovement.isDashing && !playerMovement.isAttackDashing)
        {
            if (!attack360)
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        SoundManager.Instance.Play("PlayerAttack1");
                        break;
                    case 1:
                        SoundManager.Instance.Play("PlayerAttack2");
                        break;
                    case 2:
                        SoundManager.Instance.Play("PlayerAttack3");
                        break;
                }
                Instantiate(meleeAttackHitbox);
            }
            else
            {
                SoundManager.Instance.Play("360Attack");
            }

            attack1Animation = !attack1Animation;
            meleeWeaponAnimator.SetBool("Attack1", attack1Animation);

            meleeAttackParticles.SetActive(true);
            meleeAttackParticles.GetComponent<ParticleSystem>().Play();

            playerMovement.MeleeAttackDash();
            for (int i = 0; i < GetComponent<PlayerInventory>().UniqueItems.Count; ++i)
            {
                GetComponent<PlayerInventory>().UniqueItems[i].MeleeAttack();
            }

            meleeAttackSpeed_ = 0;
        }
    }

    public void Ranged()
    {
        if (rangedAttackSpeed_ >= playerStats.m_CurrentRangedAttackSpeed && !playerMovement.isDashing && !playerMovement.isAttackDashing)
        {
            SoundManager.Instance.Play("AirSliceAttack");
            attack1Animation = !attack1Animation;
            meleeWeaponAnimator.SetBool("Attack1", attack1Animation);
            Instantiate(rangedAttackHitbox);
            rangedAttackSpeed_ = 0;
        }
    }
}
