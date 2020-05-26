using UnityEngine;

public class CollectibleItemInteraction : BaseInteraction
{
    public string uniqueId;
    public string title;
    public string description;
    public Sprite icon;
    
    public override void OnInteraction(PlayerContext context)
    {
        Debug.Log("Putting " + this.title + " into the inventory.");
        
        Item item = new Item(uniqueId, title, description, icon);
        context.InventoryData.Add(item);
        
        Destroy(this.gameObject);
    }
}
