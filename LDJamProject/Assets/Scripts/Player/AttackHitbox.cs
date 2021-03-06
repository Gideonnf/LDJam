﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackHitbox : MonoBehaviour
{
    public int damage;

    [SerializeField] float lifetime;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] float projectileSpeed;
    [SerializeField] bool projectilePierce;
    [SerializeField] bool canHitMultipleTimes;
    [SerializeField] float timeBetweenEachHit;
    [SerializeField] bool posBasedOnMovementDir;
    [SerializeField] bool nonPlayerAttack;
    public Vector3 projectileDir;
    public Vector3 spawnPos;

    GameObject player;
    List<GameObject> objsAttacked;
    Vector3 direction;
    List<float> timeBetweenEachHit_;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dir;
        float angle;
        if (nonPlayerAttack)
        {
            transform.position = spawnPos;
            dir = -projectileDir;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle += 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (posBasedOnMovementDir)
        {
            transform.position = player.transform.position - (player.GetComponent<PlayerMovement>().movementDir * distanceFromPlayer);
            dir = player.GetComponent<PlayerMovement>().movementDir;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //angle += 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.position = player.transform.position + (player.GetComponent<PlayerCombat>().playerLookDir * distanceFromPlayer);
            dir = -player.GetComponent<PlayerCombat>().playerLookDir;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle += 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        objsAttacked = new List<GameObject>();
        if (nonPlayerAttack)
            direction = projectileDir;
        else
            direction = player.GetComponent<PlayerCombat>().playerLookDir;
        timeBetweenEachHit_ = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        if (projectileSpeed > 0)
        {
            transform.position += direction * projectileSpeed * Time.deltaTime;
        }

        for (int i = 0; i < timeBetweenEachHit_.Count; i++)
        {
            if(timeBetweenEachHit_[i] < timeBetweenEachHit && timeBetweenEachHit > 0)
                timeBetweenEachHit_[i] += Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            for (int i = 0; i < objsAttacked.Count; i++)
            {
                if (objsAttacked[i] == other.gameObject && timeBetweenEachHit_[i] >= timeBetweenEachHit)
                {
                    return;
                }
                else if(objsAttacked[i] == other.gameObject)
                {
                    timeBetweenEachHit_[i] = 0;
                }
            }

            if (!nonPlayerAttack)
            {
                for (int i = 0; i < player.GetComponent<PlayerInventory>().UniqueItems.Count; ++i)
                {
                    player.GetComponent<PlayerInventory>().UniqueItems[i].WhenEnemyHit(other.gameObject);
                }
            }

            other.transform.parent.GetComponent<EnemyBase>().TakeDamage(damage);
            timeBetweenEachHit_.Add(0);
            objsAttacked.Add(other.gameObject);
            if (projectileSpeed > 0 && !projectilePierce)
            {
                Destroy(gameObject);
            }
        }

        if(other.gameObject.CompareTag("Chest"))
        {
            Debug.Log(objsAttacked.Count);
            for (int i = 0; i < objsAttacked.Count; i++)
            {
                if (objsAttacked[i] == other.gameObject)
                {
                    return;
                }
            }
            other.gameObject.GetComponent<Animator>().SetTrigger("Open");

            timeBetweenEachHit_.Add(0);
            objsAttacked.Add(other.gameObject);

        }
        //if(other.gameObject.CompareTag("Wall"))
        //{
        //    if (projectileSpeed > 0) 
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
}
