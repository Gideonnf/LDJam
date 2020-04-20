﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardInteraction : NPCTextInteraction
{
    [Header("Ending Dialogue Thing")]
    public Dialogue m_EndDialogue;
    //public string m_DialogueSound;

    //Wizardmenu
    bool ConversationStarted = false;
   // bool m_PlayerNearby = false;
    bool EnoughMonies = false;

    public override void Awake()
    {
        base.Awake();
        //m_PlayerNearby = false;

        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //if (spriteRenderer != null)
        //    spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
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

        // Check if the player has enough monies
        if (PlayerController.Instance.m_PlayerInventory.m_PlayerMoney >= 1000)
        {
            EnoughMonies = true;
        }


        //if npc is not currently talking
        if (m_DialogueManager.m_Talking)
        {
            //if click or press the correct button, go to next sentence
            if (Input.GetKeyDown(KeyCode.E))
                NextLine();
        }
        else
        {
            //if clicked on npc || player go close and interact with npc
            if (m_PlayerNearby)
            {
                //check for input
                if (Input.GetKeyDown(KeyCode.E))
                    TriggerDialogue();
            }
        }

        // If the player started hte conversation with the wizard
        if (ConversationStarted == true && EnoughMonies == false)
        {
            // If the conversation have ended, m_talking is false
            if (m_DialogueManager.m_Talking == false)
            {
                ConversationStarted = false;
                // Start the UI Screen for the trading
                WizardMenu.Instance.OpenUI();
            }
        }
    }


    public override void TriggerDialogue()
    {
        // base.TriggerDialogue();
        //if clicked on npc
        if (EnoughMonies == true)
        {
            m_DialogueManager.StartDialogue(m_EndDialogue);
            SoundManager.Instance.Play(m_DialogueSound);
        }
        else
        {
            m_DialogueManager.StartDialogue(m_Dialogue);
            SoundManager.Instance.Play(m_DialogueSound);

            ConversationStarted = true;
        }

    }

    public override void NextLine()
    {
        base.NextLine();
    }
}
