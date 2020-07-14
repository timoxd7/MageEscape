using MyBox;
using System.Collections.Generic;
using UnityEngine;

public class ZoneLoader : MonoBehaviour
{
    public bool loadInOnGameStart = false;
    public List<GameObject> zoneObjects;

    private bool currentLoadState = false;
    private bool currentLoadStateSet = false;
    private bool lastSetLoadState;
    private int keepActivatedCount = 0;

    private void Start()
    {
        lastSetLoadState = loadInOnGameStart;

        if (loadInOnGameStart)
        {
            Load(true);
        } else
        {
            Load(false);
        }
    }
    [ButtonMethod]
    public void ActivateZone()
    {
        lastSetLoadState = true;
        Load(true);
    }

    [ButtonMethod]
    public void DeactivateZone()
    {
        lastSetLoadState = false;
        
        if (keepActivatedCount == 0)
            Load(false);
    }


    public void SetKeepActivated()
    {
        keepActivatedCount++;
    }

    public void ReleaseKeepActivated()
    {
        if (keepActivatedCount > 0)
            keepActivatedCount--;

        if (keepActivatedCount == 0)
            Load(lastSetLoadState);
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
