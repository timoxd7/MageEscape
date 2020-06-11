using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem.InteractionStrategies
{
    public class ListInventoryInteraction : BaseInteraction
    {
        public override void OnInteraction(PlayerContext context)
        {
            Dictionary<string, Item> items = context.InventoryData.Items;
            foreach (KeyValuePair<string, Item> item in items)
            {
                Debug.Log(item.Value.UniqeId + ":::" + item.Value.Title);
            }
        }
    }
}