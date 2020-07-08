using UnityEngine;

public class Item
{
    public string UniqeId { get; }
    public string Title { get; }
    public string Description { get; }
    public Sprite Sprite { get; }
    public bool Consumable { get; }

    public Item(string uniqueId, string title, string description, Sprite sprite, bool consumable = false)
    {
        this.UniqeId = uniqueId;
        this.Title = title;
        this.Description = description;
        this.Sprite = sprite;
        this.Consumable = consumable;
    }

    public Item(Item item)
    {
        this.UniqeId = item.UniqeId;
        this.Title = item.Title;
        this.Description = item.Description;
        this.Sprite = item.Sprite;
        this.Consumable = item.Consumable;
    }
}
