using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    public Vector2Int m_MinMaxRoomNumber = new Vector2Int(10,12);
    public List<Room> m_RoomTypeData = new List<Room>();
    public GameObject m_RoomParent;

    Dictionary<RoomOpeningTypes, RoomOpeningInfo> m_RoomOpeningsData = new Dictionary<RoomOpeningTypes, RoomOpeningInfo>();
    Dictionary<RoomOpeningTypes, GameObject> m_RoomObjectData = new Dictionary<RoomOpeningTypes, GameObject>();

    Dictionary<Vector2Int, RoomOpeningTypes> m_Taken = new Dictionary<Vector2Int, RoomOpeningTypes>();

    Vector2 m_RoomTileWidthHeight;

    public void Start()
    {
        //sort the rooms accordingly
        foreach(Room room in m_RoomTypeData)
        {
            m_RoomOpeningsData.Add(room.m_RoomOpeningInfo.m_RoomOpeningType, room.m_RoomOpeningInfo);
            m_RoomObjectData.Add(room.m_RoomOpeningInfo.m_RoomOpeningType, room.m_RoomPrefab);
        }

        //get width and heigt of room
        SpriteRenderer spriteRenderer = null;
        if (m_RoomObjectData.ContainsKey(RoomOpeningTypes.L_R_U_D_OPENING))
        {
            spriteRenderer = m_RoomObjectData[RoomOpeningTypes.L_R_U_D_OPENING].GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
            m_RoomTileWidthHeight = new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);

        GenerateLevel();
    }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            GenerateLevel();
        }
    }

    public void GenerateLevel()
    {
        m_Taken.Clear();
        foreach (Transform child in m_RoomParent.transform)
        {
            Destroy(child.gameObject);
        }

        Vector2Int start = Vector2Int.zero;
        Queue<Vector2Int> m_RoomLocations = new Queue<Vector2Int>();
        m_Taken.Add(start, RoomOpeningTypes.L_R_U_D_OPENING);
        m_RoomLocations.Enqueue(start);

        while (m_RoomLocations.Count > 0)
        {
            Vector2Int roomLocation = m_RoomLocations.Dequeue();
            RoomOpeningTypes roomType = m_Taken[roomLocation];

            if (m_Taken.Count >= m_MinMaxRoomNumber.y)
            {
                //dont add anymore, edit current ones left in the queue instead
                if (m_Taken.ContainsKey(roomLocation))
                    m_Taken[roomLocation] = ChangeCurrentRoomToExact(roomLocation);
            }
            else
            {
                RoomOpeningInfo openingInfo = null;

                 if (m_RoomOpeningsData.ContainsKey(roomType))
                    openingInfo = m_RoomOpeningsData[roomType];

                if (openingInfo == null)
                    continue;

                //make sure to check the door direction and have no rooms that is already taking that space
                Vector2Int newPos = roomLocation + new Vector2Int(0, 1);
                if (!m_Taken.ContainsKey(newPos) && openingInfo.m_UpOpening)
                {
                    if (AddToTaken(newPos, AddNextRoom(newPos)))
                        m_RoomLocations.Enqueue(newPos);
                }

                newPos = roomLocation + new Vector2Int(0, -1);
                if (!m_Taken.ContainsKey(newPos) && openingInfo.m_DownOpening)
                {
                    if (AddToTaken(newPos, AddNextRoom(newPos)))
                        m_RoomLocations.Enqueue(newPos);
                }

                newPos = roomLocation + new Vector2Int(1, 0);
                if (!m_Taken.ContainsKey(newPos) && openingInfo.m_RightOpening)
                {
                    if (AddToTaken(newPos, AddNextRoom(newPos)))
                        m_RoomLocations.Enqueue(newPos);
                }

                newPos = roomLocation + new Vector2Int(-1, 0);
                if (!m_Taken.ContainsKey(newPos) && openingInfo.m_LeftOpening)
                {
                    if (AddToTaken(newPos, AddNextRoom(newPos)))
                        m_RoomLocations.Enqueue(newPos);
                }
            }
        }

        InstantiateRooms();
        InitUI();
    }

    public bool AddToTaken(Vector2Int pos, RoomOpeningTypes type)
    {
        if (m_Taken.ContainsKey(pos))
            return false;

        m_Taken.Add(pos, type);
        return true;
    }

    public void CheckNeighbours(Vector2Int pos, ref int numberOfNeighbours, ref bool rightNeighbour, ref bool leftNeighbour, ref bool upNeighbour, ref bool downNeighbour)
    {
        //check if there are any neighbors
        Vector2Int offset = pos + new Vector2Int(0, 1);
        if (m_Taken.ContainsKey(offset))
        {
            upNeighbour = true;
            ++numberOfNeighbours;
        }

        //check down
        offset = pos + new Vector2Int(0, -1);
        if (m_Taken.ContainsKey(offset))
        {
            downNeighbour = true;
            ++numberOfNeighbours;
        }

        //check right
        offset = pos + new Vector2Int(1, 0);
        if (m_Taken.ContainsKey(offset))
        {
           rightNeighbour = true;
           ++numberOfNeighbours;
        }

        //check left
        offset = pos + new Vector2Int(-1, 0);
        if (m_Taken.ContainsKey(offset))
        {
            leftNeighbour = true;
            ++numberOfNeighbours;
        }
    }

    public void CheckNeighboursAndOpenings(Vector2Int pos, ref int numberOfNeighbours,ref bool rightNeighbour, ref bool leftNeighbour, ref bool upNeighbour, ref bool downNeighbour)
    {
        //check if there are any neighbors and neighbors have possible path to them
        RoomOpeningInfo neightbourRoomOpenings = null;

        Vector2Int offset = pos + new Vector2Int(0, 1);
        if (m_Taken.ContainsKey(offset))
        {
            neightbourRoomOpenings = m_RoomOpeningsData[m_Taken[offset]]; //get the neighbour opening room data
            if (neightbourRoomOpenings.m_DownOpening) //if neighbour can connect too
            {
                upNeighbour = true;
                ++numberOfNeighbours;
            }
        }

        //check down
        offset = pos + new Vector2Int(0, -1);
        if (m_Taken.ContainsKey(offset))
        {
            neightbourRoomOpenings = m_RoomOpeningsData[m_Taken[offset]]; //get the neighbour opening room data
            if (neightbourRoomOpenings.m_UpOpening) //if neighbour can connect too
            {
                downNeighbour = true;
                ++numberOfNeighbours;
            }
        }

        //check right
        offset = pos + new Vector2Int(1, 0);
        if (m_Taken.ContainsKey(offset))
        {
            neightbourRoomOpenings = m_RoomOpeningsData[m_Taken[offset]]; //get the neighbour opening room data
            if (neightbourRoomOpenings.m_LeftOpening) //if neighbour can connect too
            {
                rightNeighbour = true;
                ++numberOfNeighbours;
            }
        }

        //check left
        offset = pos + new Vector2Int(-1, 0);
        if (m_Taken.ContainsKey(offset))
        {
            neightbourRoomOpenings = m_RoomOpeningsData[m_Taken[offset]]; //get the neighbour opening room data
            if (neightbourRoomOpenings.m_RightOpening) //if neighbour can connect too
            {
                leftNeighbour = true;
                ++numberOfNeighbours;
            }
        }
    }

    public RoomOpeningTypes ChangeCurrentRoomToExact(Vector2Int pos)
    {
        bool upNeighbour, downNeighbour, rightNeighbour, leftNeighbour;
        upNeighbour = downNeighbour = rightNeighbour = leftNeighbour = false;
        int numberOfNeighbours = 0;

        CheckNeighboursAndOpenings(pos, ref numberOfNeighbours, ref rightNeighbour, ref leftNeighbour, ref upNeighbour, ref downNeighbour);

        return GetExactRoomType(numberOfNeighbours, rightNeighbour, leftNeighbour, upNeighbour, downNeighbour);
    }

    public RoomOpeningTypes AddNextRoom(Vector2Int pos)
    {
        bool upNeighbour, downNeighbour, rightNeighbour, leftNeighbour;
        upNeighbour = downNeighbour = rightNeighbour = leftNeighbour = false;
        int numberOfNeighbours = 0;

        CheckNeighbours(pos, ref numberOfNeighbours, ref rightNeighbour, ref leftNeighbour, ref upNeighbour, ref downNeighbour);

        //check the number of neighbours, check the direction for 'specific' ones
        List<RoomOpeningTypes> possibleRooms = GetPossibleRoomTypes(pos, numberOfNeighbours, rightNeighbour, leftNeighbour, upNeighbour, downNeighbour);

        if (possibleRooms.Count > 0)
            return possibleRooms[Random.Range(0, possibleRooms.Count - 1)];
        else
            return RoomOpeningTypes.NO_OPENING;
    }

    public List<RoomOpeningTypes> GetPossibleRoomTypes(Vector2Int pos, int minNumberOfOpenings, bool rightNeighbour, bool leftNeighbour, bool upNeighbour, bool downNeighbour)
    {
        List<RoomOpeningTypes> possibleRooms = new List<RoomOpeningTypes>();

        RoomOpeningInfo neightbourRoomOpenings = null;
        bool downNeighbourAvail, upNeighbourAvail, rightNeightbourAvail, leftneighbourAvail;
        downNeighbourAvail = upNeighbourAvail = rightNeightbourAvail = leftneighbourAvail = true;

        //check if the up neighbour is available to link
        Vector2Int offset = pos + new Vector2Int(0, 1);
        if (upNeighbour)
        {
            neightbourRoomOpenings = m_RoomOpeningsData[m_Taken[offset]]; //get the neighbour opening room data                
            upNeighbourAvail = neightbourRoomOpenings.m_DownOpening;
        }

        offset = pos + new Vector2Int(0, -1);
        if (downNeighbour)
        {
            neightbourRoomOpenings = m_RoomOpeningsData[m_Taken[offset]]; //get the neighbour opening room data                
            downNeighbourAvail = neightbourRoomOpenings.m_UpOpening;
        }

        //check if right neighbour is available to link
        offset = pos + new Vector2Int(1, 0);
        if (rightNeighbour)
        {
            neightbourRoomOpenings = m_RoomOpeningsData[m_Taken[offset]]; //get the neighbour opening room data                
            rightNeightbourAvail = neightbourRoomOpenings.m_LeftOpening;
        }

        offset = pos + new Vector2Int(-1, 0);
        if (leftNeighbour)
        {
            neightbourRoomOpenings = m_RoomOpeningsData[m_Taken[offset]]; //get the neighbour opening room data                
            leftneighbourAvail = neightbourRoomOpenings.m_RightOpening;
        }


        foreach (KeyValuePair<RoomOpeningTypes, RoomOpeningInfo> roomPair in m_RoomOpeningsData)
        {
            RoomOpeningInfo room = roomPair.Value;

            if (room.m_NumberOfOpenings < minNumberOfOpenings)
                continue;

            if (rightNeighbour) //if theres a right neighbour
            {
                if (!rightNeightbourAvail && room.m_RightOpening) //if theres an opening abv, but up neighbour cant link
                    continue;
                else if (rightNeightbourAvail && !room.m_RightOpening) //if neighbour can take but room no opening
                    continue;
            }

            if (leftNeighbour) 
            {
                if (!leftneighbourAvail && room.m_LeftOpening) //if theres an opening abv, but up neighbour cant link
                    continue;
                else if (leftneighbourAvail && !room.m_LeftOpening) //if neighbour can take but room no opening
                    continue;
            }

            if (upNeighbour) 
            {
                if (!upNeighbourAvail && room.m_UpOpening) //if theres an opening abv, but up neighbour cant link
                    continue;
                else if (upNeighbourAvail && !room.m_UpOpening) //if neighbour can take but room no opening
                    continue;
            }

            if (downNeighbour) 
            {
                if (!downNeighbourAvail && room.m_DownOpening) //if theres an opening abv, but up neighbour cant link
                    continue;
                else if (downNeighbourAvail && !room.m_DownOpening) //if neighbour can take but room no opening
                    continue;
            }

            possibleRooms.Add(room.m_RoomOpeningType);
        }

        return possibleRooms;
    }

    public RoomOpeningTypes GetExactRoomType(int minNumberOfOpenings, bool rightNeighbour, bool leftNeighbour, bool upNeighbour, bool downNeighbour)
    {
        foreach (KeyValuePair<RoomOpeningTypes, RoomOpeningInfo> roomPair in m_RoomOpeningsData)
        {
            RoomOpeningInfo room = roomPair.Value;

            if (room.m_NumberOfOpenings != minNumberOfOpenings)
                continue;

            //everything must be exact
            if (rightNeighbour == room.m_RightOpening &&
                leftNeighbour == room.m_LeftOpening &&
                upNeighbour == room.m_UpOpening &&
                downNeighbour == room.m_DownOpening)
            {
                return room.m_RoomOpeningType;
            }
        }

        return RoomOpeningTypes.NO_OPENING;
    }

    public void InstantiateRooms()
    {
        foreach (KeyValuePair<Vector2Int,RoomOpeningTypes> roomInfo in m_Taken)
        {
            Vector2Int gridPos = roomInfo.Key;
            RoomOpeningTypes roomType = roomInfo.Value;

            //if for some reason theres no opening, check the surrounds and give it one
            if (roomType == RoomOpeningTypes.NO_OPENING)
                roomType = ChangeCurrentRoomToExact(gridPos);

            if (m_RoomObjectData.ContainsKey(roomType))
            {
                Vector2 worldPos = new Vector2(gridPos.x * m_RoomTileWidthHeight.x, gridPos.y * m_RoomTileWidthHeight.y);
                GameObject room = Instantiate(m_RoomObjectData[roomType], worldPos, m_RoomObjectData[roomType].transform.rotation);

                if (m_RoomParent != null)
                    room.transform.parent = m_RoomParent.transform;
            }
        }           
    }

    public void InitUI()
    {
        DungeonMinimap.Instance.InitMiniMap(m_Taken);
    }
}
