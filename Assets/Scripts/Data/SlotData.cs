using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotData
{
    public ItemData item;
    public int count;

    public bool IsFull()
    {
        return count >= item.maxCount;
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public void AddOne()
    {
        count++;
    }

    public void AddNewItem(ItemData item)
    {
        this.item = item;
        count++;
    }
}
