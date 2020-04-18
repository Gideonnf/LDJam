using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject instructionText;
    [SerializeField] float distanceFromPlayer;

    public bool playerNearWagon;
    public bool playerPullingWagon;

    // Start is called before the first frame update
    void Start()
    {
        playerNearWagon = false;
        playerPullingWagon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPullingWagon)
        {
            if (player.GetComponent<PlayerMovement>().movementDir != new Vector3(0, 0, 0))
            {
                transform.position = player.transform.position - (player.GetComponent<PlayerMovement>().movementDir * distanceFromPlayer);
                Vector3 dir = player.GetComponent<PlayerMovement>().movementDir;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                angle += 90;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !playerPullingWagon)
        {
            playerNearWagon = true;
            instructionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player && !playerPullingWagon)
        {
            playerNearWagon = false;
            instructionText.SetActive(false);
        }
    }
    public void Interact()
    {
        playerPullingWagon = !playerPullingWagon;
        if(playerPullingWagon)
            instructionText.SetActive(false);
    }
}
