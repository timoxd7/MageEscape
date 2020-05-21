using MyBox;
using UnityEngine;

public class ResetAnimationTrigger : StateMachineBehaviour
{
    public string triggerName;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (triggerName != "")
            animator.ResetTrigger(triggerName);
    }
}
