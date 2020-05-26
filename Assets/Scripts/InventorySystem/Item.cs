using UnityEngine;

public class Item
{
    public string UniqeId { get; }
    public string Title { get; }
    public string Description { get; }
    public Sprite Sprite { get; }

    public Item(string uniqueId, string title, string description, Sprite sprite)
    {
        this.UniqeId = uniqueId;
        this.Title = title;
        this.Description = description;
        this.Sprite = sprite;
    }

    public Item(Item item)
    {
        this.UniqeId = item.UniqeId;
        this.Title = item.Title;
        this.Description = item.Description;
        this.Sprite = item.Sprite;
    }
}
