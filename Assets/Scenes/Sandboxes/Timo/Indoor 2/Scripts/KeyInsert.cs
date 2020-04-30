using UnityEngine;

public class KeyInsert : MonoBehaviour, IInteractable
{

    public GameObject animatedKey;
    public Animator animatedKeyAnimator;
    public Animator openChestAnimator;
    public float waitAfterInsertion = 1.3f;
    public string keyRotationAnimationTrigger = "Unlock";
    public string chestOpenAnimationTrigger = "Open";

    private Timer openTimer;

    void Start()
    {
        animatedKey.SetActive(false);
        openTimer = new Timer();
    }

    void Update()
    {
        if (openTimer.GetStarted())
        {
            if (openTimer.Get() > waitAfterInsertion)
            {
                // If key was inserted and it is longer than waitAfterInsertion, the Open animation will be triggered and this Object will be destroyed (Cleaning!!!)
                openChestAnimator.SetTrigger(chestOpenAnimationTrigger);
                Destroy(gameObject);
            }
        }
    }
    
    public void OnInteraction()
    {
    // -> Key inserted
    // Make animated Key visible
    animatedKey.SetActive(true);

    // Start Unlock animation and save start Time
    animatedKeyAnimator.SetTrigger(keyRotationAnimationTrigger);
    openTimer.Start();
    }
}