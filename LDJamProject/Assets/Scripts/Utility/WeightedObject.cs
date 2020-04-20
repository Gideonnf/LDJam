using System;
using System.Collections.Generic;

// Template class from
// https://gamedev.stackexchange.com/questions/162976/how-do-i-create-a-weighted-collection-and-then-pick-a-random-element-from-it


class WeightedObject<T>
{
    public struct Entry
    {
        public float accumulatedWeight;
        public T item;
    }

    public List<Entry> entries = new List<Entry>();
    public float accumulatedWeight;
    public Random rand = new Random();
    
    // Adds a weighted object to the list
    public void AddEntry(T item, float weight)
    {
        accumulatedWeight += weight;
        entries.Add(new Entry { item = item, accumulatedWeight = accumulatedWeight });
    }

    public T GetRandomAlways()
    {
        double r = rand.NextDouble() * accumulatedWeight;

        //double r = accumulatedWeight; // for testing
        // Loop through all the objects in the list
        foreach (Entry entry in entries)
        {
            // if the accumulated weight is more than or equal to R
            // it'll trigger
            // if not then r is too big and it will look for the next item
            if (entry.accumulatedWeight >= r)
            {
                return entry.item;
            }
        }
        return default(T); // Only if there are no entries
    }

    // To get random event
    public T GetRandom()
    {
        // Generate a random number between the sums of all the weight
        // Adds a chance based on 40% of the total accumulated weight to spawn no items
        // double r = rand.NextDouble() * (accumulatedWeight + (accumulatedWeight * 0.8)
        //double r = rand.NextDouble() * accumulatedWeight;
        //double r = rand.NextDouble() * (accumulatedWeight + (accumulatedWeight * 0.8));
       // EquipmentManager.Instance.rand.NextDouble();
        //double r = EquipmentManager.Instance.rand.NextDouble() * (accumulatedWeight + (accumulatedWeight * 0.8));
        double r = 50;
        //double r = accumulatedWeight; // for testing
        // Loop through all the objects in the list
        foreach (Entry entry in entries)
        {
            // if the accumulated weight is more than or equal to R
            // it'll trigger
            // if not then r is too big and it will look for the next item
            if (entry.accumulatedWeight >= r )
            {
                return entry.item;
            }
        }
        return default(T); // Only if there are no entries
    }

    // To wipe the list
    // Needed for game event as the random chances might change
    public void ClearList()
    {
        entries.Clear();
    }

}