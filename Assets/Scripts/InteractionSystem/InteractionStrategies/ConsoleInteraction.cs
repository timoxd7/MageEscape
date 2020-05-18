using UnityEngine;

public class ConsoleInteraction : BaseInteraction
{
    public override void OnInteraction(InteractionContext context)
    {
        Debug.Log("Interaction: " + gameObject.name);
    }
}