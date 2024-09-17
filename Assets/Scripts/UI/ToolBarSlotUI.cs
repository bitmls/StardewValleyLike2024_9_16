using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarSlotUI : SlotUI
{
    public Sprite slotLight;
    public Sprite slotDark;

    private bool isLight = true;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Highlight()
    {
        image.sprite = slotLight;
        isLight = true;
        Debug.Log("light");
    }

    public void UnHighlight()
    {
        image.sprite = slotDark;
        isLight = false;
        Debug.Log("dark");
    }

    public void SwitchHighLight()
    {
        if (isLight)
            UnHighlight();
        else
            Highlight();
    }
}
