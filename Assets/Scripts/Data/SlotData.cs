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
        return count == 0 || item == null;
    }

    public void AddOne()
    {
        count++;
        TriggerChange();
    }

    public void ReduceOne()
    {
        count--;
        if (IsEmpty())
            Clear();
        TriggerChange();
    }

    public void AddNewItem(ItemData item)
    {
        this.item = item;
        count++;
        TriggerChange();
    }

    public void Clear()
    {
        item = null;
        count = 0;
        TriggerChange();
    }

    public void CopySlot(SlotData slotData)
    {
        this.item = slotData.item;
        this.count = slotData.count;
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
