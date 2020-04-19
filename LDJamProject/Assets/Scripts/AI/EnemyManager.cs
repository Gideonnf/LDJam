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
    List<EnemyBase>[] m_enemyPool = new List<EnemyBase>[(int)EnemyType.NUM_ENEMY_TYPES];
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
            m_enemyPool[currEnemyIndex] = new List<EnemyBase>();
            for (int i = 0; i < enemyInfo.startingAmount; ++i)
            {
                EnemyBase newEnemy = Instantiate(enemyInfo.enemyPrefab);
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
    public EnemyBase FetchEnemy(EnemyType _type)
    {
        EnemyBase newEnemy = null; // The enemy to return
        int tempIndex = m_indexes[(int)_type];
        ref int actualIndex = ref m_indexes[(int)_type];
        ref List<EnemyBase> selectedEnemyPool = ref m_enemyPool[(int)_type];
        // If the enemy is inactive and ready for deployment
        if (!selectedEnemyPool[tempIndex].gameObject.activeSelf)
        {
            if (++actualIndex == selectedEnemyPool.Count)
                actualIndex = 0;    // Wrap back to 0 if the index reaches the end
            newEnemy = selectedEnemyPool[tempIndex]; // Init the enemy and return
            newEnemy.Init();
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
                EnemyBase newPrefab = EnemyPrefabs[(int)_type].enemyPrefab;
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
        newEnemy.Init();
        return newEnemy;
    }

    [System.Serializable]
    public struct EnemyPooledObject
    {
        public EnemyBase enemyPrefab;
        public int startingAmount;
        public int amountToPool;
    }
}
