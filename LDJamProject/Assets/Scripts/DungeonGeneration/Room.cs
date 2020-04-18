using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room 
{
    public GameObject m_RoomPrefab;

    public RoomOpeningInfo m_RoomOpeningInfo;
}

[System.Serializable]
public class RoomOpeningInfo
{
    public RoomOpeningTypes m_RoomOpeningType;

    public int m_NumberOfOpenings = 1;

    [Header("Opening directions")]
    public bool m_RightOpening = false;
    public bool m_LeftOpening = false;
    public bool m_UpOpening = false;
    public bool m_DownOpening = false;
}
