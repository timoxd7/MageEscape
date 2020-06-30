using MyBox;
using UnityEngine;

public class CollectibleItemInteraction : BaseInteraction
{
    public string uniqueId;
    public string title;
    public string description;
    public Sprite icon;
    public bool consumable = true;

    public bool autoDestroyThis = true;
    public bool destroyOtherObject = false;

    [ConditionalField(nameof(destroyOtherObject))]
    public GameObject otherObjectToDestroy;
    
    public override void OnInteraction(PlayerContext context)
    {
        Item item = new Item(uniqueId, title, description, icon, consumable);
        if (context.inventory.AddItem(item))
        {
            Debug.Log("Putting " + title + " into the inventory.");
            if (autoDestroyThis)
            {
                Destroy(gameObject);
            }

            if (destroyOtherObject)
            {
                if (otherObjectToDestroy != null)
                {                    
                    Destroy(otherObjectToDestroy);
                }
                else
                {
                    Debug.LogError("No otherObjectToDestroy given!", this);
                }
            }
        }
    }
}