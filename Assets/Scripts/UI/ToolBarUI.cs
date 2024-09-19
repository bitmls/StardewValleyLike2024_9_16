using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarUI : MonoBehaviour
{
    public List<ToolBarSlotUI> slotUIList;

    private ToolBarSlotUI selectedSlotUI;
    private int selectedIndex = -1;

    void Start()
    {
        InitUI();
    }

    private void Update()
    {
        ToolBarSelectControl();
    }

    public ToolBarSlotUI GetSelectedSLotUI()
    {
        return selectedSlotUI;
    }

    private void ToolBarSelectControl()
    {
        for (int i = (int)KeyCode.Alpha1; i <= (int)KeyCode.Alpha9; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                int index = i - (int)KeyCode.Alpha1;

                if (selectedSlotUI != null && selectedIndex != index)
                    selectedSlotUI.Highlight();

                if (selectedIndex == index)
                {
                    selectedSlotUI.SwitchHighLight();
                }
                else
                {
                    selectedSlotUI = slotUIList[index];
                    selectedIndex = index;
                    selectedSlotUI.UnHighlight();
                }
            }
        }
    }

    private void InitUI()
    {
        slotUIList = new List<ToolBarSlotUI>(new ToolBarSlotUI[9]);
        ToolBarSlotUI[] slotUIArray = transform.GetComponentsInChildren<ToolBarSlotUI>();

        foreach (ToolBarSlotUI slotUI in slotUIArray)
        {
            slotUIList[slotUI.index] = slotUI;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        List<SlotData> slotDataList = InventoryManager.Instance.toolBar.slotList;

        for (int i = 0; i < slotDataList.Count; i++)
        {
            slotUIList[i].SetSlotData(slotDataList[i]);
        }
    }
}
