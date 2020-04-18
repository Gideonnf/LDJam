using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RoomTypeUI
{
    public RoomOpeningTypes m_RoomType;
    public Sprite m_RoomDisplayImage;
}

public class DungeonMinimap : SingletonBase<DungeonMinimap>
{
    public List<RoomTypeUI> m_RoomUIData = new List<RoomTypeUI>();

    [Header("Main UI")]
    public GameObject m_MiniMapParent;
    public float m_LerpSpeed = 1.2f;
    public float m_LerpLimit = 0.9f;

    [Header("UI Room")]
    public GameObject m_RoomUIPrefab;
    public GameObject m_RoomUIParent;
    public Color m_HighlightedColor; //for room player is in
    public Color m_VisitedRoomColor; //for rooms player went out of

    [Header("UI Icons")]
    public GameObject m_PlayerIcon;
    public GameObject m_BossIcon;

    float m_LerpTimer = 0.0f;
    Vector2 m_SizeOfRoomUI;
    Dictionary<Vector2Int, Image> m_RoomUIImages = new Dictionary<Vector2Int, Image>();
    Dictionary<RoomOpeningTypes, Sprite> m_RoomUI = new Dictionary<RoomOpeningTypes, Sprite>();

    Vector2Int m_CurrHighlightedRoom = new Vector2Int(0,0);

    public void Start()
    {
        foreach(RoomTypeUI uiData in m_RoomUIData)
        {
            m_RoomUI.Add(uiData.m_RoomType, uiData.m_RoomDisplayImage);
        }

        RectTransform rectTransform = m_RoomUIPrefab.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            m_SizeOfRoomUI = rectTransform.sizeDelta;
        }
    }

    public void ResetUI()
    {
        m_RoomUIImages.Clear();

        foreach (Transform child in m_RoomUIParent.transform)
        {
            Destroy(child.gameObject);
        }

        m_PlayerIcon.transform.localPosition = Vector2.zero;
        m_BossIcon.transform.localPosition = Vector2.zero;
    }

    public void InitMiniMap(Dictionary<Vector2Int, RoomOpeningTypes> m_RoomsGenerated, Vector2Int startingRoomGridPos, Vector2Int bossRoomGridPos)
    {
        ResetUI();

        //get the positiona and type of map and print accordingly
        foreach (KeyValuePair<Vector2Int, RoomOpeningTypes> room in m_RoomsGenerated)
        {
            RoomOpeningTypes type = room.Value;
            Vector2Int gridPos = room.Key;

            GameObject uiObject = Instantiate(m_RoomUIPrefab, m_RoomUIParent.transform);
            uiObject.transform.localPosition = new Vector2(gridPos.x * m_SizeOfRoomUI.x, gridPos.y * m_SizeOfRoomUI.y);

            Image uiImage = uiObject.GetComponent<Image>();
            if (uiImage != null)
            {
                if (m_RoomUI.ContainsKey(type))
                    uiImage.sprite = m_RoomUI[type];

                //set all rooms inactive
                uiObject.SetActive(false);
                m_RoomUIImages.Add(gridPos, uiImage);
            }
        }

        SetIcons(startingRoomGridPos, bossRoomGridPos);
    }

    public void SetIcons(Vector2Int startingRoomGridPos, Vector2Int bossRoomGridPos)
    {
        if (m_RoomUIImages.ContainsKey(bossRoomGridPos))
            m_BossIcon.transform.localPosition = m_RoomUIImages[bossRoomGridPos].transform.localPosition;

        PlayerChangeRoom(startingRoomGridPos);
    }

    public void PlayerChangeRoom(Vector2Int playerNewPos)
    {
        //make prev room the image more faded out 
        if (m_RoomUIImages.ContainsKey(m_CurrHighlightedRoom))
        {
            m_RoomUIImages[m_CurrHighlightedRoom].color = m_VisitedRoomColor;
        }

        //current room image active + opacity full
        if (m_RoomUIImages.ContainsKey(playerNewPos))
        {
            m_RoomUIImages[playerNewPos].gameObject.SetActive(true);
            m_RoomUIImages[playerNewPos].color = m_HighlightedColor;
            m_PlayerIcon.transform.localPosition = m_RoomUIImages[playerNewPos].transform.localPosition;
        }

        //lerp the UI
        StartCoroutine(LerpMapToCentre());
    }

    IEnumerator LerpMapToCentre()
    {
        //position the entire map to the centre
        m_LerpTimer += Time.deltaTime * m_LerpSpeed;
        while (m_LerpTimer <= m_LerpLimit)
        {
            if (m_MiniMapParent != null)
                m_MiniMapParent.transform.localPosition = Vector2.Lerp(m_MiniMapParent.transform.localPosition, -m_PlayerIcon.transform.localPosition, m_LerpTimer);

            yield return null;
        }

        m_MiniMapParent.transform.localPosition = -m_PlayerIcon.transform.localPosition;
    }
}
