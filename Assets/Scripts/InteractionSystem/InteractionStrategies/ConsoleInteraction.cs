using UnityEngine;

public class ConsoleInteraction : BaseInteraction
{
    public override void OnInteraction()
    {
        Debug.Log("Interaction: " + gameObject.name);
    }
}