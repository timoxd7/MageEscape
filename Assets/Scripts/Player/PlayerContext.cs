/**
 * Spielerkontext gebündelt handeln
 */
public class PlayerContext
{
    public InventoryData InventoryData { get; } = new InventoryData();
    public InteractionData InteractionData { get; } = new InteractionData();
}