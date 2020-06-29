using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUiController : MonoBehaviour, IObserver
{
    public InventoryController inventoryController;

    private InventoryData _inventoryData;
    private Transform _panel;

    private List<Transform> UiSlots => GetSlots();

    #region Observer
    
    public void UpdateState()
    {
        for (int i = 0; i < InventoryController.INVENTORY_MAX_ITEMS; i++)
        {
            Item item = _inventoryData.Items.ElementAt(i).Value;
            if (item != null)
            {
                ChangeItemSpriteForSlot(i, _inventoryData.Items.ElementAt(i).Value);
                EnableSlot(i);
            }
            else
            {
                DisableSlot(i);
            }
        }
    }
    
    #endregion

    #region Builtin
    
    public void Start()
    {
        _panel = transform.Find("InventoryCanvas").Find("ItemPanel");
        if (_panel == null)
        {
            Debug.LogError("No panel given");
        }
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

        GetActiveSlots();
        
    }
    
    #endregion

    #region Custom

    private int CompareStateCount()
    {
        List<Transform> activeSlots = GetActiveSlots();
        
        if (activeSlots.Count > _inventoryData.Count())
        {
            return 1;
        }
        else if (activeSlots.Count < _inventoryData.Count())
        {
            return -1;
        }
        else 
        {
            return 0;
        }
    }
    
    private void EnableSlot(int i)
    {
        Transform slot = UiSlots[i].GetChild(0).GetChild(0);
        Image image = slot.GetComponent<Image>();
        image.enabled = true;
    }
    
    private  void DisableSlot(int i)
    {
        Transform slot = UiSlots[i].GetChild(0).GetChild(0);
        Image image = slot.GetComponent<Image>();
        image.enabled = false;
    }
    
    private void ChangeItemSpriteForSlot(int index, Item item)
    {
        Transform slot = UiSlots[index].GetChild(0).GetChild(0);
        Image image = slot.GetComponent<Image>();
        image.sprite = item.Sprite;
    }
    
    
    
    private List<Transform> GetActiveSlots()
    {
        List<Transform> activeSlots = new List<Transform>();
        foreach (Transform slot in UiSlots)
        {
            Transform sprite = slot.GetChild(0).GetChild(0);
            if (sprite.GetComponent<Image>().isActiveAndEnabled)
            {
                activeSlots.Add(slot);
            }
        }
        return activeSlots;
    }
    
    private List<Transform> GetSlots()
    {
        List<Transform> slots = new List<Transform>();
        foreach (Transform child in _panel)
        {
            slots.Add(child);
        }

        return slots;
    }

    #endregion
    
    
}
