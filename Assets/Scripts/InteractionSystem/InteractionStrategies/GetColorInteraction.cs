using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.Events;

public class GetColorInteraction : BaseInteraction
{
    public RiddleColor color;
    public Sprite sprite;

    public bool playSound = false;
    [ConditionalField(nameof(playSound))]
    public SoundSourcePlayer soundSourcePlayer;

    public UnityEvent ifNoGlassGiven;

    public override void OnInteraction(PlayerContext context)
    {
        if (playSound)
        {
            if (soundSourcePlayer != null)
            {
                soundSourcePlayer.Play();
            }
        }

        Item glass = GetGlassFromInventory(context);
        if (glass != null)
        {

            string uniqueId;
            string title;
            string description = "Jetzt ist in dem Glass etwas drin...";
            switch (color)
            {
                case RiddleColor.Red:
                    uniqueId = "glass_red";
                    title = "Eine rote Flüssigkeit";
                    break;
                case RiddleColor.Blue:
                    uniqueId = "glass_blue";
                    title = "Eine blaue Substanz";
                    break;
                case RiddleColor.Green:
                    uniqueId = "glass_green";
                    title = "Ein grünes Etwas";
                    break;
                default:
                    uniqueId = "glass_empty";
                    title = "Glass";
                    break;
            }
            Item newGlass = new Item(uniqueId, title, description, sprite);
            context.inventory.RemoveItem(glass.UniqeId);
            context.inventory.AddItem(newGlass);
        }
        else
        {
            if (null != ifNoGlassGiven)
            {
                ifNoGlassGiven.Invoke();
            }
            else
            {
                Debug.LogError("No Alternate Event given", gameObject);
            } 
        }
    }

    private Item GetGlassFromInventory(PlayerContext context)
    {
        List<Item> items = context.inventory.GetAllItems();
        foreach (Item item in items)
        {
            if (item.UniqeId == "glass_green" || item.UniqeId == "glass_red" || item.UniqeId == "glass_blue" || item.UniqeId == "glass_empty")
            {
                return item;
            }
        }

        return null;
    }
}

public enum RiddleColor
{
    Red,
    Blue,
    Green,
    None
}
