using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMoveHandler : MonoBehaviour
{
    public static ItemMoveHandler instance { get; private set; }

    private Image icon;
    private SlotData selectedSlotData;

    private Player player;

    private bool isCtrlDown = false;

    private void Awake()
    {
        instance = this;
        icon = GetComponentInChildren<Image>();
        HideIcon();
        player = GameObject.FindAnyObjectByType<Player>();
    }

    private void Update()
    {
        if (icon.enabled)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(), Input.mousePosition,
                null,
                out position);
            icon.GetComponent<RectTransform>().anchoredPosition = position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                TrowItem();
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            ClearHandForce();
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCtrlDown = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCtrlDown = false;
        }
    }

    public void OnSlotClick(SlotUI slotUI)
    {
        // 1.There are items on hand
        if (selectedSlotData != null)
        {
            // 1.1.selected slot is empty
            if (slotUI.GetSlotData().IsEmpty())
            {
                MoveItemToEmptySlot(selectedSlotData, slotUI.GetSlotData());
            }
            // 1.2.selected slot is not empty
            else
            {
                // 1.2.1.it selects its own slot
                if (selectedSlotData == slotUI.GetSlotData())
                    return;
                // 1.2.2.the items in selected slot and the items in hand are the same
                if(selectedSlotData.item.type == slotUI.GetSlotData().item.type)
                {
                    MoveItemToNotEmptySlot(selectedSlotData, slotUI.GetSlotData());
                }
                // 1.2.3.the items in selected slot and the items in hand are not the same
                else
                {
                    SwapSlot(selectedSlotData, slotUI.GetSlotData());
                }
            }
        }
        // 2.There are no items on hand
        else
        {
            // 2.1.selected slot is empty
            if (slotUI.GetSlotData().IsEmpty())
                return;
            // 2.2.selected slot is not empty
            selectedSlotData = slotUI.GetSlotData();
            ShowIcon(selectedSlotData.item.sprite);
        }
    }

    public void ShowIcon(Sprite sprite)
    {
        icon.sprite = sprite;
        icon.enabled = true;
    }

    public void HideIcon()
    {
        icon.enabled = false;
    }

    private void ClearHand()
    {
        if (selectedSlotData.IsEmpty())
        {
            HideIcon();
            selectedSlotData = null;
        }
    }

    private void ClearHandForce()
    {
        HideIcon();
        selectedSlotData = null;
    }

    private void TrowItem()
    {
        if (selectedSlotData == null)
            return;

        GameObject prefab = selectedSlotData.item.prefab;
        int count = selectedSlotData.count;

        if (isCtrlDown)
        {
            player.ThrowItem(prefab, 1);
            selectedSlotData.ReduceNum();
        }
        else
        {
            player.ThrowItem(prefab, count);
            selectedSlotData.Clear();
        }
        ClearHand();
    }

    private void MoveItemToEmptySlot(SlotData srcSlotData, SlotData dstSlotdata)
    {
        if (isCtrlDown)
        {
            dstSlotdata.AddNewItem(srcSlotData.item);
            srcSlotData.ReduceNum();
        }
        else
        {
            dstSlotdata.CopySlot(srcSlotData);
            srcSlotData.Clear();
        }
        ClearHand();
    }

    private void MoveItemToNotEmptySlot(SlotData srcSlotData, SlotData dstSlotData)
    {
        if(isCtrlDown)
        {
            if (dstSlotData.IsFull())
                return;
            dstSlotData.AddNum();
            srcSlotData.ReduceNum();
        }
        else
        {
            int emptyCount = dstSlotData.item.maxCount - dstSlotData.count;
            int haveCount = srcSlotData.count;
            if (emptyCount > haveCount)
            {
                dstSlotData.AddNum(haveCount);
                srcSlotData.ReduceNum(haveCount);
            }
            else
            {
                dstSlotData.AddNum(emptyCount);
                srcSlotData.ReduceNum(emptyCount);
            }
        }
        ClearHand();
    }

    private void SwapSlot(SlotData srcSlotData, SlotData dstSlotData)
    {
        ItemData itemData = dstSlotData.item;
        int count = dstSlotData.count;

        dstSlotData.CopySlot(srcSlotData);
        srcSlotData.AddNewItem(itemData, count);

        ClearHandForce();
    }
}
