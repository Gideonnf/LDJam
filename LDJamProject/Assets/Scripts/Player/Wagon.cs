using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public static Wagon instance = null;
    [SerializeField] GameObject player;
    [SerializeField] GameObject instructionText;
    [SerializeField] GameObject nearWagon;
    [SerializeField] float xDistanceFromPlayer;
    [SerializeField] float yDistanceFromPlayer;
    [SerializeField] float timeItTakesToPickUpCaravan;
    [SerializeField] GameObject upDownHitbox;
    [SerializeField] GameObject leftRightHitbox;

    public bool playerNearWagon;

    PlayerMovement playerMovement;
    Animator wagonAnimator;

    GameObject wagonMoveSFX;
    bool isIdling;
    float pickUpCaravanTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Wagon detected! Deleting new instance");
            instance.gameObject.SetActive(true);
        }
        instance = this;
        playerNearWagon = false;
        wagonAnimator = GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
        isIdling = false;
        pickUpCaravanTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.isPickingUpCaravan)
        {
            pickUpCaravanTimer += Time.deltaTime;
            if (pickUpCaravanTimer >= timeItTakesToPickUpCaravan)
                playerMovement.isPickingUpCaravan = false;
        }

        else if(playerMovement.isPullingCaravan)
        {
            if (player.GetComponent<PlayerMovement>().movementDir != Vector3.zero)
            {
                if(isIdling)
                {
                    isIdling = false;
                    wagonMoveSFX = SoundManager.Instance.Play("CaravanMove");
                }
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
                        upDownHitbox.SetActive(true);
                        leftRightHitbox.SetActive(false);
                        break;
                    case PlayerMovement.FaceDirection.down:
                        wagonAnimator.SetBool("FaceUp", false);
                        wagonAnimator.SetBool("FaceDown", true);
                        wagonAnimator.SetBool("FaceLeft", false);
                        wagonAnimator.SetBool("FaceRight", false);
                        dir = -Vector3.up;
                        transform.position = player.transform.position + (Vector3.up * yDistanceFromPlayer);
                        upDownHitbox.SetActive(true);
                        leftRightHitbox.SetActive(false);
                        break;
                    case PlayerMovement.FaceDirection.left:
                        wagonAnimator.SetBool("FaceUp", false);
                        wagonAnimator.SetBool("FaceDown", false);
                        wagonAnimator.SetBool("FaceLeft", true);
                        wagonAnimator.SetBool("FaceRight", false);
                        dir = -Vector3.right;
                        transform.position = player.transform.position + (Vector3.right * xDistanceFromPlayer);
                        upDownHitbox.SetActive(false);
                        leftRightHitbox.SetActive(true);
                        break;
                    case PlayerMovement.FaceDirection.right:
                        wagonAnimator.SetBool("FaceUp", false);
                        wagonAnimator.SetBool("FaceDown", false);
                        wagonAnimator.SetBool("FaceLeft", false);
                        wagonAnimator.SetBool("FaceRight", true);
                        transform.position = player.transform.position - (Vector3.right * xDistanceFromPlayer);
                        dir = Vector3.right;
                        upDownHitbox.SetActive(false);
                        leftRightHitbox.SetActive(true);
                        break;
                }
               
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //angle += 90;
                nearWagon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                if (!isIdling)
                {
                    isIdling = true;
                    if (wagonMoveSFX != null)
                        wagonMoveSFX.GetComponent<AudioSource>().Stop();
                }
            }
        }
        else
        {
            if (!isIdling)
            {
                isIdling = true;
                if (wagonMoveSFX != null)
                    wagonMoveSFX.GetComponent<AudioSource>().Stop();
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
            playerMovement.isPickingUpCaravan = true;
            pickUpCaravanTimer = 0;
        }
        else
        {
            wagonAnimator.SetBool("PlayerPulling", false);
        }
    }
}
