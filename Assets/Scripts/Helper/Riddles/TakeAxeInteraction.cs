using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAxeInteraction : BaseInteraction
{
    public string uniqueId;
    public string title;
    public string description;
    public Sprite icon;
    public DialogMessage question;

    private PlayerContext currentContext;

    public override void OnInteraction(PlayerContext context)
    {
        currentContext = context;
        question.Show();
    }

    public void AxeTaken()
    {
        Debug.Log("Putting " + title + " into the inventory.");

        Item item = new Item(uniqueId, title, description, icon);
        currentContext.InventoryData.Add(item);

        Destroy(gameObject);
    }
}
