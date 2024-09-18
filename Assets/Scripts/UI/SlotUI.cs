using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    public int index;
    private SlotData data;

    public Image iconImage;
    public TextMeshProUGUI countText;

    public void SetSlotData(SlotData data)
    {
        this.data = data;
        data.AddListener(OnDataChange);

        UpdateUI();
    }

    public SlotData GetSlotData()
    {
        return data;
    }

    private void OnDataChange()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (data.item == null)
        {
            iconImage.enabled = false;
            countText.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            countText.enabled = true;
            iconImage.sprite = data.item.sprite;
            countText.text = data.count.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemMoveHandler.instance.OnSlotClick(this);
    }
}
