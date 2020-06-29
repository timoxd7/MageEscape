using MyBox;
using UnityEngine;

public class CollectibleItemInteraction : BaseInteraction
{
    public string uniqueId;
    public string title;
    public string description;
    public Sprite icon;

    public bool autoDestroyThis = true;
    public bool destroyOtherObject = false;

    [ConditionalField(nameof(destroyOtherObject))]
    public GameObject otherObjectToDestroy;
    
    public override void OnInteraction(PlayerContext context)
    {
        Debug.Log("Putting " + title + " into the inventory.");
        
        Item item = new Item(uniqueId, title, description, icon);
        context.InventoryData.Add(item);
        
        if (autoDestroyThis)
            Destroy(gameObject);

        if (destroyOtherObject)
            if (otherObjectToDestroy != null)
                Destroy(otherObjectToDestroy);
            else
                Debug.LogError("No otherObjectToDestroy given!", this);
    }
}
