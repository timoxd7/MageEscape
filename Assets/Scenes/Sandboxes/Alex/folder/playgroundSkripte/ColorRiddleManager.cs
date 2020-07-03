using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ColorRiddleManager : MonoBehaviour
{
    private List<RiddleColor> values;
    private const string PassPhrase = "rbbgbrgr";
    
    public UnityEvent unityEvent;

    public void Start()
    {
        values = new List<RiddleColor>();
        for (int i = 0; i < 8; i++)
        {
            values.Add(RiddleColor.None);
        }
    }

    public void UpdateState(int order, RiddleColor value)
    {
        values[order] = value;
        CheckCode();
    }

    private void CheckCode()
    {
        string solution = "";
        foreach (RiddleColor riddleColor in values)
        {
            string color = ResolveEnum(riddleColor);
            solution += color;
        }
        Debug.Log(solution);
        if (solution == PassPhrase)
        {
            Debug.Log("Yippie!");
            unityEvent.Invoke();
        }
    }
    
    private string ResolveEnum(RiddleColor value)
    {
        switch (value)
        {
            case RiddleColor.Blue:
                return "b";
            case RiddleColor.Red:
                return "r";
            case RiddleColor.Green:
                return "g"; 
            default:
                return "X";
        }
    }
}
