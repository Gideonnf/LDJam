using UnityEngine;

[System.Serializable]
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue m_Dialogue;
    public string m_DialogueSound;

    [HideInInspector]
    public DialogueManager m_DialogueManager;

    public void Start()
    {
        m_DialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        if (m_DialogueManager != null)
        {
            m_DialogueManager.StartDialogue(m_Dialogue);

            //Play Sound
            SoundManager.Instance.Play(m_DialogueSound);
        }
    }
}
