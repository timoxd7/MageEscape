using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HintTriggerZone : MonoBehaviour
{
    public TimedHint hint;
    public Type hintTriggerType;
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            if (hint != null)
            {
                switch (hintTriggerType) {
                    case Type.Enter:
                        hint.RiddleStart();
                        break;

                    case Type.Exit:
                        hint.RiddleDone();
                        break;

                    default:
                        Debug.LogError("No triggerType given!", this);
                        break;
                }
            } else
            {
                Debug.LogError("No hint given!", this);
            }
        }
    }

    public enum Type
    {
        None,
        Enter,
        Exit
    }
}
