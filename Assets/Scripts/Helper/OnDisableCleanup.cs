using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableCleanup : MonoBehaviour
{
    [Tooltip("Destructs all following Objects on a disable-event")]
    public List<GameObject> objectsToDestruct;

    private void OnDisable()
    {
        if (objectsToDestruct.IsNullOrEmpty())
        {
            Debug.Log("List to Destruct is empty!", this);
            return;
        }

        foreach(GameObject currentObject in objectsToDestruct)
        {
            if (currentObject != null)
            {
                Destroy(currentObject);
            }
        }
    }
}
