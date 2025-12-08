using System;
using System.Collections.Generic;
using UnityEngine;

public class DeathDropItem : Death
{

    public List<DropTableElement> drops;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropRandomItem()
    {
        // Choose which item to drop
        GameObject objectToDrop;

        // Quit early if there are no items in the drop table!
        if (drops == null || drops.Count <= 0)
        {
            Debug.LogWarning("ERROR: Attempt to drop when there is no drops in table!");
            return;
        }

        // Make another array - this one holds the "cutoff" (or high value) for choosing that item
        float[] CDA = new float[drops.Count];

        // First item is our first drop weight
        CDA[0] = drops[0].weight;

        // Fill the remainder of the CDA array
        for (int i = 1; i < drops.Count; i++)
        {
            CDA[i] = CDA[i-1] + drops[i].weight;
        }

        // Choose a random number between 0 and our total CD
        int randomWeight = Random.Range(0, (int) CDA[CDA.Length-1]);

        /****
         * LONG, BAD, PROCESSOR HEAVY WAY
        
        for (int i = 0; i < CDA.Length; i++)
        {
            if (randomWeight < CDA[i])
            {
                // Drop the parallel item!
                objectToDrop = drops[i].objectToDrop;
                DropItem(objectToDrop, transform.position, transform.rotation);

                // Exit early!
                return;
            }
        }
        *****/

        /********* Better way -  Use Binary Search */
        int value = Array.BinarySearch(CDA, randomWeight);
        // If our random is EXACTLY one of our CDA values, drop that item
        if ( value >= 0)
        {
            objectToDrop = drops[value].objectToDrop;
            DropItem(objectToDrop, transform.position, transform.rotation);
        } else
        {
            // IF NOT, we need to find the next higher cutoff to find our index
            int invertedValue = ~value;

            // Drop based on inverted value!
            objectToDrop = drops[invertedValue].objectToDrop;
            DropItem(objectToDrop, transform.position, transform.rotation);

        }

        // We went through every weight and didn't find an object? Something went horribly wrong!
        Debug.LogError("ERROR: Tried to drop but couldn't find an item.");
    }

    public void DropItem(GameObject objectToDrop, Vector3 dropLocation, Quaternion dropRotation)
    {
        // Instantiate the object
        Instantiate<GameObject>(objectToDrop, dropLocation, dropRotation);

        // TODO: Instantiate a particle effect
        // TODO: Play a sound for the drop
        // TODO: Use physics to throw the object away from the drop location or towards a target location
    }


    public override void Die()
    {
        DropRandomItem();
    }
}
