using UnityEngine;

public class SetAnimatorTriggerByInteraction : BaseInteraction
{
    public Animator animator;
    public string triggerName;

    public override void OnInteraction(PlayerContext context)
    {
        if (triggerName == "")
        {
            Debug.LogError("Animation should be triggered, but no triggerName given!", this);
            return;
        }

        if (animator == null)
        {
            Debug.LogError("Animator should be triggered, but no Animator given!", this);
            return;
        }

        animator.SetTrigger(triggerName);
    }
}
