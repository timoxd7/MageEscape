using MyBox;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    [Header("References & Tags")]
    public BlackBlend blackBlend;
    public GameObject inventoryUi;
    public Transform newCameraPosition;
    public PlayerAccessibility playerAccessibility;
    public GameObject playerCamera;
    public LevelLoader levelLoader;
    public string mainMenuScene = "MainMenu";

    [Header("Zones")]
    public List<ZoneLoader> zonesToEnable;
    public List<ZoneLoader> zonesToDisable;

    [Header("Delayed Executers")]
    public UnityEventExecuter afterBlackExecuter;
    public UnityEventExecuter afterLightExecuter;
    public UnityEventExecuter fireworkExecuter;

    private bool firstBlendOver = false;

    public void EscapeRoom()
    {
        blackBlend.SetBlack(true);
        inventoryUi.SetActive(false);
        playerAccessibility.LockPlayer(false);
        afterBlackExecuter.Execute();
    }

    public void AfterBlackExecuterCallback()
    {
        if (!firstBlendOver)
        {
            firstBlendOver = true;
            blackBlend.SetBlack(false);
            afterLightExecuter.Execute();

            playerCamera.transform.position = newCameraPosition.position;
            playerCamera.transform.rotation = newCameraPosition.rotation;

            if (!zonesToEnable.IsNullOrEmpty())
            {
                foreach (ZoneLoader zone in zonesToEnable)
                {
                    if (zone != null)
                        zone.ActivateZone();
                }
            }

            if (!zonesToDisable.IsNullOrEmpty())
            {
                foreach (ZoneLoader zone in zonesToDisable)
                {
                    if (zone != null)
                        zone.DeactivateZone();
                }
            }

            fireworkExecuter.Execute();
        } else
        {
            playerAccessibility.ReleasePlayer();
            playerAccessibility.LockPlayer();
            levelLoader.LoadLevel(mainMenuScene);
        }
    }

    public void AfterWhiteExecuterCallback()
    {
        blackBlend.SetBlack(true);
        afterBlackExecuter.Execute();
    }
}
