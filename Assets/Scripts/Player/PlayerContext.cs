using UnityEngine;

/**
 * Spielerkontext gebündelt handeln
 */
public class PlayerContext : MonoBehaviour
{
    public InventoryData InventoryData { get; } = new InventoryData();
    public InteractionData InteractionData { get; } = new InteractionData();
}