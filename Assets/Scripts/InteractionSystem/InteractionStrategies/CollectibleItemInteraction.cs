using UnityEngine;

public class CollectibleItemInteraction : BaseInteraction
{
    public string title;
    public string description;
    public Sprite icon;
    
    public override void OnInteraction()
    {
        Debug.Log("Putting " + this.title + " into the inventory.");
        
        Item item = new Item(title, description, icon);
        // GetInventory.Put(item); oder so ähnlich
        
        Destroy(this.gameObject);
    }
}
