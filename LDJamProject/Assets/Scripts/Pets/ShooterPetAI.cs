using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPetAI : MonoBehaviour
{
    [SerializeField] float attackSpeed;
    [SerializeField] GameObject projectile;
    [SerializeField] float distanceFromPet;
    [SerializeField] Animator shooterPetAnimator;

    GameObject target;
    float attackSpeedTimer_;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        attackSpeedTimer_ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            attackSpeedTimer_ += Time.deltaTime;
            if (attackSpeedTimer_ >= attackSpeed)
            {
                SoundManager.Instance.Play("BirdTrigger");
                GameObject projectile_ = Instantiate(projectile);
                projectile_.GetComponent<AttackHitbox>().projectileDir = (target.transform.position - transform.parent.position).normalized;
                projectile_.GetComponent<AttackHitbox>().spawnPos = transform.parent.position + (target.transform.position - transform.parent.position).normalized * distanceFromPet;
                attackSpeedTimer_ = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && target == null)
        {
            target = collision.gameObject;
            attackSpeedTimer_ = 0;
            shooterPetAnimator.SetBool("Attacking", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            if (collision.gameObject == target)
            {
                target = null;
                shooterPetAnimator.SetBool("Attacking", false);
            }
        }
    }
}
