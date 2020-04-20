using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : SingletonBase<HealthManager>
{
    [Tooltip("Parent object for all the player health")]
    public GameObject PlayerHealthObject;
    [Tooltip("Parent object for all the caravan health")]
    public GameObject CaravanHealthObject;

    public GameObject HealthPrefab;
    //public GameObject CaravanHealthPrefab;

    public Sprite PlayerHealthFilled;
    public Sprite PlayerHealthEmpty;

    public Sprite CaravanHealthFilled;
    public Sprite CaravanHealthEmpty;

    [Header("Configuration for healthbar")]
    public Vector2 PlayerHealthPos = new Vector2(-856, 315);
    public float HealthDistance = 95;

    public Vector2 CaravanHealthPos = new Vector2(-856, 240);
    public float CaravanDistance = 95;

    PlayerStats playerStats;

    List<GameObject> playerHealthList = new List<GameObject>();
    List<GameObject> caravanHealthList = new List<GameObject>();

    int currentPlayerHealth = 0;
    int currentCaravanHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerController.Instance.m_PlayerStats;
        CreatePlayerHealth();
        CreateCaravanHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayerHealth != playerStats.m_CurrentHealth)
        {
            UpdatePlayerHealth();
        }

        else if (currentCaravanHealth != playerStats.m_CurrentCaravanHealth)
        {
            UpdateCaravanHealth();
        }

    }

    public void UpdatePlayerHealth()
    {
        ClearPlayerHealth();
        CreatePlayerHealth();
    }

    public void UpdateCaravanHealth()
    {
        ClearCaravanHealth();
        CreateCaravanHealth();
    }

    void CreatePlayerHealth()
    {
        currentPlayerHealth = playerStats.m_CurrentHealth;

        for(int i = 0; i < playerStats.m_MaxHealth; ++i)
        {
            GameObject HealthObject = Instantiate(HealthPrefab, PlayerHealthObject.transform);

            // Change position
            Vector2 HealthPos = PlayerHealthPos;
            //shift it
            HealthPos.x += HealthDistance * i;
            // Set the position
            HealthObject.GetComponent<RectTransform>().anchoredPosition = HealthPos;

            // If its within the player's current health
            if (i < playerStats.m_CurrentHealth)
            {
                HealthObject.GetComponent<Image>().sprite = PlayerHealthFilled;
            }
            else //The players current heallth is lower than the max
            {
                HealthObject.GetComponent<Image>().sprite = PlayerHealthEmpty;
            }

            playerHealthList.Add(HealthObject);
        }
    }

    void CreateCaravanHealth()
    {
        currentCaravanHealth = playerStats.m_CurrentCaravanHealth;

        for (int i = 0; i < playerStats.m_MaxCaravanHealth; ++i)
        {
            GameObject HealthObject = Instantiate(HealthPrefab, CaravanHealthObject.transform);

            // Change position
            Vector2 HealthPos = CaravanHealthPos;
            //shift it
            HealthPos.x += HealthDistance * i;
            // Set the position
            HealthObject.GetComponent<RectTransform>().anchoredPosition = HealthPos;

            // If its within the player's current health
            if (i < playerStats.m_CurrentCaravanHealth)
            {
                HealthObject.GetComponent<Image>().sprite = CaravanHealthFilled;
            }
            else //The players current heallth is lower than the max
            {
                HealthObject.GetComponent<Image>().sprite = CaravanHealthEmpty;
            }

            caravanHealthList.Add(HealthObject);
        }

    }

    void ClearPlayerHealth()
    {
        // Destroy all the player health
        for(int i = 0; i < playerHealthList.Count; ++i)
        {
            Destroy(playerHealthList[i]);
        }

        // Clear the list
        playerHealthList.Clear();
    }

    void ClearCaravanHealth()
    {
        for (int i = 0; i < caravanHealthList.Count; ++i)
        {
            Destroy(caravanHealthList[i]);
        }

        // Clear the list
        caravanHealthList.Clear();

    }
}
