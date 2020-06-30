using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ZoneLoadPoint : MonoBehaviour
{
    public ZoneLoader zone;
    public ZoneLoadPointType loadPointType;
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            if (zone == null)
            {
                Debug.LogError("No zone given!", this);
                return;
            }

            switch (loadPointType)
            {
                case ZoneLoadPointType.None:
                    Debug.LogError("ZoneLoadPointType not spezified!", this);
                    break;

                case ZoneLoadPointType.LoadIn:
                    zone.ActivateZone();
                    break;

                case ZoneLoadPointType.LoadOut:
                    zone.DeactivateZone();
                    break;

                default:
                    Debug.LogError("ZoneLoadPointType illegal", this);
                    break;
            }
        }
    }

    public enum ZoneLoadPointType : byte
    {
        None = 0,
        LoadIn = 1,
        LoadOut = 2
    }
}
