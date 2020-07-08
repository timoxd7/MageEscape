using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem.InteractionStrategies
{
    public class ListInventoryInteraction : BaseInteraction
    {
        public override void OnInteraction(PlayerContext context)
        {
            List<Item> items = context.InventoryData.Items;
            foreach (Item item in items)
            {
                Debug.Log(item.UniqeId + ":::" + item.Title);
            }
        }
    }
}