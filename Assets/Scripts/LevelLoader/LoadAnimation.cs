using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAnimation : MonoBehaviour
{
    public RectTransform rotatingCircler;
    public int circleParts = 16;
    public float circlePartTime = 0.03125f;
    public bool invertDirection;

    private float originRotation;
    private int currentRotationState = 0;
    private float unusedTime = 0;

    private void Start()
    {
        originRotation = rotatingCircler.rotation.eulerAngles.z;
    }

    void Update()
    {
        unusedTime += Time.deltaTime;

        while (unusedTime >= circlePartTime)
        {
            unusedTime -= circlePartTime;
            currentRotationState++;

            if (currentRotationState >= circleParts)
                currentRotationState -= circleParts;

            int usedCircleParts = 0;
            if (invertDirection)
                usedCircleParts -= circleParts;
            else
                usedCircleParts = circleParts;

            float newRotation = originRotation + ((360f / (float)usedCircleParts) * (float)currentRotationState);
            rotatingCircler.rotation = Quaternion.Euler(rotatingCircler.rotation.eulerAngles.x, rotatingCircler.rotation.eulerAngles.y, newRotation);
        }
    }
}
