using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int damage;

    [SerializeField] float lifetime;
    [SerializeField] float distanceFromPlayer;
    GameObject player;
    List<GameObject> objsAttacked;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + (player.GetComponent<PlayerCombat>().playerLookDir * distanceFromPlayer);
        Vector3 dir = -player.GetComponent<PlayerCombat>().playerLookDir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("peen");
        if (other.gameObject.CompareTag("Enemy"))
        {
            //foreach(GameObject objAttacked in objsAttacked)
            //{
            //    if (objAttacked == other.gameObject)
            //        return;
            //}
            Debug.Log("peen2");
            Destroy(other.gameObject);
        }
    }
}
