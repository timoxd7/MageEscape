using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagierInteraction : BaseInteraction
{
    public GameObject glassSphere;
    public Animator magicianAnimator;
    public string freeMagicianTrigger = "FreeMagician";
    public MagierFlyAnimation flyAnimator;
    public GameObject fogToDisable;
    public GameObject poisonFogToDisable;
    public DialogMessage glassSphereNotice;
    public DialogMessage keywordInputInit;
    public DialogMessage keywordInputNext;
    public DialogMessage keywordWrong;
    public DialogMessage keywordCorrect;
    public string keyphrase = "regionem lumborum tumor in inferioribus";
    public int phraseWordCount = 5;

    private string currentPhrase = "";
    private int currentWordCount = 0;
    private bool magicianFreed = false;

    public override void OnInteraction(PlayerContext context)
    {
        if (magicianFreed)
        {
            keywordCorrect.Show();
        }
        else
        {
            if (!glassSphere.activeSelf)
                glassSphereNotice.Show();
            else
                keywordInputInit.Show();
        }
    }

    public void AddWord(string word)
    {
        if (currentWordCount != 0)
            currentPhrase += " ";

        currentPhrase += word;
        currentWordCount++;

        if (currentWordCount == phraseWordCount)
        {
            if (currentPhrase == keyphrase)
            {
                UnlockMagician();
            } else
            {
                keywordInputNext.text = "...";
                keywordWrong.Show();
            }

            currentPhrase = "";
            currentWordCount = 0;
        } else
        {
            keywordInputNext.text = currentPhrase + " ...";
            keywordInputNext.Show();
        }
    }

    public void CloseDialog()
    {
        keywordInputNext.text = "...";
    }

    public void UnlockMagician()
    {
        magicianAnimator.SetTrigger(freeMagicianTrigger);
        flyAnimator.UnFly();
        fogToDisable.SetActive(false);
        poisonFogToDisable.SetActive(false);
        magicianFreed = true;
    }
}
