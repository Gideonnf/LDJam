using UnityEngine;

public class NPCTextInteraction : MonoBehaviour
{
    [Header("Dialogue")]
    public Dialogue m_Dialogue;
    public string m_DialogueSound;

    [Header("Dialogue UI")]
    public DialogueManager m_DialogueManager;

    bool m_PlayerNearby = false;

    public void Awake()
    {
        m_PlayerNearby = false;
    }

    private void Update()
    {
        if (m_DialogueManager == null)
            return;

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
                TriggerDialogue();
            }
        }
    }

    public void TriggerDialogue()
    {
        //if clicked on npc
        m_DialogueManager.StartDialogue(m_Dialogue);
        SoundManager.Instance.Play(m_DialogueSound);
    }

    public void NextLine()
    {
        m_DialogueManager.DisplaySpeech();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            m_PlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            m_PlayerNearby = false;
    }
}
