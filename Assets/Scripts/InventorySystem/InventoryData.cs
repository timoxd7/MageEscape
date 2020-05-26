using System.Collections.Generic;

public class InventoryData
{
    private readonly List<Item> _items = new List<Item>();
    public List<Item> Items => _items;

    public void Add(Item item)
    {
        _items.Add(item);
    }

    public Item Remove(Item item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);
            return item;
        }
        else
        {
            return null;
        }
    }

    public bool Contains(Item item)
    {
        return _items.Contains(item);
    }
}
