using UnityEngine;

//host info about what we need in the dialogue
[System.Serializable]
public class Dialogue 
{
    [TextArea(3, 10)]
    public string[] m_Sentences;
    public string m_Name;
}
