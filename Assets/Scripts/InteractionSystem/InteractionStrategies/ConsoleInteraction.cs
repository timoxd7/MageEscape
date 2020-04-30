using UnityEngine;

public class ConsoleInteraction : MonoBehaviour, IInteractable
{
    public void OnInteraction()
    {
        Debug.Log("Interaction: " + gameObject.name);
    }
}