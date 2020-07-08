using MyBox;
using System;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class HighlightDetection : BaseDetection
{

    [Header("Outline Settings")]
    [Tooltip("The Highlight Type set in the OutlineEffect")]
    public int outlineType = 0;
    public bool eraseRenderer = false;

    [Header("Outlined Object(s)")]
    [Tooltip("Specify which object should be outlined. If no objects are given, it will try to outline the Mesh sitting on this object (if enabled)")]
    public List<MeshRenderer> outlinedObjects;
    [Tooltip("Dont't Add this object automatically if list is empty")]
    public bool doNotAutoAddThisIfEmpty = false;
    [Tooltip("Automatically add this object to be outlined, even if other objects are already in this list")]
    public bool autoAddThis = false;

    [Header("Other")]
    public bool destroyOutlineInstancesOnDestruction = true;


    private List<Outline> outlineInstances;
    private bool lastKnownHighlightVisible = false;


    private void Start()
    {
        outlineInstances = new List<Outline>();

        if (outlinedObjects == null)
            outlinedObjects = new List<MeshRenderer>();

        if ((outlinedObjects.IsNullOrEmpty() && !doNotAutoAddThisIfEmpty) || autoAddThis)
        {
            MeshRenderer thisMeshRenderer = gameObject.GetComponent<MeshRenderer>();

            if (thisMeshRenderer != null)
            {
                outlinedObjects.Add(thisMeshRenderer);
            }
        }

        foreach (MeshRenderer outlineObject in outlinedObjects)
        {
            Outline outlineInstance = outlineObject.GetOrAddComponent<Outline>();
            outlineInstances.Add(outlineInstance);
            outlineInstance.enabled = false;
        }
    }

    public override void OnDetectionEnter()
    {
        if (!enabled)
            return;

        if (!outlineInstances.IsNullOrEmpty())
        {
            foreach (Outline outlineInstance in outlineInstances)
            {
                if (outlineInstance != null)
                {
                    outlineInstance.enabled = true;
                    // !!!
                }
                else
                {
                    Debug.LogError("Can't create or locate outline Instance", gameObject);
                }
            }
        }

        lastKnownHighlightVisible = true;

    }

    public override void OnDetectionExit()
    {
        if (!enabled)
            return;

        if (!outlineInstances.IsNullOrEmpty())
        {
            foreach (Outline outlineInstance in outlineInstances)
            {
                if (outlineInstance != null)
                    outlineInstance.enabled = false;
            }
        }

        lastKnownHighlightVisible = false;
    }

    private void OnDestroy()
    {
        if (lastKnownHighlightVisible)
        {
            OnDetectionExit();
        }

        if (destroyOutlineInstancesOnDestruction)
        {
            if (outlineInstances.IsNullOrEmpty())
                return;

            foreach (Outline outlineInstance in outlineInstances)
            {
                if (outlineInstance != null)
                    Destroy(outlineInstance);
            }
        }
    }

    private void OnDisable()
    {
        if (lastKnownHighlightVisible)
        {
            OnDetectionExit();
            lastKnownHighlightVisible = true;
        }
    }

    private void OnEnable()
    {
        if (lastKnownHighlightVisible)
        {
            OnDetectionEnter();
        }
    }
}
