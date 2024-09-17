using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotData
{
    public ItemData item;
    public int count;

    private Action OnChange;

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
        TriggerChange();
    }

    public void AddNewItem(ItemData item)
    {
        this.item = item;
        count++;
        TriggerChange();
    }

    private void TriggerChange()
    {
        OnChange?.Invoke();
    }

    public void AddListener(Action OnChange)
    {
        this.OnChange = OnChange;
    }


}
