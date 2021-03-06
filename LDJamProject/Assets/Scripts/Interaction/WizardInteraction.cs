﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WizardInteraction : NPCTextInteraction
{
    [Header("Ending Dialogue Thing")]
    public Dialogue m_EndDialogue;
    //public string m_DialogueSound;

    //Wizardmenu
    bool ConversationStarted = false;
    // bool m_PlayerNearby = false;
    bool EnoughMonies = false;
    // im just hard coding shit lol
    bool TriggeredFirstDialogue = false;
    bool TriggeredSecondDialogue = false;


    public override void Awake()
    {
        base.Awake();
        //m_PlayerNearby = false;

        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //if (spriteRenderer != null)
        //    spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }

    public void OnEnable()
    {
        SoundManager.Instance.Play("WizardSpawn");
    }

    // Start is called before the first frame update
    void Start()
    {
        // = /WizardMenuReference.GetComponent<WizardMenu>();
    }

    // Update is called once per frame
    public override void Update()
    {
        // base.Update();

        if (m_DialogueManager == null)
            return;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = (int)(transform.position.y * -100);

        // Check if the player has enough monies
        if (PlayerController.Instance.m_PlayerInventory.m_PlayerMoney >= 1000)
        {
            EnoughMonies = true;
        }


        //if npc is currently talking
        if (m_DialogueManager.m_Talking)
        {
            //if click or press the correct button, go to next sentence
            if (Input.GetKeyDown(KeyCode.E))
                NextLine();
        }
        // IF he is not talking
        else
        {
            //if clicked on npc || player go close and interact with npc
            if (m_PlayerNearby)
            {
                //check for input
                if (Input.GetKeyDown(KeyCode.E))
                    TriggerDialogue();
            }

            // 2nd conversation started
            // first dialogue ended
            if (ConversationStarted == true && TriggeredFirstDialogue == true && EnoughMonies)
            {
                // Set 2nd dialogue flag to true
                TriggeredSecondDialogue = true;
                ConversationStarted = false;
            }

            // First conversation started
            // First Dialogue flag is still false
            if (ConversationStarted == true && TriggeredFirstDialogue == false)
            {
                // Reset the boolean flag
                ConversationStarted = false;
                // Set the flag for first dialogue end to true
                TriggeredFirstDialogue = true;
            }

            if (!EnoughMonies && TriggeredFirstDialogue && m_DialogueManager.m_Talking)
            {
                // Start the UI screen
                WizardMenu.Instance.OpenUI();
            }

            if (ConversationStarted == false && TriggeredFirstDialogue == true && TriggeredSecondDialogue == true)
            {
                if (m_DialogueManager.m_Talking)
                    return;

                Animator animator = GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("WizardVamoosh");
                    StartCoroutine(ChangeSceneAfterTime());
                }
                else
                    SceneManager.LoadScene("EndScene");
            }



            //// if npc is done talking
            //// and first dialogue is done
            //if (TriggeredFirstDialogue && TriggerSecondDialogue)
            //{

            //}
        }

        //if(TriggeredFirstDialogue)
        //{
        //    TriggerSecondDialogue = true;
        //}

        //// If the player started hte conversation with the wizard
        //if (ConversationStarted == true && EnoughMonies == false)
        //{
        //    // If the conversation have ended, m_talking is false
        //    if (m_DialogueManager.m_Talking == false)
        //    {
        //        TriggeredFirstDialogue = true;

        //        ConversationStarted = false;
        //        // Start the UI Screen for the trading
        //        WizardMenu.Instance.OpenUI();
        //    }
        //}
    }


    public override void TriggerDialogue()
    {
        // base.TriggerDialogue();
        //if clicked on npc
        if (EnoughMonies == true && TriggeredFirstDialogue == true)
        {
            //TriggerSecondDialogue = true;
            m_DialogueManager.StartDialogue(m_EndDialogue);
            SoundManager.Instance.Play(m_DialogueSound);
        }
        else
        {

            m_DialogueManager.StartDialogue(m_Dialogue);
            SoundManager.Instance.Play(m_DialogueSound);

        }

        ConversationStarted = true;
    }

    public override void NextLine()
    {
        base.NextLine();
    }

    IEnumerator ChangeSceneAfterTime()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("EndScene");
    }
}
