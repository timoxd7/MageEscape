using UnityEngine;

/**
 * Spielerkontext gebündelt handeln
 */
public class PlayerContext : MonoBehaviour
{
    public InventoryData InventoryData { get; } = new InventoryData();
    public InteractionData InteractionData { get; } = new InteractionData();

    public InteractionController interaction;
    public InventoryController inventory;

    private void Start()
    {
        interaction = gameObject.GetComponent<InteractionController>();
        inventory = gameObject.GetComponent<InventoryController>();
        if (interaction == null)
        {
            Debug.LogError("No InteractionController given");
        }
        if (inventory == null)
        {
            Debug.LogError("No InventoryController given");
        }
    }
}