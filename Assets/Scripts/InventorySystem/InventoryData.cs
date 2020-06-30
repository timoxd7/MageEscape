using System.Collections.Generic;

public class InventoryData
{
    private readonly List<Item> _items = new List<Item>();
    public List<Item> Items => _items;

    public void Add(Item item)
    {
        _items.Add(item);
    }

    public Item Remove(string itemId)
    {
        foreach (Item item in Items)
        {
            if (item.UniqeId == itemId)
            {
                Items.Remove(item);
                return item;
            }
        }

        return null;
    }

    public Item Contains(string itemId)
    {
        foreach (Item item in Items)
        {
            if (item.UniqeId == itemId)
            {
                return item;
            }
        }

        return null;
    }

    public int Count()
    {
        return _items.Count;
    }
}
