using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject instructionText;
    [SerializeField] float xDistanceFromPlayer;
    [SerializeField] float yDistanceFromPlayer;

    public bool playerNearWagon;


    PlayerMovement playerMovement;
    Animator wagonAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerNearWagon = false;
        wagonAnimator = GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.isPullingCaravan)
        {
            if (player.GetComponent<PlayerMovement>().movementDir != Vector3.zero)
            {
                Vector3 dir = Vector3.zero;
                switch (playerMovement.playerFaceDir)
                {
                    case PlayerMovement.FaceDirection.up:
                        wagonAnimator.SetBool("FaceUp", true);
                        wagonAnimator.SetBool("FaceDown", false);
                        wagonAnimator.SetBool("FaceLeft", false);
                        wagonAnimator.SetBool("FaceRight", false);
                        dir = Vector3.up;
                        transform.position = player.transform.position - (Vector3.up * yDistanceFromPlayer);
                        break;
                    case PlayerMovement.FaceDirection.down:
                        wagonAnimator.SetBool("FaceUp", false);
                        wagonAnimator.SetBool("FaceDown", true);
                        wagonAnimator.SetBool("FaceLeft", false);
                        wagonAnimator.SetBool("FaceRight", false);
                        dir = -Vector3.up;
                        transform.position = player.transform.position + (Vector3.up * yDistanceFromPlayer);
                        break;
                    case PlayerMovement.FaceDirection.left:
                        wagonAnimator.SetBool("FaceUp", false);
                        wagonAnimator.SetBool("FaceDown", false);
                        wagonAnimator.SetBool("FaceLeft", true);
                        wagonAnimator.SetBool("FaceRight", false);
                        dir = -Vector3.right;
                        transform.position = player.transform.position + (Vector3.right * xDistanceFromPlayer);
                        break;
                    case PlayerMovement.FaceDirection.right:
                        wagonAnimator.SetBool("FaceUp", false);
                        wagonAnimator.SetBool("FaceDown", false);
                        wagonAnimator.SetBool("FaceLeft", false);
                        wagonAnimator.SetBool("FaceRight", true);
                        transform.position = player.transform.position - (Vector3.right * xDistanceFromPlayer);
                        dir = Vector3.right;
                        break;
                }
               
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //angle += 90;
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !playerMovement.isPullingCaravan)
        {
            playerNearWagon = true;
            instructionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player && !playerMovement.isPullingCaravan)
        {
            playerNearWagon = false;
            instructionText.SetActive(false);
        }
    }
    public void Interact()
    {
        playerMovement.isPullingCaravan = !playerMovement.isPullingCaravan;
        if (playerMovement.isPullingCaravan)
        {
            instructionText.SetActive(false);
            wagonAnimator.SetBool("PlayerPulling", true);
        }
        else
        {
            wagonAnimator.SetBool("PlayerPulling", false);
        }
    }
}
