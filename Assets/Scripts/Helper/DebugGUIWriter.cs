using System.Collections.Generic;
using UnityEngine;

public class DebugGUIWriter : MonoBehaviour
{
    public float lineSpacing = 0;
    public float lineOffset = 10;
    public Vector2 initPosition = new Vector2(10, 10);
    public Vector2 textBoxSize = new Vector2(700, 20);

    private List<string> debugLines;
    private bool clearNeeded = false;

    void Awake()
    {
        debugLines = new List<string>();
    }

    void OnGUI()
    {
        float currentLineOffset = 0;

        // Print each Line
        foreach(string line in debugLines)
        {
            if (line != null && line != "")
            {
                GUI.Label(new Rect(initPosition.x, initPosition.y + currentLineOffset, textBoxSize.x, textBoxSize.y), line);
                currentLineOffset += lineSpacing + lineOffset;

                Debug.Log("Writing " + line);
            }
        }

        // Empty buffer
        clearNeeded = true;
    }

    // Should only be called from inside a Update() method (anythinb BEFORE OnGUI()!)
    public void AddLine(string line)
    {
        if (clearNeeded)
        {
            clearNeeded = false;
            debugLines.Clear();
        }

        debugLines.Add(line);
    }
}
