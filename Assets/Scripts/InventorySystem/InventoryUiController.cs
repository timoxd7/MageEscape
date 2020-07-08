using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUiController : MonoBehaviour, IObserver
{
    public InventoryController inventoryController;

    private InventoryData _inventoryData;

    private List<Transform> UiSlots => GetUiSlots();
    
    public void UpdateState()
    {
        foreach (Transform slot in UiSlots)
        {
            DisableSlot(slot);
        }
        
        for (int i = 0; i < _inventoryData.Items.Count(); i++)
        {
            Transform slot = UiSlots.ElementAt(i);
            Item item = _inventoryData.Items.ElementAt(i);

            if (item != null)
            {
                ChangeSpriteForSlot(slot, item.Sprite);
                EnableSlot(slot);
            }
        }
    }
    
    public void Start()
    {
        _inventoryData = inventoryController.playerContext.InventoryData;
        if (_inventoryData == null)
        {
            Debug.LogError("No inventoryData given");
        }
        if (inventoryController == null)
        {
            Debug.LogError("No Observable Subject given");
        }
        inventoryController.Register(this);
        UpdateState();
    }

    private void EnableSlot(Transform slot)
    {
        Image image = GetUiSlotItemImage(slot);
        image.enabled = true;
    }
    
    private  void DisableSlot(Transform slot)
    {
        Image image = GetUiSlotItemImage(slot);
        image.enabled = false;
    }
    
    private void ChangeSpriteForSlot(Transform slot, Sprite sprite)
    {
        Image image = GetUiSlotItemImage(slot);
        image.sprite = sprite;
    }

    private List<Transform> GetUiSlots()
    {
        List<Transform> slots = new List<Transform>();
        Transform panel = transform.Find("InventoryCanvas").Find("ItemPanel");
        foreach (Transform child in panel)
        {
            slots.Add(child);
        }

        return slots;
    }

    private Image GetUiSlotItemImage(Transform slot)
    {
        // Panel ... Sprite
        Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("No image found");
        }

        return image;
    }
}
