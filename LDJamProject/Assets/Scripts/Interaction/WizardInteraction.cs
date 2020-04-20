using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardInteraction : NPCTextInteraction
{
   //Wizardmenu
    bool ConversationStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        // = /WizardMenuReference.GetComponent<WizardMenu>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        // If the player started hte conversation with the wizard
        if (ConversationStarted)
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
        base.TriggerDialogue();

        ConversationStarted = true;
    }
}
