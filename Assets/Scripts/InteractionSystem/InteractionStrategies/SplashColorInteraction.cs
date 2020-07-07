using UnityEngine;
using MyBox;

public class SplashColorInteraction : BaseInteraction
{
    public Sprite blueSplash;
    public Sprite redSplash;
    public Sprite greenSplash;
    public Sprite emptyGlass;

    public SpriteRenderer spriteRenderer;
    public ColorRiddleManager colorRiddleManager;

    public int key = -1;
    private RiddleColor value = RiddleColor.None;

    public bool playSound = false;
    [ConditionalField(nameof(playSound))]
    public SoundSourcePlayer soundSourcePlayer;

    public void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer given");
        }

        if (colorRiddleManager == null)
        {
            Debug.LogError("No RiddleManager given :(  ... ");
        }
    }

    public override void OnInteraction(PlayerContext context)
    {
        string glassUniqueId = "glass_empty";
        Item emptyGlassId = new Item(glassUniqueId, glassUniqueId, glassUniqueId, emptyGlass);
        InventoryController inventory = context.inventory;

        if (null != context.inventory.ContainsItem("glass_blue"))
        {
            ChangeSprite(blueSplash);
            value = RiddleColor.Blue;
            if (playSound)
            {
                if (soundSourcePlayer != null)
                {
                    soundSourcePlayer.Play();
                }
            }
        }
        else if (null != context.inventory.ContainsItem("glass_red"))
        {
            ChangeSprite(redSplash);
            value = RiddleColor.Red;
            if (playSound)
            {
                if (soundSourcePlayer != null)
                {
                    soundSourcePlayer.Play();
                }
            }
        }
        else if (null != context.inventory.ContainsItem("glass_green"))
        {
            ChangeSprite(greenSplash);
            value = RiddleColor.Green;
             if (playSound)
        {
            if (soundSourcePlayer != null)
            {
                soundSourcePlayer.Play();
            }
        }
        }
        colorRiddleManager.UpdateState(key, value);
    }
    
    private void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
