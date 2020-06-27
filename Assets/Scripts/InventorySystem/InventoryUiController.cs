using UnityEngine;

public class InventoryUiController : MonoBehaviour, IObserver
{
    public InventoryController inventoryController;

    private InventoryData _inventoryData;
    private Transform _panel;

    #region Observer
    
    public void UpdateState()
    {
        /*todo*/
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
    }
    
    #endregion

    #region Custom

    #endregion
    
    
}
