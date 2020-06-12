using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogButton : MonoBehaviour
{
    public DialogMessage parentMessage;
    public DialogOption dialogOption;

    public void OnClick()
    {
        dialogOption.Execute(parentMessage);
    }
}
