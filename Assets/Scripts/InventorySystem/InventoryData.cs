using System.Collections.Generic;

public class InventoryData
{
    private readonly Dictionary<string,Item> _items = new Dictionary<string, Item>();
    public Dictionary<string, Item> Items => _items;

    public void Add(Item item)
    {
        _items.Add(item.UniqeId, item);
    }

    public Item Remove(string itemId)
    {
        if (_items.TryGetValue(itemId, out Item value))
        {
            _items.Remove(itemId);
            return value;
        }
        else
        {
            return null;
        }
    }

    public bool Contains(string itemId)
    {
        return _items.TryGetValue(itemId, out Item item);
    }
}
