using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonBase<EnemyManager>
{
    public enum EnemyType
    {
        MELEE_A,        // Suicide bombing skulls
        MELEE_B,        // Big boi but slow boi

        RANGED_A,       // Ranged shooting creatures
        //BOSS_A,         // The boss
        //BOSS_MINION_A,  // The boss' minion
        NUM_ENEMY_TYPES
    }

    // The prefab list of enemies
    [SerializeField] EnemyPooledObject[] EnemyPrefabs = new EnemyPooledObject[(int)EnemyType.NUM_ENEMY_TYPES];
    // The pool of objects
    List<GameObject>[] m_enemyPool = new List<GameObject>[(int)EnemyType.NUM_ENEMY_TYPES];
    int[] m_indexes = new int[(int)EnemyType.NUM_ENEMY_TYPES];

    private void Start()
    {
        // Reset all the indexes to 0
        for (int i = 0; i < (int)EnemyType.NUM_ENEMY_TYPES; ++i)
        {
            m_indexes[i] = 0;
        }
        // create new enemies
        int currEnemyIndex = 0;
        foreach(EnemyPooledObject enemyInfo in EnemyPrefabs)
        {
            m_enemyPool[currEnemyIndex] = new List<GameObject>();
            for (int i = 0; i < enemyInfo.startingAmount; ++i)
            {
                GameObject newEnemy = Instantiate(enemyInfo.enemyPrefab);
                newEnemy.gameObject.SetActive(false);
                m_enemyPool[currEnemyIndex].Add(newEnemy);
            }
            ++currEnemyIndex;
        }
    }

    /// <summary>
    /// Fetches a new enemy. Automatically sets them active
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    public GameObject FetchEnemy(EnemyType _type)
    {
        GameObject newEnemy = null; // The enemy to return
        int tempIndex = m_indexes[(int)_type];
        ref int actualIndex = ref m_indexes[(int)_type];
        ref List<GameObject> selectedEnemyPool = ref m_enemyPool[(int)_type];
        // If the pool size was 0 anyway
        if (selectedEnemyPool.Count == 0)
        {
            var newPrefab = EnemyPrefabs[(int)_type].enemyPrefab;
            for (int i = 0; i < EnemyPrefabs[(int)_type].amountToPool; ++i)
            {
                selectedEnemyPool.Add(Instantiate(newPrefab));
            }
        }
        // If the enemy is inactive and ready for deployment
        if (!selectedEnemyPool[tempIndex].gameObject.activeSelf)
        {
            if (++actualIndex == selectedEnemyPool.Count)
                actualIndex = 0;    // Wrap back to 0 if the index reaches the end
            newEnemy = selectedEnemyPool[tempIndex]; // Init the enemy and return
            newEnemy.GetComponent<EnemyBase>().Init();
            return newEnemy;
        }
        // Search for an enemy if inactive one wasn't found
        while (newEnemy == null)
        {
            if (++tempIndex == selectedEnemyPool.Count)
                tempIndex = 0;    // Wrap back to 0 if the index reaches the end
            if (tempIndex == actualIndex)   // Start creating more to pool instead
            {
                actualIndex = selectedEnemyPool.Count + 1;
                if (actualIndex == selectedEnemyPool.Count)
                    actualIndex = 0;
                int numberToCreate = EnemyPrefabs[(int)_type].amountToPool;
                GameObject newPrefab = EnemyPrefabs[(int)_type].enemyPrefab;
                for (int i = 0; i < numberToCreate; ++i)
                {
                    m_enemyPool[(int)_type].Add(Instantiate(newPrefab));
                }
                newEnemy = selectedEnemyPool[actualIndex];
                break;
            }
            if (selectedEnemyPool[tempIndex].gameObject.activeSelf)
            {
                if (++tempIndex == selectedEnemyPool.Count)
                    tempIndex = 0;
                actualIndex = tempIndex;
                newEnemy = selectedEnemyPool[tempIndex];
            }

        }
        newEnemy.gameObject.SetActive(true);
        newEnemy.GetComponent<EnemyBase>().Init();
        return newEnemy;
    }

    [System.Serializable]
    public struct EnemyPooledObject
    {
        public GameObject enemyPrefab;
        public int startingAmount;
        public int amountToPool;
    }
}
