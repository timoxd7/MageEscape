using UnityEngine;

public class KeyInsert : BaseInteraction
{

    public GameObject animatedKey;
    public SoundSourcePlayer keyRotateSound;
    public SoundSourcePlayer chestOpenSound;
    public Animator animatedKeyAnimator;
    public Animator openChestAnimator;
    public Interactable interactable;
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

                if (chestOpenSound != null)
                    chestOpenSound.Play();

                Destroy(gameObject);
            }
        }
    }
    
    public override void OnInteraction(PlayerContext context)
    {
        // -> Key inserted
        // Make animated Key visible
        animatedKey.SetActive(true);

        if (keyRotateSound != null)
            keyRotateSound.Play();

        // Start Unlock animation and save start Time
        animatedKeyAnimator.SetTrigger(keyRotationAnimationTrigger);
        openTimer.Start();

        // Disable Interactable
        if (interactable != null)
            interactable.enabled = false;
    }
}