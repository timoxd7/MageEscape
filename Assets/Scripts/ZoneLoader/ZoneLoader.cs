using MyBox;
using System.Collections.Generic;
using UnityEngine;

public class ZoneLoader : MonoBehaviour
{
    public bool loadInOnGameStart = false;
    public List<GameObject> zoneObjects;

    private bool currentLoadState = false;
    private bool currentLoadStateSet = false;

    private void Start()
    {
        if (loadInOnGameStart)
        {
            Load(true);
        } else
        {
            Load(false);
        }
    }

    public void ActivateZone()
    {
        Load(true);
    }

    public void DeactivateZone()
    {
        Load(false);
    }


    private void Load(bool activeState)
    {
        if (activeState == currentLoadState && currentLoadStateSet)
            return;

        if (zoneObjects.IsNullOrEmpty())
        {
            Debug.LogError("Zone has no objects attached! Destroying this Zone!");
            Destroy(this);
            return;
        }

        foreach (GameObject currentObject in zoneObjects)
        {
            if (currentObject != null)
            {
                currentObject.SetActive(activeState);
            }
        }

        currentLoadState = activeState;
        currentLoadStateSet = true;
    }
}
