using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneTransition : MonoBehaviour
{
    [Header("Dialogue")]
    public Dialogue[] m_DialogueTexts;
    public GameObject[] m_CutScenes;
    public GameObject m_StoryText;
    public Animator m_FadeOutInAnimator;
    public CutSceneDialogue m_CutSceneDialogue;

    int m_CurrentDialogue = 0;
    bool m_AnimPlaying = false;

    public void Clicked()
    {
        if (!m_CutSceneDialogue.m_Talking && !m_AnimPlaying)
        {
            ++m_CurrentDialogue;

            //start fade in and out anim
            m_FadeOutInAnimator.SetTrigger("StartFade");
            m_AnimPlaying = true;
        }
        else
        {
            m_CutSceneDialogue.DisplaySpeech(); //fast forward the speech
        }
    }

    public void FadeOutFinish()
    {
        //set prev inactive
        if (m_CurrentDialogue >= 1)
            m_CutScenes[m_CurrentDialogue - 1].SetActive(false);

        //set current active
        if (m_CurrentDialogue >= m_CutScenes.Length)
        {
            SceneManager.LoadScene("GameScene");
            return;
        }
        else if (m_CurrentDialogue == m_CutScenes.Length - 1)
        {
            m_StoryText.SetActive(false);
        }

        m_CutScenes[m_CurrentDialogue].SetActive(true);
        m_FadeOutInAnimator.SetTrigger("FadeIn");
    }

    public void FadeInFinish()
    {
        if (m_CurrentDialogue < m_CutScenes.Length - 1)
            m_StoryText.SetActive(true);

        m_CutSceneDialogue.StartDialogue(m_DialogueTexts[m_CurrentDialogue]);

        m_AnimPlaying = false;
    }
}
