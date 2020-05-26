using UnityEngine;

public class InteractionStartAnimationByTrigger : BaseInteraction
{
    public GameObject player;
    public RuntimeAnimatorController animatorController;
    public string triggerName;

    public override void OnInteraction(PlayerContext context)
    {
        if (triggerName == "")
        {
            Debug.LogError("Animation should be triggered, but no triggerName given!", this);
            return;
        }

        // May clean up before
        Animator animator = player.GetComponent<Animator>();
        if (animator != null)
            Destroy(animator);
            

        animator = player.AddComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;
        animator.SetTrigger(triggerName);
    }
}
