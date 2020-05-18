using UnityEngine;

public class Item
{
    private static int _idCounter = 0;

    public int Id { get; }
    public string Title { get; }
    public string Description { get; }
    public Sprite Sprite { get; }

    public Item(string title, string description, Sprite sprite)
    {
        this.Id = _idCounter++;
        this.Title = title;
        this.Description = description;
        this.Sprite = sprite;
    }

    public Item(Item item)
    {
        this.Id = _idCounter++;
        this.Title = item.Title;
        this.Description = item.Description;
        this.Sprite = item.Sprite;
    }
}
