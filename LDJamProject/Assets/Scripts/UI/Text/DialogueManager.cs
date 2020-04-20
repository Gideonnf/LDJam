using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector]
    public Queue<string> m_Sentences;

    [Header("Dialogue UI")]
    public TextMeshProUGUI m_NameText;
    public TextMeshProUGUI m_DialogueText;
    public GameObject m_TextBox;
    public GameObject m_ArrowText; //only shown when the speech is done
    private Animator m_DialogueAnimator;

    [Header("Sound")]
    public string m_LetterSound;

    private bool m_IsCouroutineRunning = false;
    private string m_CurrentSentence;

    private void Start()
    {
        m_Sentences = new Queue<string>();

        m_DialogueAnimator = m_TextBox.GetComponentInChildren<Animator>();
    }

    //when interacted, call once
    public void StartDialogue(Dialogue dialogue)
    {
        //play animation to pop up dialogue
        ShowSpeechUI(true);
        Talk(dialogue); //show text
    }

    public void Talk(Dialogue dialogue)
    {
        m_NameText.text = dialogue.m_Name;

        m_Sentences.Clear();

        foreach (string sentence in dialogue.m_Sentences)
        {
            m_Sentences.Enqueue(sentence);
        }

        DisplaySpeech();
    }

    public void DisplaySpeech()
    {
        if (!m_IsCouroutineRunning)
        {
            DisplayNextSentence();
        }
        else
        {
            //if player wants to skip the animation, it will speed through and print out fully
            m_IsCouroutineRunning = false;
            StopAllCoroutines();
            m_DialogueText.text = m_CurrentSentence;

            if (m_ArrowText != null)
                m_ArrowText.SetActive(true);
        }
    }

    public void DisplayNextSentence()
    {
        if (m_Sentences.Count == 0) //reach end of queue
        {
            EndDialogue();
            return;
        }

        string sentence = m_Sentences.Dequeue();
        m_CurrentSentence = sentence;
        StartCoroutine(TypeSentence(sentence));

        if (m_ArrowText != null)
            m_ArrowText.SetActive(false);
    }

    void EndDialogue()
    {
        //does animation
        ShowSpeechUI(false);
    }

    void ShowSpeechUI(bool active)
    {
        //TODO:: SHOW PROPER UI

        if (m_TextBox != null)
        {
            m_TextBox.SetActive(active);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        m_IsCouroutineRunning = true;

        m_DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            m_DialogueText.text += letter;
            SoundManager.Instance.Play(m_LetterSound);
            yield return null;
        }

        if (m_ArrowText != null)
            m_ArrowText.SetActive(true);

        m_IsCouroutineRunning = false; //when couroutine stops
    }
}
