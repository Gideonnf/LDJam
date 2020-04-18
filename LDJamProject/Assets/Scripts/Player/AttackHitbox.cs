using System.Collections;
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

    GameObject player;
    List<GameObject> objsAttacked;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + (player.GetComponent<PlayerCombat>().playerLookDir * distanceFromPlayer);
        Vector3 dir = -player.GetComponent<PlayerCombat>().playerLookDir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        objsAttacked = new List<GameObject>();
        direction = player.GetComponent<PlayerCombat>().playerLookDir;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) 
        {
            Destroy(gameObject);
        }
        if(projectileSpeed > 0)
        {
            transform.position += direction * projectileSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            foreach(GameObject objAttacked in objsAttacked)
            {
                if (objAttacked == other.gameObject)
                    return;
            }

            EquipmentManager.Instance.NormalItemDrop(gameObject.transform.position);

            Destroy(other.gameObject);
            if (projectileSpeed > 0 && !projectilePierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
