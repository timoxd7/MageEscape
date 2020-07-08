using MyBox;
using UnityEngine;

public class SimpleEmptyObjectDialog : BaseInteraction
{
    [Header("Empty Object Dialog")]
    public bool autoAssignDialog = true;
    [ConditionalField(nameof(autoAssignDialog))]
    public string emptyObjectDialogTag = "EmptyObjectDialog";
    [ConditionalField(nameof(autoAssignDialog), true)]
    public GameObject emptyObjectDialog;

    [Header("Text")]
    public bool useStandardText = true;
    [ConditionalField(nameof(useStandardText), true)]
    public string messageText = "Das werde ich wohl nicht benötigen";

    [Header("Sound")]
    public bool playSound = false;
    [ConditionalField(nameof(playSound))]
    public SoundSourcePlayer soundSourcePlayer;

    private DialogMessage dialogMessage;


    void Start()
    {
        if (autoAssignDialog)
            emptyObjectDialog = GameObject.FindGameObjectWithTag(emptyObjectDialogTag);

        if (emptyObjectDialog == null)
        {
            Debug.LogError("No emptyObjectDialog given!", this);
            Destroy(this);
            return;
        }

        GameObject currentDialog = Instantiate(emptyObjectDialog, transform);
        dialogMessage = currentDialog.GetComponentInChildren<DialogMessage>();
    }

    public override void OnInteraction(PlayerContext context)
    {
        if (!useStandardText)
            dialogMessage.text = messageText;

        if (soundSourcePlayer != null && playSound)
        {
            soundSourcePlayer.Play();
        } else
        {
            Debug.LogError("No soundSourcePlayer given!");
        }

        dialogMessage.Show();
    }
}
