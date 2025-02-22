﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimator : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator);
    }
}
