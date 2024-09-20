using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackpackUI : MonoBehaviour
{
    private GameObject parrentUI;

    public List<SlotUI> slotUIList;

    private void Awake()
    {
        parrentUI = transform.Find("ParrentUI").gameObject;
    }

    private void Start()
    {
        InitUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUI();
        }
    }

    private void InitUI()
    {
        slotUIList = new List<SlotUI>(new SlotUI[24]);
        SlotUI[] slotUIArray = transform.GetComponentsInChildren<SlotUI>();

        foreach (SlotUI slotUI in slotUIArray)
        {
            slotUIList[slotUI.index] = slotUI;
        }

        UpdateUI();
        ToggleUI();
    }

    private void UpdateUI()
    {
        List<SlotData> slotDataList = InventoryManager.Instance.backpack.slotList;

        for (int i = 0; i < slotDataList.Count; i++)
        {
            slotUIList[i].SetSlotData(slotDataList[i]);
        }
    }

    private void ToggleUI()
    {
        parrentUI.SetActive(!parrentUI.activeSelf);
    }

    public void OnCloseClick()
    {
        ToggleUI();
    }
}
