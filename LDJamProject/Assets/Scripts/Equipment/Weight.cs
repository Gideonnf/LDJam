using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight 
{
    public class Entry
    {
        public float accumulatedWeight = 0.0f;
        public GameObject item = null;

        public Entry(GameObject _item, float _AccumulatedWeight)
        {
            accumulatedWeight = _AccumulatedWeight;
            item = _item;
        }
    }

    public List<Entry> entries;
    public float accumulatedWeight = 0;
    
    public Weight()
    {
        entries = new List<Entry>();
        accumulatedWeight = 0;
    }

    public void AddEntry(GameObject item, float weight)
    {
        accumulatedWeight += weight;
        Entry newEntry = new Entry(item, accumulatedWeight);
        entries.Add(newEntry);
    }

    public GameObject GetRandom()
    {
        float r = Random.Range(0.0f, (accumulatedWeight + (accumulatedWeight * 0.8f)));

        

        foreach (Entry entry in entries)
        {
            if (entry.accumulatedWeight >= r)
            {
                return entry.item;
            }
        }

        return null;
    }

    public GameObject GetConfirmRandom()
    {
        float r = Random.Range(0.0f, accumulatedWeight);

        foreach (Entry entry in entries)
        {
            if (entry.accumulatedWeight >= r)
            {
                return entry.item;
            }
        }

        return null;
    }

    public void ClearList()
    {
        entries.Clear();
    }
}
