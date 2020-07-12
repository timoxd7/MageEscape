using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTriggerTimes : MonoBehaviour
{
    public TimedHint hint;
    public int triggerExecutionTimes = 1;

    private int triggeredTimes = 0;
    private bool consumed = false;

    public void Trigger()
    {
        triggeredTimes++;

        if (consumed)
            return;

        if (triggeredTimes >= triggerExecutionTimes)
        {
            if (hint != null)
            {
                hint.RiddleDone();
            }

            consumed = true;
        }
    }
}
