using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotChange : MonoBehaviour
{
    public GameObject emptyPot;
    public GameObject fullPot;

    public void PodFilled()
    {
        if (emptyPot == null)
        {
            Debug.LogError("No emptyPot given!", this);
            return;
        }

        if (emptyPot.gameObject.activeSelf)
        {
            if (fullPot == null)
            {
                Debug.LogError("No fullPot given!", this);
            }

            emptyPot.SetActive(false);
            fullPot.SetActive(true);
        }
    }
}
