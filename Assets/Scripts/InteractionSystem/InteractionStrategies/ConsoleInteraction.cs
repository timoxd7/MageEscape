using UnityEngine;

public class ConsoleInteraction : BaseInteraction
{
    public override void OnInteraction(PlayerContext context)
    {
        Debug.Log("Interaction: " + gameObject.name);
    }
}