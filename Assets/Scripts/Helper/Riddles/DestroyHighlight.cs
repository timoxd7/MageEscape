using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHighlight : MonoBehaviour
{
    public void DestroyHighlightScript()
    {
        HighlightDetection highlightDetection = GetComponent<HighlightDetection>();

        if (highlightDetection != null)
            Destroy(highlightDetection);
    }
}
